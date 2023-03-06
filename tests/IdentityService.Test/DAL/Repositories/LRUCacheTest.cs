using IdentityService.DAL.Repositories;
using IdentityService.DAL.Repositories.Interfaces;

namespace IdentityService.Test.DAL.Repositories;

[TestFixture]
public class LruCacheTest
{
    private IKvCache<string, string> Cache { get; set; }
    
    [SetUp]
    public void Setup()
    {
        Cache = new KvCache<string,string>(100);
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