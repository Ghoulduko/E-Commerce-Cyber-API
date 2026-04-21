using AutoMapper;
using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Interfaces;
using Cyber.Core.Entities;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class FavoriteProductService : IFavoriteProductService
{
    private readonly IProductRepository _service;
    private readonly IGenericService<FavoriteProduct> _favoriteProductService;
    private readonly IMapper _mapper;
    
    public FavoriteProductService(IProductRepository service, IGenericService<FavoriteProduct> favoriteProductService, IMapper mapper)
    {
        _service = service;
        _favoriteProductService = favoriteProductService;
        _mapper = mapper;
    }

    public async Task AddProductToFavorites(FavoriteProductDto favoriteProduct)
    {
        var favoritedProductExistence = await _favoriteProductService.CheckExistence(x => x.ProductId == favoriteProduct.ProductId);
        if (favoritedProductExistence == true) throw new Exception("The product is already favorited");
        
        var productToAdd = _mapper.Map<FavoriteProduct>(favoriteProduct);
        await _favoriteProductService.Add(productToAdd);
        await _favoriteProductService.Save();
    }
    
    public async Task<List<ProductDto>> GetAllFavoritedProducts(int userId)
    {
        var productsToReturn = await _service.GetFavoriteProducts(userId);
        
        return _mapper.Map<List<ProductDto>>(productsToReturn);
    }

    public async Task DeleteProductFromFavorites(int id, int userId)
    {
        var favoriteProduct = await _favoriteProductService.GetById(id);
        if (favoriteProduct == null) 
            throw new ArgumentNullException($"No Product was found with the id of: {id}.");
        if (favoriteProduct.UserId != userId)
            throw new ArgumentException("You cannot delete favorite product with other user");
        
        await _favoriteProductService.Delete(id);
    }
    
}