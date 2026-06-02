using MonMan.Models;
using Microsoft.EntityFrameworkCore;

namespace MonMan.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionAccount>().HasData(
                new TransactionAccount { Id = 1, Name = "Cashless", IsSaving = false },
                new TransactionAccount { Id = 2, Name = "Cash", IsSaving = false }
                );
        }

        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionAccount> Accounts { get; set; } = null!;
    }
}
