namespace AdvertisingService.Models
{
    public class Platform
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Locations { get; set; } = new List<string>();
    }
}
