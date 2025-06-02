using BusinessObjects;

namespace DataAccessObjects
{
	public class CategoryDAO
	{
		public static List<Category> GetCategories()
		{
			var listCategories = new List<Category>();
			try
			{
				using var context = new FunewsManagementContext();
				listCategories = context.Categories.ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return listCategories;
		}

		public static void SaveCategory(Category c)
		{
			try
			{
				using var context = new FunewsManagementContext();
				context.Categories.Add(c);
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void UpdateCategory(Category c)
		{
			try
			{
				using var context = new FunewsManagementContext();

				var existingCategory = context.Categories.Find(c.CategoryId);
				if (existingCategory != null)
				{
					existingCategory.CategoryName = c.CategoryName;
					existingCategory.CategoryDesciption = c.CategoryDesciption;
					existingCategory.ParentCategoryId = c.ParentCategoryId;
					existingCategory.IsActive = c.IsActive; 

					context.SaveChanges();
				}
				else
				{
					throw new Exception("Category not found");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void DeleteCategory(Category c)
		{
			try
			{
				using var context = new FunewsManagementContext();
                var c1 = context.Categories.SingleOrDefault(cat => cat.CategoryId == c.CategoryId);
                context.Categories.Remove(c1);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public static Category GetArticleByID(short id)
		{
			using var db = new FunewsManagementContext();
			return db.Categories.SingleOrDefault(c => c.CategoryId.Equals(id));
		}
	}
}
