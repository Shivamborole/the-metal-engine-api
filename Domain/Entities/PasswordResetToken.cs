namespace InvoicingAPI.Domain.Entities
{
    public class PasswordResetToken
    {
         public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }       // FIXED (changed from int)

        public string Token { get; set; } = string.Empty;

        public DateTime Expiry { get; set; }

        public bool IsUsed { get; set; }




    }
}
