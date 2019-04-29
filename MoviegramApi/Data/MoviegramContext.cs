using Microsoft.EntityFrameworkCore;
using MoviegramApi.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.Data
{
    /// <summary>
    /// EF Core database context.
    /// We are using a code first approach, therefore objects and relations are defined here and in the DataModels folder
    /// Everything in the Migrations folder is auto generated
    /// Migrations are created with 'add-migration [name]'
    /// Migrations can be applied with 'update-database'
    /// Alternatively, sql upgrade code can be generated wtih 'Script-Migration -From [migration]'
    /// </summary>
    public class MoviegramContext : DbContext
    {
        public MoviegramContext(DbContextOptions<MoviegramContext> options) : base(options)
        {        
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().ToTable("Movies");
        }
    }
}
