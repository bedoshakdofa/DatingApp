using AutoMapper;
using DatingApp.Data.Models;
using DatingApp.DTOs;
namespace DatingApp.Helpers
{
    public class AutoMapperProfilies:Profile
    {
        public AutoMapperProfilies()
        {
           CreateMap<User,MemberDto>();
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDto, User>();
        }
    }
}
