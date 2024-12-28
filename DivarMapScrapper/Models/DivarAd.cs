namespace DivarMapScrapper.Models
{
    public class DivarAd
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Url { get; set; }
        public bool IsInUpperRightQuadrant { get; set; }
        public string Token { get; set; }
    }
}