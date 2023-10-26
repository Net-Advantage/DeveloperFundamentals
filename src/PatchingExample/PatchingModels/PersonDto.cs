namespace PatchingModels;
public partial class PersonDto
{
	public Guid Id { get; set; }
	public string Username { get; set; } = default!;
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public DateOnly DateOfBirth { get; set; }
	public decimal HourlyRate { get; set; }
	public decimal AnnualSalary { get; set; }
}