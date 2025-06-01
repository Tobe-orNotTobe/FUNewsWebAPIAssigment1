using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories
{
	public class NewsArticleRepository : INewsArticleRepository
	{
		public void DeleteNewsArticle(NewsArticle n) => NewsArticleDAO.DeleteNewsArticle(n);

		public List<NewsArticle> GetNewsArticles() => NewsArticleDAO.GetNewsArticles();

		public NewsArticle GetNewsArticleById(string id) => NewsArticleDAO.GetArticleByID(id);

		public void SaveNewsArticle(NewsArticle n) => NewsArticleDAO.SaveNewsArticle(n);

		public void UpdateNewsArticle(NewsArticle n) => NewsArticleDAO.UpdateNewsArticle(n);
	}
}
