namespace PatchingPersistence;
public class Person
{
	public Guid Id { get; set; }
	public required string Username { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public ICollection<Book>? Books { get; set; }
	public ICollection<Address>? Addresses { get; set; }
}
