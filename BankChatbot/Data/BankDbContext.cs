using BankChatbot.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankChatbot.Data
{
    public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions options):base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Fraud> Frauds { get; set; }
        public DbSet<Bank> Banks { get; set; }

    }
    }

