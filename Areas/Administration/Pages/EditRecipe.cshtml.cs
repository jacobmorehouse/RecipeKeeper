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
	}
}
