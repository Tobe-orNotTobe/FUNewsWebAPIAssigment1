using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Interfaces;

namespace FUNewsWebAPI.Controllers
{
	public class CategoriesController : ODataController
	{
		private readonly ICategoryService _service;
		private readonly INewsArticleService _newsService;

		public CategoriesController(ICategoryService service, INewsArticleService newsService)
		{
			_service = service;
			_newsService = newsService;
		}

		// GET: odata/Categories
		[EnableQuery(PageSize = 20)]
		public IActionResult Get()
		{
			return Ok(_service.GetCategories().AsQueryable());
		}

		// GET: odata/Categories(5)
		[EnableQuery]
		[Authorize(Roles = "Staff,Admin")]
		public IActionResult Get([FromODataUri] short key)
		{
			var category = _service.GetCategoryById(key);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}

		// POST: odata/Categories
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Post([FromBody] Category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_service.SaveCategory(category);
				return Created(category);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT: odata/Categories(5)
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Put([FromODataUri] short key, [FromBody] Category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != category.CategoryId)
			{
				return BadRequest("Key mismatch");
			}

			var existingCategory = _service.GetCategoryById(key);
			if (existingCategory == null)
			{
				return NotFound();
			}

			try
			{
				_service.UpdateCategory(category);
				return Updated(category);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE: odata/Categories(5)
		[Authorize(Roles = "Staff")]
		public IActionResult Delete([FromODataUri] short key)
		{
			var category = _service.GetCategoryById(key);
			if (category == null)
			{
				return NotFound();
			}

			// Check if category is used in any news articles
			var articles = _newsService.GetNewsArticles();
			if (articles.Any(a => a.CategoryId == key))
			{
				return BadRequest("Cannot delete category that is assigned to news articles.");
			}

			try
			{
				_service.DeleteCategory(category);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}