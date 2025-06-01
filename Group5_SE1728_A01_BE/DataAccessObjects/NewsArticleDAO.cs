using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
	public class NewsArticleDAO
	{
		public static List<NewsArticle> GetNewsArticles()
		{
			var listNewsArticle = new List<NewsArticle>();
			try
			{
				using var db = new FunewsManagementContext();
				listNewsArticle = db.NewsArticles.Include(f => f.Category).ToList();
			}
			catch (Exception e) { }
			return listNewsArticle;
		}

		public static void SaveNewsArticle(NewsArticle n)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.NewsArticles.Add(n);
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void UpdateNewsArticle(NewsArticle n)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.Entry(n).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void DeleteNewsArticle(NewsArticle n)
		{
			try
			{
				using var context = new FunewsManagementContext();
				var n1 = context.NewsArticles.SingleOrDefault(c => c.NewsArticleId == n.NewsArticleId);
				context.NewsArticles.Remove(n1);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static NewsArticle GetArticleByID(string id)
		{
			using var db = new FunewsManagementContext();
			return db.NewsArticles.Include(f => f.Tags).Include(f => f.Category).SingleOrDefault(c => c.NewsArticleId.Equals(id));
		}
	}
}
