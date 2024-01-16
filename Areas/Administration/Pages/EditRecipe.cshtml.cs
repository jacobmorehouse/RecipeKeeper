using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class EditRecipeModel : PageModel
	{
		[BindProperty]
		public int Id { get; set; }

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
		public List<Ingredient> ingredientList { get; set; }

		[ViewData]
		public List<UOM> UOMList { get; set; }

		[ViewData]
		public List<Recipe> RecipeList { get; set; }

		[ViewData]
		public Recipe thisRecipe { get; set; }



		public EditRecipeModel(RKContext db) { 
			_db = db;
		}


		public void OnGet(int RecipeId)
		{
			Recipe rec = (	from r in _db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes).Include(r => r.RecipeCategory)
							where r.Id == RecipeId
							select r).FirstOrDefault();

			//TODO some logic here if rec is null...

			var thisRecipeCategory = _db.RecipeCategory.Where(x => x.Id == rec.RecipeCategory.Id).FirstOrDefault();

			name = rec.Name; 
			description = rec.Description;
			recipeCategory = thisRecipeCategory.Id;
			detail = rec.Detail;

			thisRecipe = rec;
			RecipeCategoryList = _db.RecipeCategory.ToList();
			ingredientList = _db.Ingredient.ToList();
			UOMList = _db.UOM.ToList();
			RecipeList = _db.Recipe.ToList();
		}


		public IActionResult OnPost(int RecipeId) {
			if (ModelState.IsValid)
			{
				int displayOrder = 0;
				Recipe theRecipeWeAreEditing = (from r in _db.Recipe
												where r.Id == RecipeId
												select r).FirstOrDefault();


				foreach (var ingItem in from ing in Request.Form where ing.Key.StartsWith("ingredientOption") select ing)
				{
					int ingNumber = int.Parse(ingItem.Key.Replace("ingredientOption", ""));
					int thisIngredientOption = int.Parse(Request.Form["ingredientOption" + ingNumber]);
					var thisingredientUnitValue = Decimal.Parse(Request.Form["ingredientUnitValue" + ingNumber]);
					int thisingredientUOM = int.Parse(Request.Form["ingredientUOM" + ingNumber]);
					//TODO switch these parses over to a tryparse, and add some logic to what we send back to the page.


					Recipe_Ingredient thisRI = new Recipe_Ingredient() { UOMId = thisingredientUOM, IngredientId = thisIngredientOption, Quantity = thisingredientUnitValue, DisplayOrder = displayOrder, RecipeId = RecipeId };
					theRecipeWeAreEditing.Ingredients.Add(thisRI);
					displayOrder++;
				}


				foreach (var relatedRecipe in from rr in Request.Form where rr.Key.StartsWith("relatedRecipeOption") select rr)
				{
					int rrNumber = int.Parse(relatedRecipe.Key.Replace("relatedRecipeOption", ""));
					RelatedRecipe thisRR = new RelatedRecipe() { relatedRecipeId = int.Parse(Request.Form["relatedRecipeOption" + rrNumber]), RecipeId = RecipeId };

					//todo here if it is already in the list, don't add it again. 

					theRecipeWeAreEditing.RelatedRecipes.Add(thisRR);
				}


				//First we have to delete existing recipe_ingredients...
				IEnumerable<Recipe_Ingredient> riToRevome = from ri in _db.Recipe_Ingredient
															where ri.RecipeId == RecipeId
															select ri;
				foreach (var ri in riToRevome) { _db.Recipe_Ingredient.Remove(ri); }

				//next we have to delete existing relatedRecipes
				IEnumerable<RelatedRecipe> rrListToDelete = from rr in _db.RelatedRecipe
															where rr.RecipeId == RecipeId
															select rr;
				foreach (var rr in rrListToDelete) { _db.RelatedRecipe.Remove(rr); }


				theRecipeWeAreEditing.Id = RecipeId;
				theRecipeWeAreEditing.Name = name;
				theRecipeWeAreEditing.Description = description;
				theRecipeWeAreEditing.Detail = detail;
				theRecipeWeAreEditing.Ingredients = theRecipeWeAreEditing.Ingredients;
				theRecipeWeAreEditing.RelatedRecipes = theRecipeWeAreEditing.RelatedRecipes;


				//recipe category...
				var rcId = int.Parse(Request.Form.Where(k => k.Key.StartsWith("RecipeCategory")).ToList().FirstOrDefault().Value);
				RecipeCategory thisRecipeCategory = (from rc in _db.RecipeCategory
													 where rc.Id == rcId
													 select rc).FirstOrDefault();

				theRecipeWeAreEditing.RecipeCategory = thisRecipeCategory;

				theRecipeWeAreEditing.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				theRecipeWeAreEditing.UpdatedDateUTC = DateTime.UtcNow;

				_db.SaveChanges();
				return RedirectToPage("Recipes");
			}
			else {
				thisRecipe = (from r in _db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes).Include(r => r.RecipeCategory)
									where r.Id == RecipeId
									select r).FirstOrDefault();

				RecipeCategoryList = _db.RecipeCategory.ToList();
				ingredientList = _db.Ingredient.ToList();
				UOMList = _db.UOM.ToList();
				RecipeList = _db.Recipe.ToList();
				return Page();
			}
		}
	}
}
