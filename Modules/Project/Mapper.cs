using AutoMapper;

namespace ArchtistStudio.Modules.Project;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Project, ListProjectResponse>();
        CreateMap<InsertProjectRequest, Project>();
        CreateMap<Project, UpdateProjectRequest>();
        
    }
}
