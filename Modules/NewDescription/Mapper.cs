using AutoMapper;

namespace ArchtistStudio.Modules.NewDescription;

public class NewDescriptionMapper : Profile
{
    public NewDescriptionMapper()
    {
        CreateMap<NewDescription, ListNewDescriptionResponse>();
        CreateMap<InsertNewDescriptionRequest, NewDescription>();
        CreateMap<NewDescription, UpdateNewDescriptionRequest>();
    }
}
