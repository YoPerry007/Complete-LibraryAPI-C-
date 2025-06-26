using libraryAPI.Contracts.Response;
using libraryAPI.Models;
using LibraryAPI.Contracts.Response;
using LibraryAPI.Models;
using Mapster;

namespace LibraryAPI.Mapping;

public class LibraryMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Author, AuthorResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Books, src => src.Books
            .Adapt<List<BookResponse>>());

        config.NewConfig<Book, BookResponse>()
            .Map(dest => dest.AuthorName, src => src.Author!.Name);


    }
}
