using Microsoft.EntityFrameworkCore;
using ParaTest.Models.Cityzen;
using NLog;
using NLog.Web;

namespace ParaTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var NLogger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddDbContext<CityzenDbContext>(opts =>
                    opts.UseSqlServer(builder.Configuration["DbConnString"]));
                builder.Services.AddScoped<CityzenDataProvider, CityzenMssqlData>();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                app.UseSwagger();
                app.UseSwaggerUI();


                //app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}