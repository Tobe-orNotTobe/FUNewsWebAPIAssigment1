using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Interfaces;

namespace FUNewsWebAPI.Controllers
{
	public class SystemAccountsController : ODataController
	{
		private readonly ISystemAccountService _service;

		public SystemAccountsController(ISystemAccountService service)
		{
			_service = service;
		}

		// GET: odata/SystemAccounts
		[EnableQuery(PageSize = 20)]
		[Authorize(Roles = "Admin")]
		public IActionResult Get()
		{
			return Ok(_service.GetAccounts().AsQueryable());
		}

		// GET: odata/SystemAccounts(5)
		[EnableQuery]
		[Authorize(Roles = "Admin, Staff")]
		public IActionResult Get([FromODataUri] short key)
		{
			var account = _service.GetAccountById(key);
			if (account == null)
			{
				return NotFound();
			}
			return Ok(account);
		}

		// POST: odata/SystemAccounts
		[EnableQuery]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] SystemAccount systemAccount)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_service.SaveAccount(systemAccount);
				return Created(systemAccount);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT: odata/SystemAccounts(5)
		[EnableQuery]
		[Authorize(Roles = "Admin,Staff")]
		public IActionResult Put([FromODataUri] short key, [FromBody] SystemAccount systemAccount)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != systemAccount.AccountId)
			{
				return BadRequest("Key mismatch");
			}

			var existingAccount = _service.GetAccountById(key);
			if (existingAccount == null)
			{
				return NotFound();
			}

			try
			{
				_service.UpdateAccount(systemAccount);
				return Updated(systemAccount);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE: odata/SystemAccounts(5)
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromODataUri] short key)
		{
			var systemAccount = _service.GetAccountById(key);
			if (systemAccount == null)
			{
				return NotFound();
			}

			// Check if account has created any news articles
			if (systemAccount.NewsArticles != null && systemAccount.NewsArticles.Any())
			{
				return BadRequest("Cannot delete account that has created news articles.");
			}

			try
			{
				_service.DeleteAccount(systemAccount);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}