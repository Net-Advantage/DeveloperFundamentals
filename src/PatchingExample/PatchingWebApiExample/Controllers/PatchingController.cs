using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabs.Core.Patching;
using PatchingModels;
using PatchingPersistence;

namespace PatchingWebApiExample.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PatchingController : ControllerBase
	{
		private readonly PatchingDbContext _dbContext;

		public PatchingController(PatchingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("getPeople")]
		public async Task<ActionResult<PersonItemDto[]>> List()
		{
			var entities = await _dbContext.People
				.AsNoTracking()
				.ToListAsync();

			var result = entities.Adapt<IEnumerable<PersonItemDto>>();
			return Ok(result);
		}

		[HttpGet]
		[Route("getPerson/{id}")]
		public async Task<ActionResult<PersonDto>> Get(Guid id)
		{
			var entity = await _dbContext.People
				.AsNoTracking()
				.FirstOrDefaultAsync(_ => _.Id == id);
			if (entity is null)
			{
				return NotFound();
			}
			var result = entity.Adapt<PersonDto>();
			return Ok(result);
		}

		[HttpPatch]
		[Route("patchPerson/{id}")]
		public async Task<ActionResult<IList<Operation>>> Patch(
			Guid id, 
			[FromBody] JsonPatchDocument patch)
		{
			var entity = await _dbContext.People.FirstOrDefaultAsync(_ => _.Id == id);
			if (entity is null)
			{
				return NotFound();
			}

			patch.ApplyTo(entity);

			var original = entity.DeepClone();
			entity.Calculate();
			await _dbContext.SaveChangesAsync();

			var result = original.CreatePatchOperations(entity);
			return Ok(result);
		}
	}
}

public static class PersonCalculator
{
	public static void Calculate(this Person entity)
	{
		entity.AnnualSalary = entity.HourlyRate * 2080;
	}
}