using BLL_Proj.Interfaces;
using DAL_Proj.Contexts;
using DAL_Proj.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Proj.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            await _context.AddAsync(item);
        }

        public void Delete(T item)
        {
            _context.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
               return (IEnumerable<T>)await _context.Employees.Include(E => E.Department).ToListAsync();
            }
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        => await _context.Set<T>().FindAsync(id);

        public void Update(T item)
        {
             _context.Update(item);
        }
    }
}
