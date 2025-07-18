using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IAmBusy.Model.Models;

public class MainDbContext(DbContextOptions<MainDbContext> options) : IdentityDbContext<User, IdentityRole<int>,int>(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<UserTask> UserTasks { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ✅ 修复后：先调用基类配置扩展实体
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(a => a.UserTasks)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.UserName).IsUnique();
            entity.Property(u => u.UserName).HasMaxLength(256).IsRequired(false);
        });

        //modelBuilder.Entity<Department>()
        //    .HasMany(d => d.Users)
        //    .WithOne(d => d.Department)
        //    .HasForeignKey(d => d.DepartmentId);

    }
}

