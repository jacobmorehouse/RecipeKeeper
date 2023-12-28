using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Data.Models
{
	public class Ingredient
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(500)]
		public string Name { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(500)]
		public string Description { get; set; }

		[Required]
		public bool Vegetarian { get; set; } = false;
		[Required]
		public bool Vegan { get; set; } = false;
		[Required]
		public int IngredientCategoryId { get; set; }

		public string? AddedById { get; set; }
		public DateTime? AddedDateTimeUTC { get; set; }
		public string? UpdatedById { get; set; }
		public DateTime? UpdatedDateUTC { get; set; }
	}
}
