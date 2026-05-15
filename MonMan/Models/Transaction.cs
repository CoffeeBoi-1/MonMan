
namespace MonMan.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal Summary { get; set; }
        public DateTime Timestamp { get; set; }
        public int TransactionAccountId { get; set; }
        public TransactionAccount TransactionAccount { get; set; } = null!;
    }
}
