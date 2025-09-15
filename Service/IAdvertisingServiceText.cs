namespace AdvertisingService.Service
{
    public interface IAdvertisingServiceJson
    {
        Task<bool> UploadDataAsync(Stream fileStream);
        List<string> SearchPlatforms(string location);
    }
}
