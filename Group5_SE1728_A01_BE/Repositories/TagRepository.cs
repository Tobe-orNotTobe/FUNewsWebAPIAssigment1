﻿using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories
{
	public class TagRepository : ITagRepository
	{
		public void DeleteTag(Tag c) => TagDAO.DeleteTag(c);

		public Tag GetTagById(int id) => TagDAO.GetTagByID(id);

		public List<Tag> GetTags() => TagDAO.GetTags();

		public void SaveTag(Tag c) => TagDAO.SaveTag(c);

		public void UpdateTag(Tag c) => TagDAO.UpdateTag(c);
	}
}
