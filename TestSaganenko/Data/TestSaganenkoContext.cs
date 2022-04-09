using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSaganenko.Models;

namespace TestSaganenko.Data
{
    public class TestSaganenkoContext : IdentityDbContext<User>
    {
        public TestSaganenkoContext(DbContextOptions<TestSaganenkoContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("Post").HasOne(e => e._user).WithMany(e => e.Posts).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Topic>().ToTable("Topic").HasOne(e => e._user).WithMany(e => e.Topics).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>().ToTable("Category").HasMany(e => e.Topics).WithOne(e => e._category).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
