using Banking_system.Enums.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_system.Model
{
    public class Transaction
    {
        public int Id { get; set; }


        public int? FromAccountId { get; set; }

        [ForeignKey("FromAccountId")]
        public Account? FromAccount { get; set; }

        public int? ToAccountId { get; set; }


        [ForeignKey("ToAccountId")]
        public Account? ToAccount { get; set; }

        public decimal amount { get; set; }

        public TransactionType TrxType { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

       
      

    }
}
