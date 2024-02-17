using AutoMapper;
using AzaRRoide.Infra.Data.Context;
using AzaRRoide.Infra.Data.Mapper;
using AzaRRoide.Interfaces;
using AzaRRoide.Services;
using AzaRRoide.Web;
using ikvm.runtime;
using Microsoft.EntityFrameworkCore;
using Refit;

namespace AzaRRoide.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EmpresaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
            });
            builder.Services.AddAutoMapper(typeof(EmpresaProfile));

            builder.Services.AddRefitClient<IEmpresaConsumoApiRefit>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://www.receitaws.com.br");
                });

            builder.Services.AddScoped<IEmpresaIntegracao, EmpresaIntegracao>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}









//using AzaRRoide.Infra.Data.Context;
//using AzaRRoide.Infra.Data.Mapper;
//using AzaRRoide.Interfaces;
//using AzaRRoide.Services;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Refit;

//namespace AzaRRoide.API
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            builder.Services.AddDbContext<EmpresaDbContext>(options =>
//            {
//                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
//            });
            
//            builder.Services.AddRefitClient<IEmpresaConsumoApiRefit>()
//    .ConfigureHttpClient(c =>
//    {
//        c.BaseAddress = new Uri("https://www.receitaws.com.br");
//    });

//            builder.Services.AddScoped<IEmpresaIntegracao, EmpresaIntegracao>();
//            builder.Services.AddAutoMapper(typeof(Startup));
//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}