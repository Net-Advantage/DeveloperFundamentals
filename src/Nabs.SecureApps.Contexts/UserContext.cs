using System.Diagnostics.CodeAnalysis;

namespace Nabs.SecureApps.Contexts;

public sealed class UserContext
{
	[SetsRequiredMembers]
	public UserContext(string username)
	{
		Username = username;
	}

	public required string Username { get; set; }
}