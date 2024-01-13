using DAL_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Interfaces
{
    public interface IGenericRepo<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        void Delete(T item);
        Task Add(T item);
        void Update(T item);

    }
}
