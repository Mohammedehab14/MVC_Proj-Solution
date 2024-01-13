using BLL_Proj.Interfaces;
using DAL_Proj.Contexts;
using DAL_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Repositories
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployee
    {
        private readonly AppDbContext context;

        public EmployeeRepo(AppDbContext context):base(context)
        {
            this.context = context;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
            => context.Employees.Where(E => E.Address == address);

        public IQueryable<Employee> SearchEmployeesByName(string name)
            => context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

    }
}
