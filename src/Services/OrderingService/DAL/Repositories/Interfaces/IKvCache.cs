namespace OrderingService.DAL.Repositories.Interfaces;

public interface IKvCache<in TKey, TValue>
{
    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">When this method returns, contains the value associated with
    /// the specified key, if the key is found; otherwise, the default value for the 
    /// type of the <paramref name="value" /> parameter.</param>
    /// <returns>true if contains an element with the specified key; otherwise, false.</returns>
    bool TryGet(TKey key, out TValue? value);

    /// <summary>
    /// Adds the specified key and value to the cache.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    void Add(TKey key, TValue value);
    
    /// <summary>
    /// Removes from cache value by specified key
    /// </summary>
    /// <param name="key">The key of the element to remove</param>
    void Remove(TKey key);
}