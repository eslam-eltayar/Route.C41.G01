using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Data;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BBL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        //private Dictionary<string, IGenericRepository<ModelBase>> _repositories;
        private Hashtable _repositories;


        public UnitOfWork(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;

            if (!_repositories.ContainsKey(Key))
            {
                
                if (Key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositories.Add(Key, repository);

                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(Key, repository);

                }
               
            }


            return _repositories[Key] as IGenericRepository<T>;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


    }
}
