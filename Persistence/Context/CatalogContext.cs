using Application.Interfaces;
using Domain.Entities.Catalog;
using Domain.Entities.OrderAggregate;
using Domain.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context
{
    public class CatalogContext : DbContext, ICatalogContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Published> Publisheds { get; set; }
        public CatalogContext() { }
        public CatalogContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlite(@"DataSource=c:\CSharp\ShowMe\DB\show-me.db");
                //optionsBuilder
                //    .UseNpgsql("Host=localhost;Port=5432;Database=zion;Username=cpux86;Password=1AC290066F");
            }
            
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
            //Console.Clear();
            optionsBuilder.LogTo(message => Console.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new MetaConfiguration());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }


    }
    
}
