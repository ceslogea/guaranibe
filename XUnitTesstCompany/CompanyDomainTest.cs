using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
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

        private const string _JSON = @"{
    'nome': 'BANCO PAN S.A.',
    'cnpj': '59.285.411/0001-13',
    'email': 'teste@teste.com',
    'address': {
        'uf': 'SP',
        'bairro': 'BELA VISTA',
        'logradouro': 'AV PAULISTA',
        'numero': '1374',
        'cep': '01.310-100',
        'complemento': 'ANDAR 16',
    },
    'CurrentRootCoinValues': {
	        'code': 'USD',
	        'codein': 'BRL',
	        'name': 'Dólar Comercial',
	        'high': '3.8478',
	        'low': '3.7016',
	        'pctChange': '-5.586',
	        'open': '0',
	        'bid': '3.7059',
	        'ask': '3.7065',
	        'varBid': '-0.2193',
	        'timestamp': '1528487940000',
	        'create_date': '2018-06-08 17:10:03'
    }
}";

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
            DbContextOptions<CompanyContext> options = GetConnectionOptions();

            // Run the test against one instance of the context
            using (CompanyContext context = new CompanyContext(options))
            {
                context.Database.EnsureCreated();
                var service = new CompanyService(context);
                var company1 = JsonConvert.DeserializeObject<Company>(_JSON);
                company1 = await service.Add(company1);
                List<Company> result = service.GetAll().ToList();
                Assert.Single(result);
                Assert.Equal(company1.Id, result.Single().Id);
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
                var company1 = JsonConvert.DeserializeObject<Company>(_JSON);
                var company2 = JsonConvert.DeserializeObject<Company>(_JSON);
                Company newCompany = await service.Add(company1);
                Company newCompany2 = await service.Add(company2);
                IEnumerable<Company> result = service.GetAll();

                Assert.NotEmpty(result);
                Assert.Equal(2, result.Count());
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
