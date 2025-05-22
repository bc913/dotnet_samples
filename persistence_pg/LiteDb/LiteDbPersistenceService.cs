using System.Linq.Expressions;
using LiteDB;

namespace DefaultNamespace;

public class LiteDbRepository<T> : IDisposable where T : IEntity
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<T> _collection;

    public LiteDbRepository(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        string connectionString = $"Filename={filePath}";
        _db = new LiteDatabase(connectionString);
        _collection = _db.GetCollection<T>(typeof(T).Name);
        
        
        // Ensure we have an index on the Id field
        // If all queries are based on Id, no need to change
        // If the query criteria is different, then ensure index accordingly
        _collection.EnsureIndex(x => x.Id);
    }
    
    #region Sync Methods
    
    public T GetById(string Id) => _collection.FindById(Id);
    public IEnumerable<T> GetAll() => _collection.FindAll();
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _collection.Find(predicate);

    public void Insert(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        // Not sure about this
        // if(string.IsNullOrEmpty(entity.Id))
        //     entity.Id = Guid.NewGuid().ToString();
        _collection.Insert(entity);
    }

    public void InsertMany(IEnumerable<T> entities) => _collection.InsertBulk(entities);
    
    public bool Update(T entity) => _collection.Update(entity);
    public bool Deleted(string id) => _collection.Delete(id);
    
    #endregion
    
    
    #region IDisposable
    
    private bool _disposed = false;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects).
                _db?.Dispose();
            }
            // Free unmanaged resources (unmanaged objects) and override a finalizer below.
            // Set large fields to null.
            _disposed = true;
        }
    }

    // Optional: Override finalizer only if Dispose(bool disposing) has code to free unmanaged resources.
    // ~LiteDbPersistenceService()
    // {
    //    Dispose(false);
    // }
    
    #endregion
}