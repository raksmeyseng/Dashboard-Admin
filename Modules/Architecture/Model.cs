
using ArchtistStudio.Modules.Project;
namespace ArchtistStudio.Modules.Architecture;

public class GetCategoryArchitectureByArchitectureResponse
{
	public Guid ProjectId { get; set; }
	public ListProjectResponse Project { get; set; } = null!;
	public bool Checked { get; set; }
}
public class ChangeArchitectureRequest
{
	public Guid ProjectId { get; set; }
	public Guid CategoryArchitectureId { get; set; }
	public bool Checked { get; set; }
}
