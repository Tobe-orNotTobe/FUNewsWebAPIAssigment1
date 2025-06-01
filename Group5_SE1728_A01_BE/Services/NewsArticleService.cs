using BusinessObjects;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
	public class NewsArticleService : INewsArticleService
	{
		private readonly INewsArticleRepository _repo;

		public NewsArticleService() => _repo = new NewsArticleRepository();

		public void DeleteNewsArticle(NewsArticle n) => _repo.DeleteNewsArticle(n);

		public NewsArticle GetNewsArticleById(string id) => _repo.GetNewsArticleById(id);

		public List<NewsArticle> GetNewsArticles() => _repo.GetNewsArticles();

		public void SaveNewsArticle(NewsArticle n) => _repo.SaveNewsArticle(n);

		public void UpdateNewsArticle(NewsArticle n) => _repo.UpdateNewsArticle(n);
	}
}
