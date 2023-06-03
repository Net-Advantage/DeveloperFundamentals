using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PatchingPersistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddNewtonsoftJson(_ =>
	{
		_.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
		{
			NamingStrategy = new CamelCaseNamingStrategy()
			{
				ProcessDictionaryKeys = false
			}
		};
		_.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
		_.SerializerSettings.NullValueHandling = NullValueHandling.Include;
	});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContextFactory<PatchingDbContext>();

var app = builder.Build();

//ensure the database is created
var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<PatchingDbContext>>();
var tempDbContext = dbContextFactory.CreateDbContext();
tempDbContext.Database.EnsureCreated();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(_ =>
{
	_.AllowAnyOrigin();
	_.WithMethods("GET", "POST", "PATCH", "PUT", "DELETE");
	//_.AllowAnyMethod();
	_.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
