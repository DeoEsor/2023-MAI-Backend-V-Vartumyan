using OrderingService.DAL.Repositories.Interfaces;

namespace OrderingService.DAL.Repositories;

/// <summary>
/// A least-recently-used cache stored like a dictionary.
/// </summary>
/// <typeparam name="TKey">The type of the key to the cached item.</typeparam>
/// <typeparam name="TValue">The type of the cached item.</typeparam>
public sealed partial class LruCache<TKey, TValue> 
    : IKvCache<TKey, TValue> 
    where TKey : notnull
{
    /// <summary>
    /// Default maximum number of elements to cache.
    /// </summary>
    private const int DefaultCapacity = 255;

    private readonly object _lockObj = new();
    private readonly int _capacity;
    private readonly Dictionary<TKey, Entry> _cacheMap;
    private readonly LinkedList<TKey> _cacheList;

    /// <summary>
    /// Initializes a new instance of the <see cref="Models.LruCache{TKey,TValue}"/> class.
    /// </summary>
    public LruCache()
        : this(DefaultCapacity)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Models.LruCache{TKey,TValue}"/> class.
    /// </summary>
    /// <param name="capacity">Maximum number of elements to cache.</param>
    public LruCache(int capacity)
    {
        _capacity = capacity > 0 ? capacity : DefaultCapacity;
        _cacheMap = new Dictionary<TKey, Entry>();
        _cacheList = new LinkedList<TKey>();
    }


    /// <inheritdoc />
    public bool TryGet(TKey key, out TValue? value)
    {
        lock (_lockObj)
        {
            if (_cacheMap.TryGetValue(key, out var entry))
            {
                Touch(entry.Node);
                value = entry.Value;
                return true;
            }
        }

        value = default;
        return false;
    }

    /// <inheritdoc />
    public void Add(TKey key, TValue value)
    {
        lock (_lockObj)
        {
            if (_cacheMap.TryGetValue(key, out var entry))
            {
                entry.Value = value;
                _cacheMap[key] = entry;
                Touch(entry.Node);
                return;
            }

            LinkedListNode<TKey>? node;
            if (_cacheMap.Count >= _capacity)
            {
                node = _cacheList.Last ?? throw new NullReferenceException();
                _cacheMap.Remove(node.Value);
                _cacheList.RemoveLast();
                node.Value = key;
            }
            else
                node = new LinkedListNode<TKey>(key);

            _cacheList.AddFirst(node);
            _cacheMap.Add(key, new Entry(node, value));
        }
    }

    public void Remove(TKey key)
    {
        lock (_lockObj)
        {
            _cacheMap.Remove(key);
            _cacheList.Remove(key);
        }
    }

    private void Touch(LinkedListNode<TKey>? node)
    {
        if (node == _cacheList.First || node == null) 
            return;
        
        _cacheList.Remove(node);
        _cacheList.AddFirst(node);
    }
}