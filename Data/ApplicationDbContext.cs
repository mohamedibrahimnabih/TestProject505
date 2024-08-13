using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.Models.ViewModel;

namespace Project1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                .Build();

            var connection = builder.GetConnectionString("DefaultConnetion");

            optionsBuilder.UseSqlServer(connection);
        }
        public DbSet<Project1.Models.ViewModel.ApplicationUserVM> ApplicationUserVM { get; set; } = default!;
        public DbSet<Project1.Models.ViewModel.LoginVM> LoginVM { get; set; } = default!;
        public DbSet<Project1.Models.ViewModel.RoleVM> RoleVM { get; set; } = default!;
    }
}
