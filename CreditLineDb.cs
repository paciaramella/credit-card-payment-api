using Microsoft.EntityFrameworkCore;

class CreditLineDb : DbContext
{
    public CreditLineDb(DbContextOptions<CreditLineDb> options)
        : base(options) { }

    public DbSet<CreditLine> CreditLines => Set<CreditLine>();
}