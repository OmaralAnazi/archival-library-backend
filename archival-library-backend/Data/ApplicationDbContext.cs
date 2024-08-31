using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using archival_library_backend.Entities;

namespace archival_library_backend.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<DocumentMetadata> DocumentMetadatas { get; set; } 
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Document Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "History" },
            new Category { Id = 2, Name = "Science and Technology" },
            new Category { Id = 3, Name = "Art and Culture" },
            new Category { Id = 4, Name = "Education and Research" },
            new Category { Id = 5, Name = "Preservation and Conservation" },
            new Category { Id = 6, Name = "Legal and Government" },
            new Category { Id = 7, Name = "Personal Collections" }
        );
    }
}