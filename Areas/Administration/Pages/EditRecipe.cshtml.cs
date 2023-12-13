using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class EditRecipeModel : PageModel
	{
		[BindProperty]
		public Recipe thisRecipe { get; set; }

		public void OnGet(int RecipeId)
		{
			var db = new thisDb();
			Recipe rec = (	from r in db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes)
							where r.Id == RecipeId
							select r).FirstOrDefault();

			//TODO some logic here if rec is null...


			this.thisRecipe = rec;
		}


		public IActionResult OnPost() {
			//if (ModelState.IsValid) { }

			//TODO not doing model state checks here because I'm freehanding this one. Need to write in some logic to validate the data here and pass back to the form if validation failed.
			var db = new thisDb();
			int displayOrder = 0;
			Recipe theRecipeWeAreEditing = (from r in db.Recipe
										where r.Id == thisRecipe.Id
										select r).FirstOrDefault();

			
			foreach (var ingItem in from ing in Request.Form where ing.Key.StartsWith("ingredientOption") select ing)
			{
				int ingNumber = int.Parse(ingItem.Key.Replace("ingredientOption", ""));
				int thisIngredientOption = int.Parse(Request.Form["ingredientOption" + ingNumber]);
				var thisingredientUnitValue = Decimal.Parse(Request.Form["ingredientUnitValue" + ingNumber]);
				int thisingredientUOM = int.Parse(Request.Form["ingredientUOM" + ingNumber]);
				//TODO switch these parses over to a tryparse, and add some logic to what we send back to the page.


				Recipe_Ingredient thisRI = new Recipe_Ingredient() { UOMId = thisingredientUOM, IngredientId = thisIngredientOption, Quantity = thisingredientUnitValue, DisplayOrder = displayOrder, RecipeId = thisRecipe.Id };
				thisRecipe.Ingredients.Add(thisRI);
				displayOrder++;
			}


			foreach (var relatedRecipe in from rr in Request.Form where rr.Key.StartsWith("relatedRecipeOption") select rr)
			{
				int rrNumber = int.Parse(relatedRecipe.Key.Replace("relatedRecipeOption", ""));
				RelatedRecipe thisRR = new RelatedRecipe() { relatedRecipeId = int.Parse(Request.Form["relatedRecipeOption" + rrNumber]), RecipeId = thisRecipe.Id };

				//todo here if it is already in the list, don't add it again. 

				thisRecipe.RelatedRecipes.Add(thisRR);

			}


			//First we have to delete existing recipe_ingredients...
			IEnumerable<Recipe_Ingredient> riToRevome = from ri in db.Recipe_Ingredient
										where ri.RecipeId == thisRecipe.Id
										select ri;
			foreach (var ri in riToRevome) { db.Recipe_Ingredient.Remove(ri); }

			//next we have to delete existing relatedRecipes
			IEnumerable<RelatedRecipe> rrListToDelete = from rr in db.RelatedRecipe
										where rr.RecipeId == thisRecipe.Id
										select rr;
			foreach (var rr in rrListToDelete) { db.RelatedRecipe.Remove(rr); }


			theRecipeWeAreEditing.Id = thisRecipe.Id;
			theRecipeWeAreEditing.Name = thisRecipe.Name;
			theRecipeWeAreEditing.Description = thisRecipe.Description;
			theRecipeWeAreEditing.Detail = thisRecipe.Detail;
			theRecipeWeAreEditing.Ingredients = thisRecipe.Ingredients;
			theRecipeWeAreEditing.RelatedRecipes = thisRecipe.RelatedRecipes;

			db.SaveChanges();

			return RedirectToPage("Recipes");
		}
	}
}
