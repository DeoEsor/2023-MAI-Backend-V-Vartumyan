// ReSharper disable once CheckNamespace
namespace IdentityService.DAL.Repositories;

public sealed partial class LruCache<TKey, TValue> where TKey : notnull
{
    private struct Entry
    {
        public readonly LinkedListNode<TKey>? Node;
        public TValue Value;

        public Entry(LinkedListNode<TKey>? node, TValue value)
        {
            Node = node;
            Value = value;
        }
    }   
}