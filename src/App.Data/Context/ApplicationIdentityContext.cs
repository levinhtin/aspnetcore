
using Microsoft.EntityFrameworkCore;
using App.Data.Models;
using App.Data.Entities.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Data.Context
{
    //public class ApplicationDbContext : OpenIddictContext<ApplicationUser>
    //{
    //    public ApplicationDbContext(DbContextOptions options)
    //        : base(options)
    //    {
    //    }
    //}
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=AspNetCore-Identity;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

