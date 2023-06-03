namespace PatchingModels;
public class PersonDto
{
	public Guid Id { get; set; }
	public string Username { get; set; } = default!;
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}