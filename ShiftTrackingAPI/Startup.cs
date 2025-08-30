using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShiftTrackingAPI.Helpers;
using ShiftTrackingAPI.Helpers.SQL;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace ShiftTrackingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=ShiftTracking.db"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShiftTrackingAPI", Version = "v1" });
                c.UseAllOfToExtendReferenceSchemas();
                c.SchemaFilter<JsonConverterDisplayName>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            try
            {
                // ѕровер€ем применена ли уже перва€ миграци€
                var initialMigrationApplied = CheckIfInitialMigrationApplied(context);

                if (initialMigrationApplied)
                {
                    // ≈сли перва€ миграци€ уже прокатана - прокатываем только последующие миграции
                    ApplySubsequentMigrations(context);
                }
                else
                {
                    // ≈сли перав€ миграци€ не применена - примен€ем все миграции
                    context.Database.Migrate();
                }
                ///генерируем данные
                DataGenerator.Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ќшибка миграции: {ex.Message}");
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShiftTrackingAPI v1"));
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<HandleException>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// дл€ безопасности структуры бд смотрим есть ли в таблице миграций перва€ миграци€
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool CheckIfInitialMigrationApplied(AppDbContext context)
        {
            try
            {
                var connection = context.Database.GetDbConnection();
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20250818121844_Initial'";
                return command.ExecuteScalar() != null;
            }
            catch
            {
                return false;
            }
        }

        private void ApplySubsequentMigrations(AppDbContext context)
        {
            var pendingMigrations = context.Database.GetPendingMigrations()
                .Where(m => m != "20250818121844_Initial")
                .ToList();

            if (pendingMigrations.Any())
            {
                var migrator = context.Database.GetService<IMigrator>();
                foreach (var migration in pendingMigrations)
                {
                    migrator.Migrate(migration);
                }
            }
        }
    }
}
