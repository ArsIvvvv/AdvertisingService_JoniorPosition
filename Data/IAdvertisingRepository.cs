namespace AdvertisingService.Data
{
    public interface IAdvertisingRepository
    {
        // Метод очистки
        void Clear();

        // Метод для добавления платформ
        void AddPlatformToLocation(string location, string platform);
        //Метод для получения платформ
        List<string> GetPlatformsForLocation(string location);
        //Метод для получения локаций
        IEnumerable<string> GetAllLocations();
    }
}
