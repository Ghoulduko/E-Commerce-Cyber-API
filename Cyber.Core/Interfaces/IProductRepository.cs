using System.Linq.Expressions;
using Cyber.Core.Entities;

namespace Cyber.Core.Interfaces;

public interface IProductRepository
{
    Task Add(Product product);
    Task<object> PaginateProducts(int page, int brandId, decimal priceFrom, decimal priceTo);
    Task<List<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<bool> CheckExistence(Expression<Func<Product, bool>> predicate);
    Task<List<Product>> Filter(Expression<Func<Product, bool>> predicate);
    Task<List<Product>> GetFavoriteProducts(int userId);
    Task Save();
    Task Delete(int id);
}