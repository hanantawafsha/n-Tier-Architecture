using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges();
        }

        public int Delete(Category category)
        {
            _context.Categories.Remove(category);
            return _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool withTracking = false)
        {
            if(withTracking)
            return _context.Categories.ToList();
            return _context.Categories.AsNoTracking().ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public int Update(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChanges();
        }
        //public bool ToggleStatus(int id)
        //{
        //    throw new NotImplementedException();

        //}

    }
}
