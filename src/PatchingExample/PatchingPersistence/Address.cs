namespace PatchingPersistence;

public class Address
{
	public Guid Id { get; set; }
	public Guid PersonId { get; set; }
	public string? Address1 { get; set; }
	public string? Address2 { get; set; }
	public string? Code { get; set; }

	public Person Person { get; set; } = default!;
}