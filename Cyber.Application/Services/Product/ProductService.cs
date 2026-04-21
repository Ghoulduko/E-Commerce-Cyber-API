using AutoMapper;
using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Interfaces;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _service;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository service, IMapper mapper)
    {
        _service = service;
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
