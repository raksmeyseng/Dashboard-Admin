
namespace ArchtistStudio.Modules.Recommend;

public class ListRecommendResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool? InActive { get; set; }
}

public class InsertRecommendRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool? InActive { get; set; }
}
