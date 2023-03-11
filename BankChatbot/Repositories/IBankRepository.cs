using BankChatbot.Model;

namespace BankChatbot.Repositories
{
    public interface IBankRepository
    {
        Task<Complain> AddComplainAsync(Complain comp);
        Task<Fraud> AddFraudAsync(Fraud fraud);
        Task<Bank> GetBank(int id);
        Task<Bank> Login(string username, string password);

        Task<Fraud> UpdateFraudAsync(int id);

        Task<Complain> UpdateComplaintAsync(int id);

    }
}
