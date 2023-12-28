using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;
using System.Security.Claims;

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
				newIngredient.AddedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				newIngredient.AddedDateTimeUTC = DateTime.UtcNow;

				var db = new thisDb();
				db.Ingredient.Add(newIngredient);
				db.SaveChanges();
			}

			return RedirectToPage("Ingredients");
		}
	}
}
