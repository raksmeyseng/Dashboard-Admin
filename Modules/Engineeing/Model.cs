
namespace ArchtistStudio.Modules.Engineeing;

public class GetCategoryByEngineeingResponse
{
	public Guid ProjectId { get; set; }

	public Project.Project Project { get; set; } = null!;
	public bool Checked { get; set; }
}
public class ChangeEngineeingRequest
{
	public Guid ProjectId { get; set; }
	public Guid CategoryId { get; set; }
	public bool Checked { get; set; }
}