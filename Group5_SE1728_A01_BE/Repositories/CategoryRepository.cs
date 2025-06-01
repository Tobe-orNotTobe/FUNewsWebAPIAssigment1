using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		public void DeleteCategory(Category c) => CategoryDAO.DeleteCategory(c);

		public List<Category> GetCategories() => CategoryDAO.GetCategories();

		public Category GetCategoryById(short id) => CategoryDAO.GetArticleByID(id);

		public void SaveCategory(Category c) => CategoryDAO.SaveCategory(c);

		public void UpdateCategory(Category c) => CategoryDAO.UpdateCategory(c);
	}
}
