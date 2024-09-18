
using ArchtistStudio.Modules.Project;

namespace ArchtistStudio.Modules.Engineeing;

public class GetCategoryEngineeringByEngineeingResponse
{
	public Guid ProjectId { get; set; }

	public ListProjectResponse Project { get; set; } = null!;
	public bool Checked { get; set; }
}
public class ChangeEngineeingRequest
{
	public Guid ProjectId { get; set; }
	public Guid CategoryEngineeringId { get; set; }
	public bool Checked { get; set; }
}