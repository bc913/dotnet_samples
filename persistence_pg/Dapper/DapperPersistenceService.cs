using System;
using System.Threading.Tasks;
using System.Data; //IDbConnection
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;


public class DapperPersistenceService<T> : IPersistenceService<T> where T : ModelBase, new()
{

    private readonly string _dbPath;
    private SqliteConnection _db;

    public DapperPersistenceService(string dbPath)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dbPath);
        _dbPath = dbPath;
    }

    private SqliteConnection CreateConnection()
    {
        return new SqliteConnection($"Data Source={_dbPath}");
    }

    public void Init()
    {
        using var connection = CreateConnection();
        _initTable();

        void _initTable()
        {
            var sql = $"""
                CREATE TABLE IF NOT EXISTS
                    Stocks (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Symbol TEXT
                );
            """;

            connection.Execute(sql);
        }
    }

    public async Task InitAsync()
    {
        using var connection = CreateConnection();
        await _initTable();

        async Task _initTable()
        {
            var sql = $"""
                CREATE TABLE IF NOT EXISTS
                    Stocks (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Symbol TEXT
                );
            """;

            await connection.ExecuteAsync(sql);
        }
    }

    public void Post(T obj)
    {
        using var connection = CreateConnection();
        var sql = $"""
            INSERT INTO Stocks (Symbol)
            VALUES ('OSMAN')
        """;

        connection.Execute(sql);
    }
    public async Task PostAsync(T obj)
    {
        using var connection = CreateConnection();
        var sql = $"""
            INSERT INTO Stocks (Symbol)
            VALUES ('OSMAN')
        """;
        await connection.ExecuteAsync(sql);
    }

    public IEnumerable<T> GetAll()
    {
        using var connection = CreateConnection();
        var sql = $"""
            SELECT * FROM Stocks
        """;

        return connection.Query<T>(sql);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = CreateConnection();
        var sql = $"""
            SELECT * FROM Stocks
        """;

        return await connection.QueryAsync<T>(sql);
    }
}