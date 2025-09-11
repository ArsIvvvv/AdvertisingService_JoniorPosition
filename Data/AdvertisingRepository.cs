using System.Collections.Concurrent;

namespace AdvertisingService.Data
{
    public class AdvertisingRepository : IAdvertisingRepository
    {
        private readonly ConcurrentDictionary<string, List<string>> _locationPlatforms = new();

        public void Clear()
        {
            _locationPlatforms.Clear();
        }

        public void AddPlatformToLocation(string location, string platform)
        {
            if (!_locationPlatforms.ContainsKey(location))
            {
                _locationPlatforms[location] = new List<string>();
            }

            if (!_locationPlatforms[location].Contains(platform))
            {
                _locationPlatforms[location].Add(platform);
            }
        }

        public List<string> GetPlatformsForLocation(string location)
        {
            return _locationPlatforms.TryGetValue(location, out var platforms)
                ? new List<string>(platforms)
                : new List<string>();
        }

        public IEnumerable<string> GetAllLocations()
        {
            return _locationPlatforms.Keys.ToList();
        }

    }
}
