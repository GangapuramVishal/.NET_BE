using Di_LifeTime;
using Di_LifeTime.Interfaces;
using Di_LifeTime.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using static Di_LifeTime.Services.Scoped;

namespace Di_LifeTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ISingleton, Singleton>();
            builder.Services.AddTransient<ITransient, Transient>();
            builder.Services.AddScoped<IScoped, Scoped>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
