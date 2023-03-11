using System.ComponentModel.DataAnnotations;

namespace BankChatbot.Model
{
    public class Complain
    {
        [Key]
        public int ComplainId { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime ComplainDate { get; set; }
        public string Subject { get; set; }

        public string Complaint { get; set; }

        public string? ReferenceId { get; set; }


    }
}
