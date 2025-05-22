using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DefaultNamespace;
using LiteDB;

// public class Client
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public string[] Phones { get; set; }
//     public bool IsActive { get; set; }
//     
//     public Address Address { get; set; }
//
//     public override string ToString()
//     {
//         return $" Id: {Id}, Name: {Name}, Phones: {string.Join(",", Phones)} | Address: {Address}";
//     }
// }

public class Client : EntityBase
{
    public string Name { get; set; }
    public string[] Phones { get; set; }
    public int IsActive { get; set; }
    
    public OrderItem OrderItem { get; set; }

    public override string ToString()
    {
        return $" Id: {Id}, Name: {Name}, Phones: {string.Join(",", Phones)} | IsActive:{IsActive}| OrderItem: {OrderItem}";
    }
}

// Export to json
// https://github.com/litedb-org/LiteDB/discussions/2051

public class LiteDbRunner<T> : IDisposable where T : EntityBase
{
    private LiteDatabase _db;
    private ILiteCollection<T> _collection;
    
    public LiteDbRunner(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        var connectionStr = $"Filename={filePath};Connection=shared";
        _db = new LiteDatabase(connectionStr);
        MigrationHandler.Migrate(_db);
        _collection = _db.GetCollection<T>(GetCollectionName(typeof(T)));
        //_collection.EnsureIndex(c => c.Name);
    }
    
    public IEnumerable<T> GetAll() => _collection.FindAll().ToList();
    //public IEnumerable<Client> GetByName(string name)=>_collection.Find(c => c.Name.Contains(name));
    public T GetOneById(int id) => _collection.FindById(id);
    //public Client? GetOneByName(string name) => _collection.FindOne(c => c.Name.Contains(name));
    
    public void InsertOne(T obj) => _collection.Insert(obj);
    public void UpdateOne(T obj) => _collection.Update(obj);
    public void DeleteOne(T obj) => _collection.Delete(obj.Id);

    // public void DeleteOneByName(string name)
    // {
    //     _collection.EnsureIndex(c => c.Name);
    //     _collection.Delete(name);
    // }

    private static string GetCollectionName(Type t)
    {
        // Simple pluralization, more complex rules might be needed
        var typeName = t.Name;
        if (typeName.EndsWith("y"))
            return typeName.Substring(0, typeName.Length - 1) + "ies";
        if (typeName.EndsWith("s"))
            return typeName + "es";
        return typeName + "s";
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    private static class MigrationHandler
    {
        /*
         * New or removed fields are handled gracefully.
         * Changing datatype of existing field can be converted in code by (e.g.) using db.Engine.UserVersion to track your db versions.
         * When renaming class fields, use BsonField attribute to differentiate between storage name and class field name.
         */
        public static void Migrate(ILiteDatabase db)
        {
            var dbVersion = db.UserVersion;
            
            if (dbVersion < 1)
            {
                MigrateToV1(db);
                db.UserVersion = 1;
            }

            if (dbVersion < 2)
            {
                MigrateToV2(db);
                db.UserVersion = 2;
            }
        }

        private static void MigrateToV1(ILiteDatabase db)
        {
            // Find the collection first
            var col = db.GetCollection("Clients");
            
            /*
             * Use ToList() to prevent Disposed exception
             * https://github.com/litedb-org/LiteDB/issues/2502
             */
            var docs = col.Query().Where(c => c.ContainsKey("IsActive")).ToList();
            //var docs = col.FindAll().ToList();
            foreach (var doc in docs)
            {
                doc["IsActive"] = Convert.ToInt32(doc["IsActive"].AsInt32);
                col.Update(doc);
            }
        }

        private static void MigrateToV2(ILiteDatabase db)
        {
            
        }
    }
}