using BankChatbot.Data;
using BankChatbot.Model;
using Microsoft.EntityFrameworkCore;

namespace BankChatbot.Repositories
{
    public class BankRepository:IBankRepository
    {
        private readonly BankDbContext complainDbContext;
        //private readonly FraudDbContext fraudDbContext;
        public BankRepository(BankDbContext _complainDbContext)
        {
            complainDbContext = _complainDbContext;
            //this.fraudDbContext = fraudDbContext;
        }
        public async Task<Complain> AddComplainAsync(Complain comp)
        {
            await complainDbContext.AddAsync(comp);
            await complainDbContext.SaveChangesAsync();
            var date = comp.ComplainDate;
            var email = comp.Email;
            var complainBody = await complainDbContext.Complains.FirstOrDefaultAsync(x => (x.ComplainDate == date && x.Email == email));
            var result = await UpdateComplaintAsync(complainBody.ComplainId);
            return result;
        }

        public async Task<Complain> UpdateComplaintAsync(int id)
        {
            var result = await complainDbContext.Complains.LastOrDefaultAsync(x => x.ComplainId == id);
            if (result == null)
            {
                return null;
            }
            else
            {
                result.ReferenceId = "ZF1800C" + id;
                //result.RoleId = employee.RoleId;
                complainDbContext.Update(result);
                await complainDbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Fraud> UpdateFraudAsync(int id)
        {
            var result = await complainDbContext.Frauds.FirstOrDefaultAsync(x => x.FraudId == id);
            if (result == null)
            {
                return null;
            }
            else
            {
                result.ReferenceId = "ZF1800F" + id;
                //result.RoleId = employee.RoleId;
                complainDbContext.Update(result);
                await complainDbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Fraud> AddFraudAsync(Fraud fraud)
        {
            await complainDbContext.AddAsync(fraud);
            await complainDbContext.SaveChangesAsync();
            var date = fraud.FraudDate;
            var email=fraud.Email;
            var fraudBody = await complainDbContext.Frauds.FirstOrDefaultAsync(x => (x.FraudDate==date&&x.Email==email));
            var result = await UpdateFraudAsync(fraudBody.FraudId);
            return result;
        }

        public async Task<Bank> GetBank(int id)
        {
            return await complainDbContext.Banks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Bank> Login(string username, string password)
        {
            return complainDbContext.Banks.FirstOrDefault(x => x.UserName == username && x.Password == password);
        }

    }
}
