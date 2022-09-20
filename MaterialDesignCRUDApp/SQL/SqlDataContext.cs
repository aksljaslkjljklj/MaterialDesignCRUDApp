using MaterialDesignCRUDApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.SQL
{
    public class SqlDataContext:DbContext
    {
        private readonly SqlConnectionStringBuilder _builder;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public SqlDataContext()
        {
            _builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "ShopDB",
                IntegratedSecurity = true
            };
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_builder.ConnectionString);
        }
    }
}
