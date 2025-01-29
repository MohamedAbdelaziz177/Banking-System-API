using Banking_system.Enums.Transactions;

namespace Banking_system.Model
{
    public class Transaction
    {
        public int Id { get; set; }

        public int? FromAccountId { get; set; }

        public int? ToAccountId { get; set; }

        public decimal amount { get; set; }

        public TransactionType TrxType { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public Account? FromAccount { get; set; } 
        public Account? ToAccount { get; set; }

    }
}
