using BusinessObjects.Validations;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Validations
{
	// SystemAccount Validation
	public partial class SystemAccountMetadata
	{
		[Required(ErrorMessage = "Account name is required")]
		[StringLength(100, ErrorMessage = "Account name cannot exceed 100 characters")]
		public string AccountName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		[StringLength(70, ErrorMessage = "Email cannot exceed 70 characters")]
		public string AccountEmail { get; set; }

		[Required(ErrorMessage = "Role is required")]
		[Range(0, 2, ErrorMessage = "Role must be 0 (Admin), 1 (Staff), or 2 (Lecturer)")]
		public int? AccountRole { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 70 characters")]
		public string AccountPassword { get; set; }
	}

	// Category Validation
	public partial class CategoryMetadata
	{
		[Required(ErrorMessage = "Category name is required")]
		[StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
		public string CategoryName { get; set; }

		[Required(ErrorMessage = "Category description is required")]
		[StringLength(250, ErrorMessage = "Category description cannot exceed 250 characters")]
		public string CategoryDesciption { get; set; }

		[Required(ErrorMessage = "Active status is required")]
		public bool? IsActive { get; set; }
	}

	// NewsArticle Validation
	public partial class NewsArticleMetadata
	{
		[Required(ErrorMessage = "News article ID is required")]
		[StringLength(20, ErrorMessage = "News article ID cannot exceed 20 characters")]
		public string NewsArticleId { get; set; }

		[Required(ErrorMessage = "News title is required")]
		[StringLength(400, ErrorMessage = "News title cannot exceed 400 characters")]
		public string NewsTitle { get; set; }

		[Required(ErrorMessage = "Headline is required")]
		[StringLength(150, ErrorMessage = "Headline cannot exceed 150 characters")]
		public string Headline { get; set; }

		[StringLength(4000, ErrorMessage = "News content cannot exceed 4000 characters")]
		public string NewsContent { get; set; }

		[StringLength(400, ErrorMessage = "News source cannot exceed 400 characters")]
		public string NewsSource { get; set; }

		[Required(ErrorMessage = "Category is required")]
		public short? CategoryId { get; set; }

		[Required(ErrorMessage = "News status is required")]
		public bool? NewsStatus { get; set; }
	}

	// Tag Validation
	public partial class TagMetadata
	{
		[Required(ErrorMessage = "Tag ID is required")]
		public int TagId { get; set; }

		[Required(ErrorMessage = "Tag name is required")]
		[StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
		public string TagName { get; set; }

		[StringLength(400, ErrorMessage = "Note cannot exceed 400 characters")]
		public string Note { get; set; }
	}
}

// Partial classes to apply metadata
namespace BusinessObjects
{
	[MetadataType(typeof(SystemAccountMetadata))]
	public partial class SystemAccount { }

	[MetadataType(typeof(CategoryMetadata))]
	public partial class Category { }

	[MetadataType(typeof(NewsArticleMetadata))]
	public partial class NewsArticle { }

	[MetadataType(typeof(TagMetadata))]
	public partial class Tag { }
}