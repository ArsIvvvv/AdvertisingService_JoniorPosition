using AdvertisingService.Data;
using AdvertisingService.Models;
using System.Text.Json;

namespace AdvertisingService.Service
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly IAdvertisingRepository _repository;

        public AdvertisingService(IAdvertisingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> UploadDataAsync(Stream fileStream)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = await JsonSerializer.DeserializeAsync<AdvertisingData>(fileStream, options);

                if (data?.Platforms == null)
                    return false;

                // Очищаем текущие данные
                _repository.Clear();

                // Обрабатываем каждую площадку
                foreach (var platform in data.Platforms)
                {
                    if (string.IsNullOrWhiteSpace(platform.Name) || platform.Locations == null)
                        continue;

                    foreach (var location in platform.Locations)
                    {
                        var normalized = NormalizeLocation(location);
                        if (normalized == null)
                            continue;

                        _repository.AddPlatformToLocation(normalized, platform.Name);
                    }
                }

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public List<string> SearchPlatforms(string location)
        {
            if (string.IsNullOrEmpty(location))
                return new List<string>();

            var normalized = NormalizeLocation(location);
            if (normalized == null)
                return new List<string>();

            var result = new List<string>();
            var current = normalized;

            // Проверяем все вложенные локации, включая корень
            while (!string.IsNullOrEmpty(current))
            {
                var platforms = _repository.GetPlatformsForLocation(current);
                foreach (var platform in platforms)
                {
                    // Ручная проверка уникальности
                    if (!result.Contains(platform))
                    {
                        result.Add(platform);
                    }
                }

                // Поднимаемся на уровень выше
                var lastSlash = current.LastIndexOf('/');
                if (lastSlash == -1)
                    break;

                current = current[..lastSlash];
            }

            return result;
        }

        private static string NormalizeLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location) || !location.StartsWith('/'))
                return null;

            // Удаляем завершающие слэши
            location = location.TrimEnd('/');
            return location.Length == 0 ? "/" : location;
        }
    }
}
