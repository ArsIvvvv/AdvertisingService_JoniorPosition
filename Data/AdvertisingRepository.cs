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
            _locationPlatforms.AddOrUpdate(location,
                _ => new List<string> { platform },
                (_, list) =>
                {
                    if (!list.Contains(platform))
                        list.Add(platform);
                    return list;
                });
        }

        public List<string> GetPlatformsForLocation(string location)
        {
            return _locationPlatforms.TryGetValue(location, out var platforms)
                ? new List<string>(platforms)
                : new List<string>();
        }

        public bool LocationExists(string location)
        {
            return _locationPlatforms.ContainsKey(location);
        }
    }
}
