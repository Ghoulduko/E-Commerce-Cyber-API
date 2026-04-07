using AutoMapper;
using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using Cyber.Core.Helper;

namespace Cyber.Application.Services;

public class ProductService
{
    private readonly CyberDbContext _context;
    private readonly ProductRepository _service;
    private readonly GenericService<FavoriteProduct> _favoriteProductService;
    private readonly IMapper _mapper;

    public ProductService(ProductRepository service, GenericService<FavoriteProduct> favoriteProductService, IMapper mapper)
    {
        _service = service;
        _favoriteProductService = favoriteProductService;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await _service.GetAll();
        var productsToReturn = _mapper.Map<List<ProductDto>>(products);
        return productsToReturn;
    }

    public async Task<List<ProductDto>> GetProductsByContentType(ContentType type)
    {
        var products = await _service.Filter(p => p.ContentType == type);
        var productsToReturn = _mapper.Map<List<ProductDto>>(products);
        return productsToReturn;
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var product = await _service.GetById(id);
        if (product == null) 
            throw new InvalidOperationException("No product found, try again!");
        var productToReturn = _mapper.Map<ProductDto>(product);
        return productToReturn;
    }

    // public async Task<List<ProductDto>> GetFilteredProducts(ProductFilterDto req)
    // {
    //     var products = await _service.Filter(p => req.Name != null && p.Name.Contains(req.Name) && p.BrandId == req.BrandId && (p.Price >= req.PriceFrom && p.Price <= req.PriceTo));
    //     var productsToReturn = _mapper.Map<List<ProductDto>>(products);
    //     return productsToReturn;
    // }

    public async Task<object> PaginatedProducts(int page, ProductFilterDto productFilter)
    {
        return await _service.PaginateProducts(page, productFilter.BrandId, productFilter.PriceFrom, productFilter.PriceTo);
    }

    public async Task AddProduct(ProductDto product)
    {
        product.Name = product.Name.Trim();
        bool productExist = await _service.CheckExistence(p => p.Name.ToLower() == product.Name.ToLower() && p.ContentType == product.ContentType);
        if (productExist) 
            throw new ArgumentException("The product already exists");

        if (product == null)
            throw new InvalidOperationException("Your entered product is invalid, Try again");
        
        var productToAdd = _mapper.Map<Product>(product);
        await _service.Add(productToAdd);
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
        var products = await _favoriteProductService.Filter(i => i.UserId == userId);

        var productsToReturn = new List<Product>();
        foreach (var favoriteProduct in products)
        {
            var product = await _service.GetById(favoriteProduct.ProductId);
            if (product != null) productsToReturn.Add(product);
        }
        
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

    public async Task UpdateProduct(int id, Product product)
    {
        var productToUpdate = await _service.GetById(id);

        if (productToUpdate == null)
            throw new ArgumentNullException($"No Product was found with the id of: {id}.");

        _mapper.Map(product, productToUpdate);
        await _service.Save();
    }

    public async Task DeleteProduct(int id)
    {
        var productToDelete = await _service.GetById(id);
        if (productToDelete == null)
            throw new ArgumentNullException($"No Product was found to delete with the id of: {id}.");

        await _service.Delete(id);
    }
}
