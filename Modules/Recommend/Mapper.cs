using AutoMapper;

namespace ArchtistStudio.Modules.Recommend;

public class RecommendMapper : Profile
{
    public RecommendMapper()
    {
        CreateMap<Recommend, ListRecommendResponse>();
        CreateMap<InsertRecommendRequest, Recommend>();
    }
}
