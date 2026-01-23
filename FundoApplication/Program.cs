using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.Repository;
using DataLogicLayer.Data;
using DataLogicLayer.Interface;
using DataLogicLayer.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace FundoApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // ✅ Swagger Configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection
            builder.Services.AddScoped<IUserBL, UserBL>();
            builder.Services.AddScoped<IUserDL, UserDl>();

            builder.Services.AddAutoMapper(typeof(UserProfile));
           //  builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
