using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Pages
{
    public class ViewRecipeModel : PageModel
    {
        public void OnGet(int RecipeId)
        {
            var db = new thisDb();
            Recipe rec = (from r in db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes).Include(r => r.RecipeCategory)
                          where r.Id == RecipeId
                          select r).FirstOrDefault();

            ViewData["thisRecipe"] = rec;

            List<Ingredient> IngredientList = new List<Ingredient>();

        }
    }
}
