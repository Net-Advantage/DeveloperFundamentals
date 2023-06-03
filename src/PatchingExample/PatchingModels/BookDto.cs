namespace PatchingModels;

public class BookDto
{
	public int Id { get; set; }
	public Guid PersonId { get; set; }
	public required string Name { get; set; }
}