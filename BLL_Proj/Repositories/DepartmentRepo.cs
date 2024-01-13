using BLL_Proj.Interfaces;
using DAL_Proj.Contexts;
using DAL_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Repositories
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartment
    {
        public DepartmentRepo(AppDbContext _context):base(_context) 
        {
            
        }


    }
}
