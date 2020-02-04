using AutoMapper;
using BLL.DTOs;
using DAL.Models.Entitys;

namespace BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();
            //CreateMap<AccountDTO, Account>();

            CreateMap<Contact, ContactDTO>().ReverseMap();
            //CreateMap<ContactDTO, Contact>();
        }
    }
}
