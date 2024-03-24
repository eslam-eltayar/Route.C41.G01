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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // private  readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            // _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address) 
        {

            //return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());

            if (string.IsNullOrEmpty(address))
            {
                return Enumerable.Empty<Employee>().AsQueryable(); // Return an empty queryable
            }

            return _dbContext.Employees.Where(E => string.Equals(E.Address, address, StringComparison.OrdinalIgnoreCase));
        }
    }

      
    
}
