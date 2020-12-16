using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PullEvikeSpecials.Models;
using System;

namespace PullEvikeSpecials.Db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        { }

        public DbSet<Special> Specials { get; set; }
        public DbQuery<SpecialsGrouped> SpecialsGrouped { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Special>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Query<SpecialsGrouped>().ToView("SpecialsGrouped");
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new Context(optionsBuilder.Options);
        }
    }
}