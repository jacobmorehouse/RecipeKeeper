using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecipeKeeper.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class addIngredientCategoryModel : PageModel
	{

		[BindProperty]
		public IngredientCategory newIngredientCategory { get; set; }

		public thisDb db = new thisDb();

		public void OnGet()
		{
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid) {

				newIngredientCategory.AddedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				newIngredientCategory.AddedDateTimeUTC = DateTime.UtcNow;

				db.IngredientCategory.Add(newIngredientCategory);
				db.SaveChanges();

				return RedirectToPage("IngredientCategories");
			} else {
				return Page();
			}

		}
	}
}
