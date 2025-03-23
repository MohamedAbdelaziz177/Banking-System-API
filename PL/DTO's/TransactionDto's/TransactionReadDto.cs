namespace Banking_system.DTO_s.TransactionDto_s
{
    public class TransactionReadDto
    {
        public int Id { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string TrxType { get; set; }
        public decimal amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
