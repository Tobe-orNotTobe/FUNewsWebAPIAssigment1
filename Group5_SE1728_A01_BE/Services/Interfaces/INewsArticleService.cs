using BusinessObjects;

namespace Services.Interfaces
{
	public interface INewsArticleService
	{
		void SaveNewsArticle(NewsArticle n);
		void DeleteNewsArticle(NewsArticle n);
		void UpdateNewsArticle(NewsArticle n);
		List<NewsArticle> GetNewsArticles();
		NewsArticle GetNewsArticleById(string id);
	}
}
