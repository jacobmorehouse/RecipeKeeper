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


        public SelectList IngredientOptions { get; set; }


        public void OnGet()
        {
			var db = new thisDb();
			IngredientOptions = new SelectList(db.Ingredient.ToList(), nameof(Ingredient.Id), nameof(Ingredient.Name));


            
		}
    }
}
