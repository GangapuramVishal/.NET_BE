
using Application;
using Infrastructure;
using WebApi.Middlewares;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAppilactionServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //----- redis 
            var cacheSettings = builder.Services.GetCacheSettings(builder.Configuration);

            //Configure redis
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettings.DistinationUrl;
            });
            //----redis
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt => opt.DisplayRequestDuration());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
