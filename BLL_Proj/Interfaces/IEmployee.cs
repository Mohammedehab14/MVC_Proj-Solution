using BLL_Proj.Repositories;
using DAL_Proj.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Interfaces
{
    public interface IEmployee : IGenericRepo<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);
        IQueryable<Employee> SearchEmployeesByName(string name);


    }
}
