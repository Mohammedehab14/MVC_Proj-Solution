using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployee EmployeeRepo { get; set; }
        public IDepartment DepartmentRepo { get; set; }
        Task<int> Complete();
    }
}
