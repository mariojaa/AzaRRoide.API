
using AzaRRoide.Application.Interfaces;
using AzaRRoide.Application.Services;
using AzaRRoide.Domain.Interfaces;
using AzaRRoide.Infra.Data.Repositories;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}