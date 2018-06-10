using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public class CompanyContext : DbContext, ICompanyContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<RootCoinValues> Coin { get; set; }
        public DbSet<RootCoin> RootCoin { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Company> Companys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddCompany("dbo");
            modelBuilder.AddRootCoin("dbo");
        }

    }
}
