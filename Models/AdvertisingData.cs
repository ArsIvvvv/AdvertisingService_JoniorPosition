namespace AdvertisingService.Models
{
    public class AdvertisingData
    {
        // Для десериализации в json
        public List<Platform> Platforms { get; set; } = new List<Platform>();
    }
}
