using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		void SaveCategory(Category c);
		void DeleteCategory(Category c);
		void UpdateCategory(Category c);
		List<Category> GetCategories();
		Category GetCategoryById(short id);
	}
}
