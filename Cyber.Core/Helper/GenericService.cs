using Cyber.Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Helper;

public class GenericService<T> where T : class
{
    private readonly CyberDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericService(CyberDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) throw new ArgumentNullException("item not found");
        return entity;
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var itemToDelete = GetById(id);
        if (itemToDelete == null) throw new ArgumentNullException("item not found");
        _dbSet.Remove(itemToDelete);
        _context.SaveChanges();
    }
}
