using Microsoft.EntityFrameworkCore;
using MoviegramApi.DataModels;

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
        /// <summary>
        /// Contructor required in order to pass options to base class
        /// </summary>
        /// <param name="options">context options</param>
        public MoviegramContext(DbContextOptions<MoviegramContext> options) : base(options)
        {        
        }

        /// <summary>
        /// DbSet of movies in the database
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Actions to perform on model building, such as mapping data classes to tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().ToTable("Movies");
        }
    }
}
