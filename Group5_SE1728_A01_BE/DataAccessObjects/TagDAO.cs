using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
	public class TagDAO
	{
		public static List<Tag> GetTags()
		{
			var listTag = new List<Tag>();
			try
			{
				using var context = new FunewsManagementContext();
				listTag = context.Tags.ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return listTag;
		}

		public static void SaveTag(Tag t)
		{
			try
			{
				using var context = new FunewsManagementContext();

				if (t.TagId == 0)
				{
					var maxId = context.Tags.Any() ? context.Tags.Max(tag => tag.TagId) : 0;
					t.TagId = maxId + 1;
				}

				context.Tags.Add(t);
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void UpdateTag(Tag t)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void DeleteTag(Tag t)
		{
			try
			{
				using var context = new FunewsManagementContext();

				context.Database.ExecuteSqlRaw("DELETE FROM NewsTag WHERE TagId = {0}", t.TagId);

				var tagToDelete = context.Tags.SingleOrDefault(tag => tag.TagId == t.TagId);
				if (tagToDelete != null)
				{
					context.Tags.Remove(tagToDelete);
					context.SaveChanges();
				}

				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static Tag GetTagByID(int id)
		{
			using var db = new FunewsManagementContext();
			return db.Tags.SingleOrDefault(c => c.TagId.Equals(id));
		}
	}
}
