using TestApp.Repositories;

namespace TestApp.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAccountRepository, AccountRepository>();
        }
    }
}
