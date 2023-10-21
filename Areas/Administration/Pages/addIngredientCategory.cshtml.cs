using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class addIngredientCategoryModel : PageModel
	{

		[BindProperty]
		public IngredientCategory newIngredientCategory { get; set; }
		
		public void OnGet()
		{
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid) {

				var db = new thisDb();
				db.IngredientCategory.Add(newIngredientCategory);
				db.SaveChanges();


				return RedirectToPage("IngredientCategories");
			} else {
				return Page();
			}

		}
	}
}
