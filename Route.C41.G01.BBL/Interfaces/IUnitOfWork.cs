using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BBL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        // Signature for Property for each and every Repository Interface

        ///public IEmployeeRepository EmployeeRepository { get; set; }
        ///public IDepartmentRepository DepartmentRepository { get; set; }

        IGenericRepository<T> Repository<T>() where T : ModelBase;

        Task<int> Complete();
        
    }
}
