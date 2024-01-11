using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper
{
	public class thisDb : DbContext
	{
		//this was for trying to implement dependancy injection
		//public thisDb(DbContextOptions<ApplicationDbContext> options) : base(options)
		//{
		//}

		public thisDb() { } //TODO this is temporary just for debugging, get rid of this, and manage all the pages normally without creating new instances of thisDB.

		public DbSet<Ingredient> Ingredient { get; set; }
		public DbSet<IngredientCategory> IngredientCategory { get; set; }
		public DbSet<Recipe_Ingredient> Recipe_Ingredient { get; set; }
		public DbSet<Recipe> Recipe { get; set; }
		public DbSet<UOM> UOM { get; set; }
		public DbSet<RelatedRecipe> RelatedRecipe { get; set; }
		public DbSet<RecipeCategory> RecipeCategory { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = WebApplication.CreateBuilder();
			IConfiguration configuration = builder.Configuration;

			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
