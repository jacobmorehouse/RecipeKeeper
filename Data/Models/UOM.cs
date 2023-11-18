using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Data.Models
{
	public class UOM
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Shortname { get; set; }

	}
}
