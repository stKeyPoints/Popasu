using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata;
using Popasu.Domain.Model;

namespace Popasu.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Climate> Climates { get; set; }
        public DbSet<Domain.Model.Parameter> Parameters { get; set; }
        public DbSet<ParameterValue> ParametersValues { get; set; }
        public DbSet<WindRose> WindRoses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
