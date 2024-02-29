namespace BACampusApp.Business.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminCreateDto, Admin>();
            CreateMap<Admin, AdminCreateDto>();
            CreateMap<Admin, AdminDetailsDto>();
            CreateMap<AdminCurrentUserUpdateDto, Admin>();
            CreateMap<AdminLoggedInUserUpdateDto, Admin>();
            CreateMap<Admin, AdminUpdateDto>().ReverseMap();
            CreateMap<Admin, AdminDto>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Admin, AdminListDto>().ReverseMap();
            CreateMap<Admin, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<AdminUpdateDto, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<AdminCreateDto, IdentityUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            
        }
    }
}
