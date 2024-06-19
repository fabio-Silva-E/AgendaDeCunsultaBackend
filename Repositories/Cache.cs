using System;
using System.Runtime.Caching;

namespace Repositories
{
    internal class Cache
    {
        private static readonly ObjectCache cache = MemoryCache.Default;

        public static object get(string chave)
        {
            return cache.Get(chave);
        }

    
        public static void add(string chave, object objeto, int tempo)
        {
            CacheItemPolicy politica = new CacheItemPolicy();
            politica.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(tempo);
            cache.Add(chave, objeto, politica);
        }

        public static void add(string chave, object objeto)
        {
            cache.Add(chave, objeto, null);
        }

        public static void delete(string chave)
        {
            cache.Remove(chave);
        }
    }
}
