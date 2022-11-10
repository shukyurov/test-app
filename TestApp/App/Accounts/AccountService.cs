using AutoMapper;
using TestApp.App.Accounts.Requests;
using TestApp.Domain.Entities;
using TestApp.Repositories;

namespace TestApp.App.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void CreateAccount(CreateAccountRequest accountDto)
        {
            var createAccount = _mapper.Map<Account>(accountDto);

            var accounts = _accountRepository.GetAccounts().ToList();

            if (accounts is null)
            {
                accounts = new List<Account>();
            }

            createAccount.Id = !accounts.Any() ? 0 : accounts.Max(_ => _.Id) + 1;

            accounts.Add(createAccount);

            _accountRepository.SaveAccountData(accounts);
        }

        public bool EditAccount(EditAccountRequest accountDto)
        {
            var editAccount = _mapper.Map<Account>(accountDto);

            var accounts = _accountRepository.GetAccounts().ToList();

            var account = accounts?.FirstOrDefault(u => u.Id == editAccount.Id);

            if (accounts is null || account is null)
            {
                return false;
            }

            accounts.Remove(account);
            accounts.Add(FillFields(editAccount, account));
            accounts = accounts.OrderBy(_ => _.Id).ToList();

            _accountRepository.SaveAccountData(accounts);

            return true;
        }

        public bool DeleteAccount(int id)
        {
            var accounts = _accountRepository.GetAccounts().ToList();

            var account = accounts?.FirstOrDefault(u => u.Id == id);

            if (accounts is null || account is null)
            {
                return false;
            }

            accounts.Remove(account);

            _accountRepository.SaveAccountData(accounts);

            return true;
        }

        private Account FillFields(Account accountModel, Account dbModel)
        {
            dbModel.FirstName = accountModel.FirstName is not null ? accountModel.FirstName : dbModel.FirstName;
            dbModel.LastName = accountModel.LastName is not null ? accountModel.LastName : dbModel.LastName;
            dbModel.Email = accountModel.Email is not null ? accountModel.Email : dbModel.Email;

            return dbModel;
        }
    }
}
