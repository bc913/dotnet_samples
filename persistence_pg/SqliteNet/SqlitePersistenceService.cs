using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;

/*
IF there is no annotation given for table name
and 
the custom table name is created through sql command
_db.Query<T> does not work since it assumes the table
name is as same as the class name.

*/
public class SqlitePersistenceService<T> : IPersistenceService<T> where T : new()
{
    private SQLiteConnection _db;
    private SQLiteAsyncConnection _dbAsync;
    public SqlitePersistenceService(string dbPath)
    {
        Console.WriteLine($"dbPath: {dbPath}");
        _db = new SQLiteConnection(dbPath);
        _dbAsync = new SQLiteAsyncConnection(dbPath);

        _db.Execute($"CREATE TABLE IF NOT EXISTS stocks (Id INTEGER PRIMARY KEY AUTOINCREMENT, Symbol TEXT)");

    }

    public async Task InitAsync()
    {
        
    }

    public void Post(T obj)
    {
        //_db.Execute("insert into stocks(Symbol) values (?)", "MSFT");
        _db.Execute("INSERT INTO stocks(Symbol) values ('MSFT')");

    }
    public async Task PostAsync(T obj)
    {
        await _dbAsync.ExecuteAsync("INSERT INTO stocks(Symbol) values ('MSFT')");
    }

    public IEnumerable<T> GetAll()
    {
        var sql = $"""
            SELECT * FROM Stocks
        """;
        return _db.Query<T>(sql);
        // return _db.Table<T>().ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"""
            SELECT * FROM Stocks
        """;
        return await _dbAsync.QueryAsync<T>(sql);
    }
}

