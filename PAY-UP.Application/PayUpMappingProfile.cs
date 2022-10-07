using AutoMapper;
using PAY_UP.Application.Dtos;
using PAY_UP.Application.Dtos.Authentication;
using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Creditors;

namespace PAY_UP.Application
{
    public class PayUpMappingProfile : Profile
    {
        public PayUpMappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<CreateUserDto, AppUser>();
            CreateMap<UpdateUserDto, AppUser>();
            CreateMap<AppUser, GetUserDto>()
                .ForMember(src => src.Fullname, dest => dest.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<AppUser, LoginResponseDto>()
            .ForMember(src => src.Fullname, dest => dest.MapFrom(x => $"{x.FirstName} {x.LastName}"));

            CreateMap<CreateCreditorDto, Creditor>();
            CreateMap<UpdateCreditorDto, Creditor>();
            CreateMap<Creditor, GetCreditorDto>();
        }
    }
}
