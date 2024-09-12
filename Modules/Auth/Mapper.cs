using AutoMapper;

namespace ArchtistStudio.Modules.Auth;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<InsertLoginRequest, Auth>();
        CreateMap<InsertRegisterRequest, Auth>();
    }
}
