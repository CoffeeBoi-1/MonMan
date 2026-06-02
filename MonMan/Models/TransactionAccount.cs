
using System.ComponentModel.DataAnnotations.Schema;

namespace MonMan.Models;

[Table("TransactionAccount")]
internal class TransactionAccount
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSaving { get; set; }
    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}