using System.Runtime.Caching;

namespace PizzaFinalApp
{
    public static class CacheFacade
    {
        private static ObjectCache _cache;

        static CacheFacade()
        {
            _cache = MemoreCache.Default;
        }

        public static void Set(string key,object data)
        {
            _cache.Set(key, data, null);
        }

        public static object GetObject(string key)
        {
            return _cache[key];
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
