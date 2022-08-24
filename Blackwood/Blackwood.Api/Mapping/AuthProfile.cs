using AutoMapper;
using Blackwood.Api.Schemas.Auth;
using Blackwood.Domain;

namespace Blackwood.Api.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<Auth, AuthSchema>();
        CreateMap<AuthSignUpSchema, User>();
        CreateMap<User, AuthNotificationSchema>();
    }
}