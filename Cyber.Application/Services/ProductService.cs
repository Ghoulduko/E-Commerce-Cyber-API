using AutoMapper;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Services;

public class ProductService
{
    private readonly CyberDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductService(CyberDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<Product> GetAllProducts()
    {
        return _dbContext.Products.ToList();
    }

    public List<Product> GetFilteredProducts(string name, Brand brand, decimal price)
    {
        var products = _dbContext.Products.Where(p => p.Name.Contains(name) && p.Brand == brand && p.Price <= price).ToList();
        return products;
    }

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException("Your entered product is invalid, Try again");
        
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void UpdateProduct(int id, Product product)
    {
        var productToUpdate = _dbContext.Products.SingleOrDefault(p => p.Id == id);

        if (productToUpdate == null)
            throw new ArgumentNullException($"No Product was found with the id of: {id}.");

        _mapper.Map(product, productToUpdate);

        _dbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var productToDelete = _dbContext.Products.SingleOrDefault(p => p.Id == id);
        if (productToDelete == null)
            throw new ArgumentNullException($"No Product was found to delete with the id of: {id}.");

        _dbContext.Products.Remove(productToDelete);
        _dbContext.SaveChanges();
    }
}
