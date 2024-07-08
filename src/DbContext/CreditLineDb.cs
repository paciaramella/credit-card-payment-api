using Microsoft.EntityFrameworkCore;

public class CreditLineDbContext : DbContext
{
    public CreditLineDbContext(DbContextOptions<CreditLineDbContext> options)
        : base(options)
    {
    }

    public DbSet<CreditLine> CreditLines { get; set; }
}