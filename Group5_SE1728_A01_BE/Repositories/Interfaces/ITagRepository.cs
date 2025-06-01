using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface ITagRepository
	{
		void SaveTag(Tag c);
		void DeleteTag(Tag c);
		void UpdateTag(Tag c);
		List<Tag> GetTags();
		Tag GetTagById(int id);
	}
}
