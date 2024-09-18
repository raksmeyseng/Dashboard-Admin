using AutoMapper;

namespace ArchtistStudio.Modules.Search;

public class SearchMapper : Profile
{
    public SearchMapper()
    {
        CreateMap<Search, GetSearchByProjectResponse>();
    }
}
