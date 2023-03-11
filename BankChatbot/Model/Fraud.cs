using System.ComponentModel.DataAnnotations;

namespace BankChatbot.Model
{
    public class Fraud
    {
        [Key]
        public int FraudId { get; set; }
        public string Email { get; set; }

        public string FraudType { get; set; }
        public int FraudAmount { get; set; }
        public DateTime FraudDate { get; set; }
        public string Details { get; set; }

        public string? ReferenceId { get; set; }
    }
}
