using AdvertisingService.Data;
using AdvertisingService.Models;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace AdvertisingService.Service
{
    public class AdvertisingServiceText : IAdvertisingServiceText
    {
        private readonly IAdvertisingRepository _repository;

        public AdvertisingServiceText(IAdvertisingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> UploadDataAsync(Stream fileStream)
        {
            try
            {
                _repository.Clear();

                using var reader = new StreamReader(fileStream, Encoding.UTF8);

                while (await reader.ReadLineAsync() is string line)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var parts = line.Split(':', 2);
                    if (parts.Length != 2)
                        continue;

                    var platform = parts[0].Trim();
                    var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var location in locations)
                    {
                        var normalized = NormalizeLocation(location.Trim());
                        if (normalized != null)
                        {
                            _repository.AddPlatformToLocation(normalized, platform);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
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

            return _repository.GetAllLocations()
                .Where(storedLocation => IsPrefix(storedLocation, normalized))
                .SelectMany(storedLocation => _repository.GetPlatformsForLocation(storedLocation))
                .Distinct()
                .ToList();
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
