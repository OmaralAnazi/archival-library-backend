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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Add custom logic:
        // ...
        // ...
    }
}