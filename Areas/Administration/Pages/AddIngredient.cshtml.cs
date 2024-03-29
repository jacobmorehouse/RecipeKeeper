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
		public RKContext _db;

		[ViewData]
		public List<IngredientCategory> IngredientCategoryList { get; set; }

		public AddIngredientModel(RKContext db)
		{
			_db = db;
		}

		public void OnGet()
		{
			IngredientCategoryList = _db.IngredientCategory.ToList();
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				newIngredient.AddedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				newIngredient.AddedDateTimeUTC = DateTime.UtcNow;

				_db.Ingredient.Add(newIngredient);
				_db.SaveChanges();
				return RedirectToPage("Ingredients");
			} else {
				IngredientCategoryList = _db.IngredientCategory.ToList();
				return Page();
			}

			
		}
	}
}
