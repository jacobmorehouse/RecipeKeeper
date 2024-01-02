using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RecipeKeeper.Data.Models
{
	public class Recipe
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = "";
		[Required]
		public string Description { get; set; } = "";
		public IList<Recipe_Ingredient> Ingredients { get; set; } = new List<Recipe_Ingredient>();
		public string? Detail { get; set; } = "";
		public IList<RelatedRecipe> RelatedRecipes { get; set; } = new List<RelatedRecipe>();
		public RecipeCategory RecipeCategory { get; set; } = new RecipeCategory();
		public string? AddedById { get; set; }
		public DateTime? AddedDateTimeUTC { get; set; }
		public string? UpdatedById { get; set; }
		public DateTime? UpdatedDateUTC { get; set; }
	}
}
