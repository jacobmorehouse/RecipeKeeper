using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
    public class AddIngredientModel : PageModel
    {
		[BindProperty]
		public Ingredient newIngredient { get; set; }

		public void OnGet()
        {
        }

        public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				var db = new thisDb();
				db.Ingredient.Add(newIngredient);
				db.SaveChanges();
			}

			return RedirectToPage("Ingredients");
		}
    }
}
