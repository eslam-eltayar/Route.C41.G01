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

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            //return _dbContext.Employees.Find(id);

            return _dbContext.Find<T>(id); // EF Core 3.1 New Feature

            //var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();

            //if (Employee == null)
            //    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();

            //return Employee;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();
        }
            
    }
}
