using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
    public class AddRecipeModel : PageModel
    {

		[BindProperty]
		public Recipe newRecipe { get; set; }


        public void OnGet()
        {
		}

        public IActionResult OnPost() { 
            if (ModelState.IsValid) {
                var db = new thisDb();
				int displayOrder = 0;


				//Ingredient logic
				foreach (var ingItem in from ing in Request.Form where ing.Key.StartsWith("ingredientOption") select ing) {
					//first get all the related parts of this ingredient
					int ingNumber = int.Parse(ingItem.Key.Replace("ingredientOption", ""));
					int thisIngredientOption = int.Parse(Request.Form["ingredientOption" + ingNumber]);
					var thisingredientUnitValue = Decimal.Parse(Request.Form["ingredientUnitValue" + ingNumber]);
					int thisingredientUOM = int.Parse(Request.Form["ingredientUOM" + ingNumber]);
					//TODO switch these parses over to a tryparse, and add some logic to what we send back to the page.


					Recipe_Ingredient thisRI = new Recipe_Ingredient() { UOMId = thisingredientUOM, IngredientId = thisIngredientOption, Quantity = thisingredientUnitValue, DisplayOrder = displayOrder };
					newRecipe.Ingredients.Add(thisRI);
					displayOrder++;
				}

				//related recipe logic
				foreach (var relatedRecipe in from rr in Request.Form where rr.Key.StartsWith("relatedRecipeOption") select rr)
				{
					int rrNumber = int.Parse(relatedRecipe.Key.Replace("relatedRecipeOption", ""));
					RelatedRecipe thisRR = new RelatedRecipe() { relatedRecipeId = int.Parse(Request.Form["relatedRecipeOption" + rrNumber]) };
					
					//todo here if it is already in the list, don't add it again. 
					
					newRecipe.RelatedRecipes.Add(thisRR);

				}


				db.Recipe.Add(newRecipe);
                db.SaveChanges();
				

			}
			return RedirectToPage("Recipes");
		}
    }
}
