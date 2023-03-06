namespace CatalogService.DAL.Repositories.Interfaces;

/// <summary>
/// Repository pattern interface
/// </summary>
/// <typeparam name="T">Storage entity</typeparam>
public interface IRepository<T> 
    : IDisposable 
    where T : class
{
    /// <summary>
    /// Getting all objects from repository
    /// </summary>
    /// <returns>Enumerable of all objects</returns>
    IEnumerable<T> GetQuery();
    
    /// <summary>
    /// Getting single object by uniq id
    /// </summary>
    /// <param name="id">GUID of object</param>
    /// <param name="data">Reference for output data if exists</param>
    /// <returns></returns>
    bool TryGet(Guid id, out T data);
    
    /// <summary>
    /// Adding object to repository
    /// </summary>
    /// <param name="item">Object instance</param>
    void Add(T item);
    
    /// <summary>
    /// Updates object in repository
    /// </summary>
    /// <param name="item">Object instance</param>
    void Update(T item);
    
    /// <summary>
    /// Updates object in repository
    /// </summary>
    /// <param name="id">GUID of object</param>
    void Delete(Guid id);
    
    /// <summary>
    /// Saving changes in repository
    /// </summary>
    void Save();
}