using TestApp.Domain.Entities;

namespace TestApp.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
        Task SaveAccountData(List<Account> accounts);
    }
}