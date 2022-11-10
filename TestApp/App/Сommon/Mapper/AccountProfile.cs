using AutoMapper;
using TestApp.App.Accounts.Requests;
using TestApp.Domain.Entities;

namespace TestApp.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountRequest, Account>();
            CreateMap<EditAccountRequest, Account>();
        }
    }
}
