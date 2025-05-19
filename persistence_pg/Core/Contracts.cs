using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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