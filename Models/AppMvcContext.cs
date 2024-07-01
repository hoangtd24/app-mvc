using aspnetcoremvc.Models.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using aspnetcoremvc.Models.Blog;

namespace aspnetcoremvc.Models;

public class AppMvcContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public AppMvcContext(DbContextOptions<AppMvcContext> options) : base(options)
    {

    }

    public DbSet<ContactModel> Contacts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Bỏ tiền tố AspNet của các bảng: mặc định
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(p => p.Slug).IsUnique();
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(p => p.Slug).IsUnique();
        });

        modelBuilder.Entity<PostCategory>(entity => {
            entity.HasKey(p => new {p.PostID, p.CategoryID});
        });
    }

    public DbSet<Category> Categories { get; set; } = default!;

    public DbSet<Post> Posts { set; get; }

    public DbSet<PostCategory> PostCategories { set; get; }
}