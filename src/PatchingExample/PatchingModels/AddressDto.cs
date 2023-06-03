namespace PatchingModels;

public class AddressDto
{
	public Guid Id { get; set; }
	public Guid PersonId { get; set; }
	public string? Address1 { get; set; }
	public string? Address2 { get; set; }
	public string? Code { get; set; }
}
