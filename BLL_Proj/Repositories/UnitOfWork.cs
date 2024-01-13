using BLL_Proj.Interfaces;
using DAL_Proj.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IEmployee EmployeeRepo { get; set; }
        public IDepartment DepartmentRepo { get; set; }
        public AppDbContext _Context { get; }

        public UnitOfWork(AppDbContext context)
        {
            _Context = context;
            EmployeeRepo = new EmployeeRepo(context);
            DepartmentRepo = new DepartmentRepo(context);
        }

        public async Task<int> Complete()
           => await _Context.SaveChangesAsync();

        public void Dispose()
           => _Context.Dispose();
    }
}
