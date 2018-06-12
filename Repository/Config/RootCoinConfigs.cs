using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Config
{
    public static class RootCoinConfigs
    {
        public static void AddRootCoin(this ModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Entity<RootCoin>(entity =>
            {
                entity.HasOne<RootCoinValues>();
            });
        }

    }
}
