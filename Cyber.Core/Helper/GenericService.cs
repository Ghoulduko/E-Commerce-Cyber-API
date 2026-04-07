using AutoMapper;
using Cyber.Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Helper;

public class GenericService<T> where T : class
{
    protected readonly CyberDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericService(CyberDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }

    public async Task<T?> GetById(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task Delete(int id)
    {
        var itemToDelete = await GetById(id);
        if (itemToDelete == null) throw new Exception("item not found");
        _dbSet.Remove(itemToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> Filter(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<bool> CheckExistence(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
