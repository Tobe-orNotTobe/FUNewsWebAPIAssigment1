using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface INewsArticleRepository
	{
		void SaveNewsArticle(NewsArticle n);
		void DeleteNewsArticle(NewsArticle n);
		void UpdateNewsArticle(NewsArticle n);
		List<NewsArticle> GetNewsArticles();
		NewsArticle GetNewsArticleById(string id);
	}
}
