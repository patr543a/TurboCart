﻿namespace TurboCart.Infrastructure.Persistence.Interfaces;

public interface IRepository<T>
{
    void Save(T entity);
    void Delete(T entity);
    T GetById(object entityId);
    IEnumerable<T> GetAll();
}