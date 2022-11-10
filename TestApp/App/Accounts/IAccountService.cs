using TestApp.App.Accounts.Requests;
using TestApp.Domain.Entities;

namespace TestApp.App.Accounts
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void CreateAccount(CreateAccountRequest accountDto);
        bool EditAccount(EditAccountRequest accountDto);
        bool DeleteAccount(int id);
    }
}
