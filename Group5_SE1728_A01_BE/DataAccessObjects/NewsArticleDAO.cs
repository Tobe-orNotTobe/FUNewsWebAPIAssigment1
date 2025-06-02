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
				listNewsArticle = db.NewsArticles
					.Include(f => f.Category)
					.Include(f => f.CreatedBy) 
					.Include(f => f.Tags)
					.ToList();
			}
			catch (Exception e) { }
			return listNewsArticle;
		}

		public static void SaveNewsArticle(NewsArticle n)
		{
			try
			{
				using var context = new FunewsManagementContext();

				if (n.Tags != null && n.Tags.Any())
				{
					var tagIds = n.Tags.Select(t => t.TagId).ToList();
					var existingTags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
					n.Tags = existingTags;
				}

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

				var existingArticle = context.NewsArticles
				 .Include(a => a.Tags)
				 .FirstOrDefault(a => a.NewsArticleId == n.NewsArticleId);
				if (existingArticle == null)
				{
					throw new Exception("Article not found");
				}

				existingArticle.NewsTitle = n.NewsTitle;
				existingArticle.Headline = n.Headline;
				existingArticle.NewsContent = n.NewsContent;
				existingArticle.NewsSource = n.NewsSource;
				existingArticle.CategoryId = n.CategoryId;
				existingArticle.NewsStatus = n.NewsStatus;
				existingArticle.UpdatedById = n.UpdatedById;
				existingArticle.ModifiedDate = n.ModifiedDate;

				if (n.Tags != null)
				{
					// Clear existing tags
					existingArticle.Tags.Clear();

					// Add new tags
					if (n.Tags.Any())
					{
						var tagIds = n.Tags.Select(t => t.TagId).ToList();
						var existingTags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();

						foreach (var tag in existingTags)
						{
							existingArticle.Tags.Add(tag);
						}
					}
				}

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
				var n1 = context.NewsArticles
				   .Include(a => a.Tags)
				   .SingleOrDefault(c => c.NewsArticleId == n.NewsArticleId);

				if (n1 != null)
				{
					n1.Tags.Clear();
					context.NewsArticles.Remove(n1);
					context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static NewsArticle GetArticleByID(string id)
		{
			using var db = new FunewsManagementContext();
			return db.NewsArticles.Include(f => f.Tags).Include(f => f.Category).Include(f => f.CreatedBy).SingleOrDefault(c => c.NewsArticleId.Equals(id));
		}
	}
}
