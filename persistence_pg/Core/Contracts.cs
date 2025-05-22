using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DefaultNamespace;

public interface IResource
{
    Task InitAsync();
}


public interface IPersistenceService<T> : IResource
{
    // Task<bool> InitAsync();
    // IEnumerable<T> GetAllModelsAsync<T>();
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    // T GetById<T>(int id);
    // T GetByIdAsync<T>(int id);


    void Post(T obj);
    Task PostAsync(T obj);
}

public interface IRepository<T> where T : IEntity
{
    T GetById(string id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    void Insert(T entity);
    void InsertMany(IEnumerable<T> entities);
    
    bool Update(T entity);
    bool Delete(string id);
    int Count(Expression<Func<T, bool>>? predicate = null);

}

public interface IDbContext
{
    IRepository<T> GetRepository<T>() where T : IEntity;
}

public interface IDbContextFactory
{
    IDbContext CreateDbContext();
}