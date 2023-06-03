using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument patch)
		{
			var entity = await _dbContext.People.FirstOrDefaultAsync(_ => _.Id == id);
			if (entity is null)
			{
				return NotFound();
			}

			patch.ApplyTo(entity);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}