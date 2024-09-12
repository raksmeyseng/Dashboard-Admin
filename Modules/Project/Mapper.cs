using AutoMapper;

namespace ArchtistStudio.Modules.Project;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Project, ListProjectResponse>();
        CreateMap<Project, DetailProjectResponse>();
        CreateMap<InsertProjectRequest, Project>();
        CreateMap<Project, UpdateProjectRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        
    }
}
