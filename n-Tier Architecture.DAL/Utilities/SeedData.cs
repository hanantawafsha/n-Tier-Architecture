using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            if(! await _context.Categories.AnyAsync())
            {
               await _context.Categories.AddRangeAsync(
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
            if (! await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Brand
                    {
                        Name = "Adidas",
                        MainImage= "medcor.png"
                    },
                    new Brand
                    {
                        Name = "Apple",
                        MainImage = "medcor.png",
                        


                    },
                    new Brand
                    {
                        Name = "Samsung",
                        MainImage= "QR-Code.png"

                    },
                    new Brand
                    {
                        Name = "Nike",
                        MainImage = "QR-Code.png"
                    }
                    );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(! await _roleManager.Roles.AnyAsync())
            {
              await  _roleManager.CreateAsync(
                    new IdentityRole("Admin"));
                await _roleManager.CreateAsync(
                    new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(
                    new IdentityRole("Customer"));
            }
            if (! await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "hananjtawafsha@gmail.com",
                    FullName = "Hanan Admin",
                    PhoneNumber = "0598458868",
                    UserName = "htawafshaAdmin",
                    EmailConfirmed = true,

                };
                var user2 = new ApplicationUser()
                {
                    Email = "hanantawafsha@gmail.com",
                    FullName = "Hanan Super Admin",
                    PhoneNumber = "0598458868",
                    UserName = "htawafshaSAdmin",
                    EmailConfirmed = true,

                };
                var user3 = new ApplicationUser()
                {
                    Email = "htawafsha@outlook.com",
                    FullName = "Hanan Custom",
                    PhoneNumber = "0598458868",
                    UserName = "htawafshacCustomer",
                    EmailConfirmed = true,

                };

                await _userManager.CreateAsync(user1,"Hanan@123" );
                await _userManager.CreateAsync(user2, "Hanan@123");
                await _userManager.CreateAsync(user3, "Hanan@123");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");


            }
            await _context.SaveChangesAsync();
        }


        
    }
}
