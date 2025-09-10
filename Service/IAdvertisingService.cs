namespace AdvertisingService.Service
{
    public interface IAdvertisingService
    {
        Task<bool> UploadDataAsync(Stream fileStream);
        List<string> SearchPlatforms(string location);
    }
}
