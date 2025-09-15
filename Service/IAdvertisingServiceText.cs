namespace AdvertisingService.Service
{
    public interface IAdvertisingServiceText
    {
        Task<bool> UploadDataAsync(Stream fileStream);
        List<string> SearchPlatforms(string location);
    }
}
