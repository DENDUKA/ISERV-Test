using ISERV.Persistence.EF.Entities;
using ISERV.Persistence.EF.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace ISERV.UI.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var databaseSettings = _configuration.GetSection("Database").Get<DatabaseSettings>();

            services.AddSingleton(databaseSettings);
            services.AddScoped<ApplicationContext>();
            services.AddScoped<UniversitiesRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}