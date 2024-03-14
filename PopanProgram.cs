using Cache;
using System.Xml;


public class TestCache
{
    public static void CacheTest()
    {
        var cache = new CacheModule<object>();

        cache.add("Key1", 2, TimeSpan.FromSeconds(10));

        cache.add("Key2", "BV", TimeSpan.FromSeconds(20));

        var obj = new Dictionary<string, int>();
        obj["Cristi"] = 10;
        cache.add("Key3", obj, TimeSpan.FromSeconds(30));

        cache.updateTime("Key1", TimeSpan.FromSeconds(40));

        while (cache.getCacheKeys().Count > 0)
        {
            try
            {
                cache.removeExpiredKeys();

                foreach (string key in cache.getCacheKeys().Keys)
                {

                    var cacheItem = cache.getCacheItem(key);

                    if (cacheItem.GetType() == typeof(Dictionary<string, int>))
                    {
                        var dic = (Dictionary<string, int>)cacheItem;
                        foreach (var item in dic)
                        {
                            Console.WriteLine(item.Key + ":" + item.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine(cacheItem);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Key expired!");
            }
            Console.WriteLine("\n");
            Thread.Sleep(1000);
        }
    }
}
