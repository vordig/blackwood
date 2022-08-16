using AutoMapper;
using Blackwood.Api.Schemas;
using Blackwood.Domain;

namespace Blackwood.Api.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookListItemSchema>()
            .ForMember(schema => schema.UpdatedAt, options => 
                options.MapFrom(source => source.Audit.UpdatedAt));
    }
}