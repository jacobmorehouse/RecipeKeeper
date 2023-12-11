using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Data.Models
{
	public class RelatedRecipe
	{
		[Key]
		public int Id { get; set; }
		
		public int RecipeId { get; set; }
		public int relatedRecipeId { get; set; }
	}
}
