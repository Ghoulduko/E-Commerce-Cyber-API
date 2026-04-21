using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Core.Entities;
using Cyber.Core.Enums;

namespace Cyber.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProducts();

    Task<List<ProductDto>> GetProductsByContentType(ContentType type);

    Task<ProductDto> GetProductById(int id);

    Task<object> PaginatedProducts(int page, ProductFilterDto productFilter);

    Task AddProduct(ProductDto product);

    Task UpdateProduct(int id, Product product);

    Task DeleteProduct(int id);
}