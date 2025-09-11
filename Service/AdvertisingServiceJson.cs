using AdvertisingService.Data;
using AdvertisingService.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace AdvertisingService.Service
{
    public class AdvertisingServiceJson : IAdvertisingServiceJson
    {
        private readonly IAdvertisingRepository _repository;

        public AdvertisingServiceJson(IAdvertisingRepository repository)
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

                _repository.Clear();

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

            return SearchPlatformsInternal(normalized);
        }

        private List<string> SearchPlatformsInternal(string location)
        {
            var result = new HashSet<string>();
            var allLocations = _repository.GetAllLocations().ToList();
            var sortedLocations = allLocations.OrderByDescending(loc => loc.Length).ToList();

            foreach (var sortedLocation in sortedLocations)
            {
                if (IsPrefix(sortedLocation, location))
                {
                    var platforms = _repository.GetPlatformsForLocation(sortedLocation);
                    foreach (var platform in platforms)
                    {
                        result.Add(platform);
                    }
                }
            }

            return result.ToList();
        }

        private bool IsPrefix(string storedLocation, string requestedLocation)
        {
            if (storedLocation == "/")
                return requestedLocation.StartsWith("/");

            return requestedLocation == storedLocation ||
                   requestedLocation.StartsWith(storedLocation + "/");
        }

        private static string NormalizeLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location) || !location.StartsWith('/'))
                return null;

            location = location.TrimEnd('/');
            return location.Length == 0 ? "/" : location;
        }
    }
}
