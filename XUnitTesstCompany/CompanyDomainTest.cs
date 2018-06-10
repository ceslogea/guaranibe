using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesstCompany
{
    public class CompanyDomainTest
    {
        public CompanyDomainTest()
        {
           
        }

        #region OK

        /// <summary>
        /// Add a single element and check if theres only 1 element in memory database
        /// Role: Add Valid company BY CNPJ
        /// </summary>
        [Fact]
        public async Task Add()
        {
            Company newCompany = new Company("Teste", "32893956858", "teste.@teste.com", new Address("Araraquara", "Santana", "Jose Closel", "464", "14804-412", ""));
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            // Run the test against one instance of the context
            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                newCompany = await service.Add(newCompany);
                List<Company> result = service.GetAll().ToList();
                Assert.Single(result);
                Assert.Equal(newCompany.Id, result.Single().Id);
            }

        }

        /// <summary>
        /// list all
        /// Role: List all company
        /// </summary>
        [Fact]
        public async Task List()
        {
            DbContextOptions<CompanyContext> options = GetConnectionOptions();


            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                Company newCompany = await service.Add(new Company("Teste", "32893956858", "teste.@teste.com", new Address("Araraquara", "Santana", "Jose Closel", "464", "14804-412", "")));
                Company newCompany2 = await service.Add(new Company("Teste2", "328939568582", "teste.@teste.com", new Address("Araraquara", "Santana", "Jose Closel", "464", "14804-412", "")));
                IEnumerable<Company> result = service.GetAll();

                Assert.NotEmpty(result);
                Assert.Collection(result, item => Assert.Contains("Teste", item.Name),
                                item => Assert.Contains("Teste2", item.Name));
            }
        }

        #endregion

        #region Cannot be OK

        /// <summary>
        /// Add a single element null
        /// Role: Add invalid null company
        /// </summary>
        [Fact]
        public async Task AddInvalidNullCompany()
        {
            Company newCompany = null;
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                Exception ex = await Assert.ThrowsAnyAsync<Exception>(() => service.Add(newCompany));
            }

        }

        /// <summary>
        /// Add a single element blank
        /// Role: Add invalid blank company
        /// </summary>
        [Fact]
        public async Task AddInvalidBlankCompany()
        {

            Company newCompany = new Company();
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                Exception ex = await Assert.ThrowsAnyAsync<Exception>(() => service.Add(newCompany));
            }
        }

        /// <summary>
        /// Add invalid Company
        /// Role: Missing parammiter [email]
        /// </summary>
        [Fact]
        public async Task AddInvalidCompanyMissingEmail()
        {
            Company newCompany = new Company("Teste", "32893956858", null, new Address("Araraquara", "Santana", "Jose Closel", "464", "14804-412", ""))
            {
                Email = null
            };
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                Exception ex = await Assert.ThrowsAnyAsync<Exception>(() => service.Add(newCompany));
            }
        }

        /// <summary>
        /// Add invalid Company
        /// Role: Missing parammiter [Name]
        /// </summary>
        [Fact]
        public async Task AddInvalidCompanyMissingName()
        {
            Company newCompany = new Company();
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            // Run the test against one instance of the context
            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                Exception ex = await Assert.ThrowsAnyAsync<Exception>(() => service.Add(newCompany));
            }
        }

        #endregion

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
