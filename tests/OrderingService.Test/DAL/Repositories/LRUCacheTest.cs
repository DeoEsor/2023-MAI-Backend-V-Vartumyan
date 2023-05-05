using OrderingService.DAL.Repositories;
using OrderingService.DAL.Repositories.Interfaces;

namespace OrderingService.Test.DAL.Repositories;

[TestFixture]
public class LruCacheTest
{
    private IKvCache<string, string> Cache { get; set; }
    
    [SetUp]
    public void Setup()
    {
        Cache = new LruCache<string,string>(100);
        Cache.Add("Jesse", "Pinkman");
        Cache.Add("Walter", "White");
        Cache.Add("Jesse", "James");
    }

    [Test]
    public void LruSimpleTest()
    {
        Cache.TryGet("Jesse", out var data);
        Assert.That(data, Is.EqualTo("James"));
        Cache.Remove("Walter");
        Cache.TryGet("Walter", out data);
        Assert.That(data, Is.EqualTo(null));
    }
}