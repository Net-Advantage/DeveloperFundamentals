namespace PatchingPersistence;

public class Book
{
	public Guid Id { get; set; }
	public required string Name { get; set; }

	public Guid PersonId { get; set; }
	public Person? Person { get; set; }
}