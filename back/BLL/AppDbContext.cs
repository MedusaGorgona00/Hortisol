using DTO.States;
using Microsoft.EntityFrameworkCore;

namespace BLL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region DbSet
    public DbSet<WashingtonDto.Total> WashingtonTotal { get; set; }

    public DbSet<WashingtonDto.ByGrade> WashingtonByGrade { get; set; }

    public DbSet<WashingtonDto.ByCompany> WashingtonByCompany { get; set; }

    public DbSet<IllinoisDto.SmallPackage> IllinoisSmallPackage { get; set; }

    public DbSet<IllinoisDto.LargePackage> IllinoisLargePackage { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<WashingtonDto.Total>().HasNoKey();
        builder.Entity<WashingtonDto.ByGrade>().HasNoKey();
        builder.Entity<WashingtonDto.ByCompany>().HasNoKey();
        builder.Entity<IllinoisDto.SmallPackage>().HasNoKey();
        builder.Entity<IllinoisDto.LargePackage>().HasNoKey();

        base.OnModelCreating(builder);
    }
}
