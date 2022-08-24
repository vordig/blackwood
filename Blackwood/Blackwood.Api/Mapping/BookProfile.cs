using AutoMapper;
using Blackwood.Api.Schemas;
using Blackwood.Api.Schemas.Book;
using Blackwood.Domain;

namespace Blackwood.Api.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookUpdateSchema, Book>();
        CreateMap<BookNoteSchema, BookNote>();
        CreateMap<BookNote, BookNoteSchema>();
        
        CreateMap<Book, BookCompactSchema>()
            .ForMember(schema => schema.UpdatedAt, options => 
                options.MapFrom(source => source.Audit.UpdatedAt));
        
        CreateMap<Book, BookDetailedSchema>()
            .ForMember(schema => schema.CreatedAt, options => 
                options.MapFrom(source => source.Audit.CreatedAt))
            .ForMember(schema => schema.UpdatedAt, options => 
                options.MapFrom(source => source.Audit.UpdatedAt));
        
        CreateMap<Book, BookNotificationSchema>()
            .ForMember(schema => schema.Authors, options => 
                options.MapFrom(source => string.Join(", ", source.Authors)));
    }
}