using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatchingModels;
using PatchingPersistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContextFactory<PatchingDbContext>();

var app = builder.Build();

//ensure the database is created
var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<PatchingDbContext>>();
var tempDbContext = dbContextFactory.CreateDbContext();
tempDbContext.Database.EnsureCreated();

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

app.MapGet("/getPeople", async (
		[FromServices] PatchingDbContext dbContext) =>
		{
			var entities = await dbContext.People
				.AsNoTracking()
				.ToListAsync();
			var result = entities.Adapt<IEnumerable<PersonItemDto>>();
			return result;
		})
		.WithName("Get People")
		.WithOpenApi();

app.MapGet("/getPerson/{id}", async (
		[FromServices] PatchingDbContext dbContext, 
		Guid id) =>
		{
			var entity = await dbContext.People
				.AsNoTracking()
				.FirstOrDefaultAsync(_ => _.Id == id);
			if (entity is null)
			{
				return Results.NotFound();
			}
			var result = entity.Adapt<PersonDto>();
			return Results.Ok(result);
		})
		.WithName("Get Person")
		.WithOpenApi();

app.MapPatch("/patchPerson/{id}", async (
		HttpContext context,
		[FromServices] PatchingDbContext dbContext,
		[FromRoute] Guid id,
		[FromBody] Operation[] operations) =>
		{
			var entity = await dbContext.People.FirstOrDefaultAsync(_ => _.Id == id);
			if (entity is null)
			{
				return Results.NotFound();
			}

			var patch = new JsonPatchDocument();
			patch.Operations.AddRange(operations);
			patch.ApplyTo(entity);
			
			var result = entity.Adapt<PersonDto>();
			//return Results.Ok(result);
			return Results.Ok();
		})
		.WithName("Patch Person")
		.WithOpenApi();

app.Run();