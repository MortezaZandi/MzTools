using System.Collections.Generic;

namespace DivarMapScrapper.Models
{
    public class ApartmentList
    {
        public List<Apartment> Apartments { get; set; }
    }

    public class Apartment
    {
        public string Context { get; set; } // Represents "@context"
        public string Type { get; set; } // Represents "@type"
        public string AccommodationCategory { get; set; }
        public string Description { get; set; }
        public FloorSize FloorSize { get; set; }
        public GeoCoordinates Geo { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string NumberOfRooms { get; set; }
        public string Url { get; set; }
        public WebInfo WebInfo { get; set; }
    }

}
