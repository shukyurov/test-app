using Newtonsoft.Json;
using TestApp.Domain.Entities;

namespace TestApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string accountDataFileName = "accountData.json";

        public AccountRepository()
        {
            CheckExistData();
        }

        public IEnumerable<Account> GetAccounts()
        {
            using (StreamReader streamReader = new StreamReader(accountDataFileName))
            {
                string json = streamReader.ReadToEnd();
                List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);

                return accounts;
            }
        }

        public async Task SaveAccountData(List<Account> accounts)
        {
            using (var streamWriter = new StreamWriter(accountDataFileName))
            {
                await streamWriter.WriteAsync(JsonConvert.SerializeObject(accounts));
            }
        }

        private async void CheckExistData()
        {
            var baseExist = File.Exists(accountDataFileName);

            if (!baseExist)
            {
                using (var streamWriter = new StreamWriter(accountDataFileName))
                {
                    await streamWriter.WriteAsync("");
                }
            }
        }
    }
}