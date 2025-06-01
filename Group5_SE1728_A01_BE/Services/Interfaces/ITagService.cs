using BusinessObjects;

namespace Services.Interfaces
{
	public interface ITagService
	{
		void SaveTag(Tag c);
		void DeleteTag(Tag c);
		void UpdateTag(Tag c);
		List<Tag> GetTags();
		Tag GetTagById(int id);
	}
}
