using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DivarMapScrapper.Models;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace DivarMapScrapper.Services
{
    public class DivarScraper : IDisposable
    {
        private readonly ChromeDriver driver;

        public DivarScraper()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Optional: run Chrome in headless mode
            driver = new ChromeDriver(options);
        }

        public async Task<List<DivarAd>> ScrapeAdsAsync(string url)
        {
            var ads = new List<DivarAd>();

            try
            {
                driver.Navigate().GoToUrl(url);

                // Initial wait for the page to load
                await Task.Delay(2000);

                var articleTokens = driver.FindElements(By.TagName("article"))
                  .Select(article => new
                  {
                      Token = article.GetAttribute("token"),
                      Url = article.FindElement(By.CssSelector("a.kt-post-card__action"))?.GetAttribute("href") ?? ""
                  })
                  .ToList();

                foreach (var articleInfo in articleTokens)
                {
                    try
                    {
                        // Re-find the article element using its token
                        var postCard = driver.FindElement(By.CssSelector($"article[token='{articleInfo.Token}']"));


                        var token = postCard.GetAttribute("token");
                        var address = postCard.FindElement(By.TagName("a")).GetAttribute("href");
                        var title = postCard.FindElement(By.TagName("h2")).Text;
                        var price = postCard.FindElement(By.ClassName("kt-post-card__description")).Text;
                        var locationName = postCard.FindElement(By.ClassName("kt-post-card__bottom-description")).Text;
                        var adUrl = postCard.FindElement(By.CssSelector("a.kt-post-card__action"))?.GetAttribute("href") ?? "";

                        // Extract coordinates from the ad page
                        var (lat, lon) = await ExtractCoordinatesFromAdPage(adUrl);

                        var ad = new DivarAd
                        {
                            Latitude = lat,
                            Longitude = lon,
                            Title = title,
                            Price = price,
                            Url = adUrl,
                            Token = token
                        };

                        ads.Add(ad);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing post card: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping ads: {ex.Message}");
                throw;
            }

            return ads;
        }

        private async Task<(double lat, double lon)> ExtractCoordinatesFromAdPage(string adUrl)
        {
            try
            {
                // Navigate to the ad page
                driver.Navigate().GoToUrl(adUrl);
                await Task.Delay(2000); // Wait for page to load

                // Look for the map widget in the page source
                var pageSource = driver.PageSource;
                var regex = new Regex(@"(\<script\stype=\""application\/ld\+json\""\>)((?<=>)([^<]+)(?=<))(\<\/script\>)");
                var jsonMatches = regex.Match(pageSource);
                var json = string.Empty;
                if (jsonMatches.Success)
                {
                    json = jsonMatches.Groups[2].Value;
                }

                var appartementsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Apartment>>(json);

                if (appartementsList.Count > 0)
                {
                    return (Convert.ToDouble(appartementsList[0].Geo.Latitude), Convert.ToDouble(appartementsList[0].Geo.Longitude));
                }

                Console.WriteLine($"No coordinates found for URL: {adUrl}");
                return (0, 0); // Return default coordinates if none found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting coordinates from {adUrl}: {ex.Message}");
                return (0, 0);
            }
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}