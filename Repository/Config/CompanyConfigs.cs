using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Config
{
    public static class CompanyConfigs
    {
        public static void AddCompany(this ModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasOne<Address>();
                entity.HasOne<RootCoinValues>();
                entity.Property("Name").IsRequired();
                entity.Property("Email").IsRequired();
                entity.Property("CNPJ").IsRequired();
            });
        }

    }
}
