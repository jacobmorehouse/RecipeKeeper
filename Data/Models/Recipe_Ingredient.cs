using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Data.Models
{
	public class Recipe_Ingredient
	{
		[Key]
		public int Id { get; set; }

		public int RecipeId { get; set; }
		public int IngredientId { get; set; }
		public int DisplayOrder { get; set; }
		public int UOMId { get; set; }
		public decimal Quantity { get; set; }
	}
}
