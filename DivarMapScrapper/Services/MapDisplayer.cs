using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using DivarMapScrapper.Models;

namespace DivarMapScrapper.Services
{
    public class MapDisplayer
    {
        private readonly WebView2 mapView;
        private const double TEHRAN_CENTER_LAT = 35.6892;
        private const double TEHRAN_CENTER_LNG = 51.3890;

        public MapDisplayer(WebView2 mapView)
        {
            this.mapView = mapView;
        }

        public async Task DisplayAdsOnMap(List<DivarAd> ads)
        {
            if (ads == null || !ads.Any())
            {
                throw new ArgumentException("No ads to display");
            }

            // Calculate quadrant for each ad
            foreach (var ad in ads)
            {
                ad.IsInUpperRightQuadrant = ad.Latitude >= TEHRAN_CENTER_LAT && 
                                          ad.Longitude >= TEHRAN_CENTER_LNG;
            }

            // Create markers JSON
            var markersJson = string.Join(",\n",
                ads.Select(ad =>
                    $@"{{
                        lat: {ad.Latitude}, 
                        lng: {ad.Longitude}, 
                        inQuadrant: {ad.IsInUpperRightQuadrant.ToString().ToLower()},
                        url: '{ad.Url.Replace("'", "\\'")}',
                        title: '{ad.Title.Replace("'", "\\'").Replace("\n", " ")}',
                        price: '{ad.Price.Replace("'", "\\'").Replace("\n", " ")}'
                    }}"
                ));

            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Locations Map</title>
                    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.7.1/dist/leaflet.css' />
                    <script src='https://unpkg.com/leaflet@1.7.1/dist/leaflet.js'></script>
                    <style>
                        body {{ margin: 0; padding: 0; }}
                        #map {{ width: 100%; height: 100vh; }}
                        .location-count {{ 
                            position: fixed; 
                            top: 10px; 
                            right: 10px; 
                            background: white; 
                            padding: 10px; 
                            border-radius: 5px; 
                            box-shadow: 0 0 10px rgba(0,0,0,0.2);
                            z-index: 1000;
                        }}
                    </style>
                </head>
                <body>
                    <div id='map'></div>
                    <div class='location-count'>
                        Total Locations: <span id='totalCount'>0</span><br>
                        In Upper Right: <span id='inQuadrantCount'>0</span>
                    </div>
                    <script>
                        var map = L.map('map').setView([{TEHRAN_CENTER_LAT}, {TEHRAN_CENTER_LNG}], 12);
                        L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                            attribution: '© OpenStreetMap contributors'
                        }}).addTo(map);

                        var markers = [{markersJson}];
                        var bounds = L.latLngBounds();
                        var inQuadrantCount = 0;

                        markers.forEach((loc, index) => {{
                            var color = loc.inQuadrant ? 'green' : 'red';
                            if (loc.inQuadrant) inQuadrantCount++;

                            var marker = L.marker([loc.lat, loc.lng], {{
                                icon: L.divIcon({{
                                    className: 'location-marker',
                                    html: `<div style=""width: 10px; height: 10px; background: ${{color}}; border-radius: 50%; border: 2px solid white;""></div>`,
                                    iconSize: [14, 14]
                                }})
                            }}).addTo(map);

                            marker.bindTooltip(loc.title, {{
                                className: 'custom-tooltip',
                                direction: 'top'
                            }});

                            marker.bindPopup(`
                                <div style=""max-width: 300px;"">
                                    <h3>${{loc.title}}</h3>
                                    <p><strong>Price:</strong> ${{loc.price}}</p>
                                    <p><strong>Location:</strong><br>
                                    Lat: ${{loc.lat}}<br>
                                    Lng: ${{loc.lng}}</p>
                                    <p><a href=""${{loc.url}}"" target=""_blank"">View on Divar</a></p>
                                </div>
                            `);

                            bounds.extend(marker.getLatLng());
                        }});

                        document.getElementById('totalCount').textContent = markers.length;
                        document.getElementById('inQuadrantCount').textContent = inQuadrantCount;

                        map.fitBounds(bounds, {{
                            padding: [50, 50]
                        }});
                    </script>
                </body>
                </html>";

            mapView.CoreWebView2.NavigateToString(htmlContent);
        }
    }
} 