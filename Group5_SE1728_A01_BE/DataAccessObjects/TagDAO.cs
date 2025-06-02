using BusinessObjects;

namespace DataAccessObjects
{
	public class TagDAO
	{
		public static List<Tag> GetTag()
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
				var t1 = context.Tags.SingleOrDefault(c => c.TagId == t.TagId);
				context.Tags.Remove(t1);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static Tag GetArticleByID(int id)
		{
			using var db = new FunewsManagementContext();
			return db.Tags.SingleOrDefault(c => c.TagId.Equals(id));
		}
	}
}
