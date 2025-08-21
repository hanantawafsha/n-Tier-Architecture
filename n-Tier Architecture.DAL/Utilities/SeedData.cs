using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Utilities
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DataSeeding()
        {
            if(_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if(_context.Categories.Any())
            {
                _context.Categories.AddRange(
                    new Category
                    {
                        Name="Clothes"
                    },
                    new Category
                    {
                        Name = "Mobiles"
                    }
                    );

            }
            //if (_context.Brands.Any())
            //{
            //    _context.Brands.AddRange(
            //        new Brand
            //        {
            //            Name = "Adidas"
            //        },
            //        new Brand
            //        {
            //            Name = "Apple"
            //        },
            //        new Brand
            //        {
            //            Name = "Samsung"
            //        },
            //        new Brand
            //        {
            //            Name = "Nike"
            //        }
            //        );
            //}
            _context.SaveChanges();
        }
    }
}
