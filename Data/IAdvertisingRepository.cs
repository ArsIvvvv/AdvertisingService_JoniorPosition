namespace AdvertisingService.Data
{
    public interface IAdvertisingRepository
    {
        void Clear();
        void AddPlatformToLocation(string location, string platform);
        List<string> GetPlatformsForLocation(string location);
        IEnumerable<string> GetAllLocations();
    }
}
