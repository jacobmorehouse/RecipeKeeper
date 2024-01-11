using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
    public class RecipesModel : PageModel
    {
		public thisDb db = new thisDb();

		public void OnGet()
        {
        }

		public IActionResult OnPostDelete()
		{
			var recipeId = int.Parse(Request.Form["recipeId"]);

			var thisRecipe = (from i in db.Recipe
								  where i.Id == recipeId
								  select i).FirstOrDefault();

			if (thisRecipe != null)
			{
				db.Recipe.Remove(thisRecipe);
				var listOfRecipeIngredients = db.Recipe_Ingredient.ToList();
				List<Recipe_Ingredient> theseRecipeIngredients = (from ri in listOfRecipeIngredients
																  where ri.RecipeId == recipeId
																  select ri).ToList();

				foreach (Recipe_Ingredient ri in theseRecipeIngredients)
				{
					db.Recipe_Ingredient.Remove(ri);
				}


				db.SaveChanges();
			}

			return RedirectToPage("Recipes");
		}
	}
}
