using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;

namespace Cyber.Application.Interfaces;

public interface IFavoriteProductService
{
    Task AddProductToFavorites(FavoriteProductDto favoriteProduct);

    Task<List<ProductDto>> GetAllFavoritedProducts(int userId);

    Task DeleteProductFromFavorites(int id, int userId);
}