using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Data.Models
{
	public class IngredientCategory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(3, ErrorMessage = "The Name should be at least 3 characters.")]
		[MaxLength(500)]
		public string Name { get; set; }
	}
}
