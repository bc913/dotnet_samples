using System.Linq.Expressions;
using DefaultNamespace;

namespace LiteDB.Engine;

public class LiteDbRepository<T> : IRepository<T> where T : IEntity
{
    
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<T> _collection;

    public LiteDbRepository(LiteDatabase db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _collection = db.GetCollection<T>(typeof(T).Name);
        
        _collection.EnsureIndex(x => x.Id);
    }
    
    public T GetById(string id) => _collection.FindById(id);

    public IEnumerable<T> GetAll() => _collection.FindAll(); //.ToList()

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _collection.Find(predicate);

    public void Insert(T entity) => _collection.Insert(entity);

    public void InsertMany(IEnumerable<T> entities) => _collection.InsertBulk(entities);

    public bool Update(T entity) => _collection.Update(entity);

    public bool Delete(string id) => _collection.Delete(id);


    public int Count(Expression<Func<T, bool>>? predicate = null) => predicate != null 
        ? _collection.Count(predicate) 
        : _collection.Count();
}

// Might be in data
public class DbConfig
{
    public string ConnectionString { get; set; }
    public bool UseSingletonConnection { get; set; } = true;
    public bool EnableLogging { get; set; } = false;
}

public class LiteDbContext : IDbContext
{
    private readonly LiteDatabase _db;
    private readonly Dictionary<Type, object> _repos;

    public LiteDbContext(DbConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrEmpty(config.ConnectionString);
        
        var connectionString = new ConnectionString(config.ConnectionString);
        
        var mapper = BsonMapper.Global;
        ConfigureBsonMapper(ref mapper);
    }

    private void ConfigureBsonMapper(ref BsonMapper mapper)
    {
        // Handle custom mappings here if needed
        // Example:
        // mapper.Entity<YourClass>().Id(x => x.CustomId);
            
        // Register converters for custom types if needed
        // Example:
        // mapper.RegisterType<CustomType>(
        //     serialize: (obj) => new BsonValue(obj.ToString()),
        //     deserialize: (bson) => new CustomType(bson.AsString)
        // );
    }
    
    public IRepository<T> GetRepository<T>() where T : IEntity
    {
        
        var type = typeof(T);

        if (!_repos.ContainsKey(type))
        {
            _repos[type] = new LiteDbRepository<T>(_db);
        }

        return (IRepository<T>)_repos[type];
    }
    
    
    
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