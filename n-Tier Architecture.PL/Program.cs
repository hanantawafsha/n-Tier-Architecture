
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.BLL.Services.Classes;
using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.BLL.Services.Utilities;
using NTierArchitecture.DAL.Data;
using NTierArchitecture.DAL.Models;
using NTierArchitecture.DAL.Repositories.Classes;
using NTierArchitecture.DAL.Repositories.Interfaces;
using NTierArchitecture.DAL.Utilities;
using NTierArchitecture.PL.Utilities;
using Scalar;
using Scalar.AspNetCore;
using Stripe;
using Stripe.Climate;
using System.Text;
//using MyFileService = NTierArchitecture.BLL.Services.Classes.FileService;
//using MyProductService = NTierArchitecture.BLL.Services.Classes.ProductService;
namespace NTierArchitecture.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //allow any
            var userPolicy = "";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: userPolicy, policy =>
                {
                    policy.AllowAnyOrigin();
                });
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 8;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.User.RequireUniqueEmail = true;

                Options.SignIn.RequireConfirmedEmail=true;
                Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                Options.Lockout.MaxFailedAccessAttempts = 5;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //repository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICheckOutRepository, CheckOutRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

            //services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            //builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IProductService, NTierArchitecture.BLL.Services.Classes.ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICheckOutService, CheckOutService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, NTierArchitecture.BLL.Services.Classes.OrderService>();
            builder.Services.AddScoped<IReviewService, BLL.Services.Classes.ReviewService>();

            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmailSender, EmailSetting>();
            builder.Services.AddScoped<IFileService, NTierArchitecture.BLL.Services.Classes.FileService>();
            builder.Services.AddScoped<ReportService>();

            // builder.Services.AddScoped<IFileService, FileService>();







            //jwt service
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwtOptions")["SecretKey"]))
            };
        });

            //stripe service
            // Configure Stripe settings
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];



            var app = builder.Build();

           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            //seed data
            var scope = app.Services.CreateScope();
            var objectOfSeedData=scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objectOfSeedData.DataSeedingAsync();
            await objectOfSeedData.IdentityDataSeedingAsync();


            app.UseHttpsRedirection();
            app.UseAuthentication();
            //add policy
            app.UseCors(userPolicy);
            app.UseAuthorization();
            // static url - images
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
