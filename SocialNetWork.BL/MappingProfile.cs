using AutoMapper;
using SocialNetWork.BL.ViewModels.Account;
using SocialNetWork.DAL.Models.Users;
using System;

namespace SocialNetWork.BL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login))
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.About, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                ;

            CreateMap<LoginViewModel, User>().ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));

            CreateMap<UserEditViewModel, User>();

            CreateMap<User, UserEditViewModel>().ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id));

            CreateMap<UserWithFriendExt, User>();

            CreateMap<User, UserWithFriendExt>();
        }
    }
}
