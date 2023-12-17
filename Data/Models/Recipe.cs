namespace RecipeKeeper.Data.Models
{
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public IList<Recipe_Ingredient> Ingredients { get; set; } = new List<Recipe_Ingredient>();
		public string Detail { get; set; } = "";
		public IList<RelatedRecipe> RelatedRecipes { get; set; } = new List<RelatedRecipe>();

		public RecipeCategory RecipeCategory { get; set; } = new RecipeCategory();
	}
}
