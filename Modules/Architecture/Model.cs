
namespace ArchtistStudio.Modules.Architecture;

public class GetCategoryByArchitectureResponse
{
	public Guid ProjectId { get; set; }
	public Project.Project Project { get; set; } = null!;

	public bool Checked { get; set; }
}
public class ChangeArchitectureRequest
{
	public Guid ProjectId { get; set; }
	public Guid CategoryId { get; set; }
	public bool Checked { get; set; }
}
