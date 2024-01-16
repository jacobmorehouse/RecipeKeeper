using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeKeeper.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class AddRecipeModel : PageModel
	{

		[BindProperty]
		public string name { get; set; }

		[BindProperty]
		public string description { get; set; }

		[BindProperty]
		public int recipeCategory { get; set; }

		[BindProperty]
		public string? detail { get; set; }

		public RKContext _db;


		[ViewData]
		public List<RecipeCategory> RecipeCategoryList { get; set; }

		[ViewData]
		public List<Ingredient> ingredientList {  get; set; }

		[ViewData]
		public List<UOM> UOMList { get; set; }

		[ViewData]
		public List<Recipe> RecipeList { get; set; }



		public AddRecipeModel(RKContext db) { 
			_db = db;
		}

		public void OnGet()
		{
			RecipeCategoryList = _db.RecipeCategory.ToList();
			ingredientList = _db.Ingredient.ToList();
			UOMList = _db.UOM.ToList();
			RecipeList = _db.Recipe.ToList();
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{

				Recipe newRecipe = new Recipe();
				newRecipe.Name = name;
				newRecipe.Description = description;
				newRecipe.Detail = detail;
				int displayOrder = 0;

				//Ingredient logic
				foreach (var ingItem in from ing in Request.Form where ing.Key.StartsWith("ingredientOption") select ing)
				{
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

					//TODO here if it is already in the list, don't add it again. 

					newRecipe.RelatedRecipes.Add(thisRR);
				}

				RecipeCategory thisRecipeCategory = (from rc in _db.RecipeCategory
													 where rc.Id == recipeCategory
													 select rc).FirstOrDefault();

				newRecipe.RecipeCategory = thisRecipeCategory;
				newRecipe.AddedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				newRecipe.AddedDateTimeUTC = DateTime.UtcNow;

				_db.Recipe.Add(newRecipe);
				_db.SaveChanges();
				return RedirectToPage("Recipes");
			}
			else {
				RecipeCategoryList = _db.RecipeCategory.ToList();
				ingredientList = _db.Ingredient.ToList();
				UOMList = _db.UOM.ToList();
				RecipeList = _db.Recipe.ToList();
				return Page();
			}
		}
	}
}
