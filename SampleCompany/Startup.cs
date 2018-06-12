using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository.Context;
using SampleCompany.Infra;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleCompany
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddDbContext<CompanyContext>(options =>
               options.UseInMemoryDatabase(databaseName: "WebAppDB")
           );
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICnpjService, CnpjService>();
            services.AddTransient<ICoinTypeSerrvice, CoinTypeService>();

            services.AddMvc();
            // Ativando o uso de cache em memória
            services.AddMemoryCache();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info { Title = "APICompany", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/Swagger.json", "APICompany v1");
            });
        }

        #region Aux

        private static DbContextOptions<CompanyContext> GetConnectionOptions()
        {
            SqliteConnection connection = GetConnection();
            DbContextOptions<CompanyContext> options = GetOptions(connection);
            return options;
        }

        private static DbContextOptions<CompanyContext> GetOptions(SqliteConnection connection)
        {
            return new DbContextOptionsBuilder<CompanyContext>()
                     .UseSqlite(connection)
                     .Options;
        }

        private static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return connection;
        }

        #endregion
    }
}
