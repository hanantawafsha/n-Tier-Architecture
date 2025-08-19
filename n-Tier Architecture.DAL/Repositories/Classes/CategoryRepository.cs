using Microsoft.EntityFrameworkCore;
using n_Tier_Architecture.DAL.Data;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
        }

       

    }
}
