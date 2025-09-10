namespace AdvertisingService.Data
{
    public interface IAdvertisingRepository
    {
        void Clear();
        void AddPlatformToLocation(string location, string platform);
        List<string> GetPlatformsForLocation(string location);
        bool LocationExists(string location);
    }
}
