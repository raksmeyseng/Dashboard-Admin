using AutoMapper;

namespace ArchtistStudio.Modules.History;

public class HistoryMapper : Profile
{
    public HistoryMapper()
    {
        CreateMap<History, ListHistoryResponse>();
        CreateMap<InsertHistoryRequest, History>();
        CreateMap<History, UpdateHistoryRequest>();
    }
}
