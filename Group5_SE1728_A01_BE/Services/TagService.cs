using BusinessObjects;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
	public class TagService : ITagService
	{
		private readonly ITagRepository _repo;

		public TagService() => _repo = new TagRepository();

		public void DeleteTag(Tag c) => _repo.DeleteTag(c);

		public Tag GetTagById(int id) => _repo.GetTagById(id);

		public List<Tag> GetTags() => _repo.GetTags();

		public void SaveTag(Tag c) => _repo.SaveTag(c);

		public void UpdateTag(Tag c) => _repo.UpdateTag(c);
	}
}
