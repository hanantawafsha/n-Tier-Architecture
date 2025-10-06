﻿using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DAL.Data;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
        }

       

    }
}
