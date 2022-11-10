using TestApp.App.Accounts;
using TestApp.Repositories;

namespace TestApp.App
{
    public static class AppDependencies
    {
       public static void AddAppDependencies(this IServiceCollection services)
       {
            services.AddScoped<IAccountService, AccountService>();
       }
    }
}
