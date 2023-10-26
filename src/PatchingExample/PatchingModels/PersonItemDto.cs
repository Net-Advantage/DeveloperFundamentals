namespace PatchingModels;

public partial class PersonItemDto
{
	public Guid Id { get; set; }
	public required string Username { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}