using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PatchingPersistence;

public class PatchingDbContext : DbContext
{
	private static readonly Lazy<SqliteConnection> Connection = new(() =>
	{
		var connectionString = "Data Source=:memory:";
		var connection = new SqliteConnection(connectionString);
		connection.Open();
		return connection;
	});

	public DbSet<Person> People { get; set; } = default!;
	public DbSet<Address> Addresses { get; set; } = default!;
	public DbSet<Book> Books { get; set; } = default!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(Connection.Value);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Username).IsRequired();
		});
		modelBuilder.Entity<Address>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Person)
				.WithMany()
				.HasForeignKey(e => e.PersonId);
		});
		modelBuilder.Entity<Book>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired();
			entity.HasOne(e => e.Person)
				.WithMany()
				.HasForeignKey(e => e.PersonId);
		});

		var personId1 = new Guid("c9070d95-3ce4-478c-a139-c3a69e3051de");
		var personId2 = new Guid("2115aad9-00f2-4fce-ab53-094c108ca8b4");

		modelBuilder.Entity<Person>()
			.HasData(new[]
			{
				new Person()
				{
					Id = personId1,
					Username = "jpdoe"
				},
				new Person()
				{
					Id = personId2,
					Username = "jddoe"
				},
			});

		modelBuilder.Entity<Address>()
			.HasData(new[]
			{
				new Address()
				{
					Id = new Guid("2115aad9-00f2-4fce-ab53-094c108ca8b4"),
					Address1 = "Address 1",
					Address2 = "Address 2",
					Code = "12345",
					PersonId = personId1
				},
			});

		modelBuilder.Entity<Book>()
			.HasData(new[]
			{
				new Book()
				{
					Id = new Guid("9be5430e-559f-4c81-9732-f2264fec51b1"),
					Name = "The Book 1",
					PersonId = personId1
				},
				new Book()
				{
					Id = new Guid(" 249a2acc-7fe4-4c7f-b293-db89df2b4b5f"),
					Name = "The Book 2",
					PersonId = personId1
				},
				new Book()
				{
					Id = new Guid("64d0b445-819c-4a47-8537-12a3ca9becd2"),
					Name = "The Book 3",
					PersonId = personId2
				}
			});
	}
}