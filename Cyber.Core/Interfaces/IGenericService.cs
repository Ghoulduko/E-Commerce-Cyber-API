using System.Linq.Expressions;

namespace Cyber.Core.Interfaces;

public interface IGenericService<T> where T : class
{
    Task Add(T entity);
    Task<List<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task<T?> GetById(int id);
    Task Delete(int id);
    Task<List<T>> Filter(Expression<Func<T, bool>> predicate);
    Task<T> GetFirst(Expression<Func<T, bool>> predicate);
    Task<bool> CheckExistence(Expression<Func<T, bool>> predicate);
    Task Save();
}