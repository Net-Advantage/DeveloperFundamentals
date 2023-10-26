namespace PatchingModels;

public partial class BookDto
{
	public int Id { get; set; }
	public Guid PersonId { get; set; }
	public required string Name { get; set; }
}