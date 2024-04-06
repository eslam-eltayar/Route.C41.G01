using Microsoft.EntityFrameworkCore;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Data;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BBL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext; // Null
        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR for creating object from "ApplicationDbContext"
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        => _dbContext.Set<T>().Add(entity);

        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
        => _dbContext.Set<T>().Remove(entity);




        public async Task<T> GetAsync(int id)
        {
            //return _dbContext.Employees.Find(id);

            return await _dbContext.FindAsync<T>(id); // EF Core 3.1 New Feature

            //var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();

            //if (Employee == null)
            //    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();

            //return Employee;
        }

        public virtual async  Task< IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            


                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }


    }
}
