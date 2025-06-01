using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Interfaces;

namespace FUNewsWebAPI.Controllers
{
	public class NewsArticlesController : ODataController
	{
		private readonly INewsArticleService _service;

		public NewsArticlesController(INewsArticleService service)
		{
			_service = service;
		}

		// GET: odata/NewsArticles - Public access for active news
		[EnableQuery(PageSize = 20)]
		[AllowAnonymous]
		public IActionResult Get()
		{
			var articles = _service.GetNewsArticles();

			if (!User.Identity.IsAuthenticated)
			{
				articles = articles.Where(a => a.NewsStatus == true).ToList();
			}

			return Ok(articles.AsQueryable());
		}

		// GET: odata/NewsArticles('id') - Public access for active news
		[EnableQuery]
		[AllowAnonymous]
		public IActionResult Get([FromODataUri] string key)
		{
			var article = _service.GetNewsArticleById(key);
			if (article == null)
			{
				return NotFound();
			}

			if (!User.Identity.IsAuthenticated && article.NewsStatus != true)
			{
				return NotFound();
			}

			return Ok(article);
		}

		// POST: odata/NewsArticles
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Post([FromBody] NewsArticle newsArticle)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			newsArticle.CreatedDate = DateTime.Now;

			try
			{
				_service.SaveNewsArticle(newsArticle);
				return Created(newsArticle);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT: odata/NewsArticles('id')
		[EnableQuery]
		[Authorize(Roles = "Staff")]
		public IActionResult Put([FromODataUri] string key, [FromBody] NewsArticle newsArticle)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != newsArticle.NewsArticleId)
			{
				return BadRequest("Key mismatch");
			}

			var existingArticle = _service.GetNewsArticleById(key);
			if (existingArticle == null)
			{
				return NotFound();
			}

			newsArticle.ModifiedDate = DateTime.Now;

			try
			{
				_service.UpdateNewsArticle(newsArticle);
				return Updated(newsArticle);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE: odata/NewsArticles('id')
		[Authorize(Roles = "Staff")]
		public IActionResult Delete([FromODataUri] string key)
		{
			var newsArticle = _service.GetNewsArticleById(key);
			if (newsArticle == null)
			{
				return NotFound();
			}

			try
			{
				_service.DeleteNewsArticle(newsArticle);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}