using AutoMapper;
using Blackwood.Core.Services.Contracts.Responses;
using Blackwood.Web.Contracts.Responses;
using HashidsNet;

namespace Blackwood.Web.Mapping;

public class MappingProfile : Profile
{
    private readonly IHashids _hashids;

    public MappingProfile(IHashids hashids)
    {
        _hashids = hashids;
        RequestsMap();
        ResponsesMap();
    }

    private void RequestsMap()
    {
        CreateMap<string, long?>().ConvertUsing(hashId => _hashids.DecodeLong(hashId).FirstOrDefault());
    }

    private void ResponsesMap()
    {
        CreateMap<CreateSessionServiceResponse, AuthWebResponse>()
            .ForMember(webResponse => webResponse.SessionId,
                options => options.MapFrom(serviceResponse => _hashids.EncodeLong(serviceResponse.SessionId)));
    }
}