using BusinessObjects;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _repo;

		public CategoryService() => _repo = new CategoryRepository();

		public void DeleteCategory(Category c) => _repo.DeleteCategory(c);

		public List<Category> GetCategories() => _repo.GetCategories();

		public Category GetCategoryById(short id) => _repo.GetCategoryById(id);

		public void SaveCategory(Category c) => _repo.SaveCategory(c);

		public void UpdateCategory(Category c) => _repo.UpdateCategory(c);
	}
}
