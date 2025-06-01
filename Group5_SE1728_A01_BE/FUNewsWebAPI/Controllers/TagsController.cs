using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Interfaces;

namespace FUNewsWebAPI.Controllers
{
	public class TagsController : ODataController
	{
		private readonly ITagService _service;

		public TagsController(ITagService service)
		{
			_service = service;
		}

		// GET: odata/Tags
		[EnableQuery(PageSize = 20)]
		public IActionResult Get()
		{
			return Ok(_service.GetTags().AsQueryable());
		}

		// GET: odata/Tags(5)
		[EnableQuery]
		[Authorize(Roles = "Staff,Admin")]
		public IActionResult Get([FromODataUri] int key)
		{
			var tag = _service.GetTagById(key);
			if (tag == null)
			{
				return NotFound();
			}
			return Ok(tag);
		}

		// POST: odata/Tags
		[HttpPost]
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Post([FromBody] Tag tag)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_service.SaveTag(tag);
				return Created(tag);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT: odata/Tags(5)
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Put([FromODataUri] int key, [FromBody] Tag tag)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != tag.TagId)
			{
				return BadRequest("Key mismatch");
			}

			var existingTag = _service.GetTagById(key);
			if (existingTag == null)
			{
				return NotFound();
			}

			try
			{
				_service.UpdateTag(tag);
				return Updated(tag);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE: odata/Tags(5)
		[Authorize(Roles = "Staff")]
		public IActionResult Delete([FromODataUri] int key)
		{
			var tag = _service.GetTagById(key);
			if (tag == null)
			{
				return NotFound();
			}

			try
			{
				_service.DeleteTag(tag);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}