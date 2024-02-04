
using System.Diagnostics;

namespace CakesLibrary.Models
{
    public class Kitchen
    {
        public event Action<Cake> CakeReady;

        readonly Storage _storage;
        readonly Workshop _workshop;
        public Kitchen(Storage storage)
        {
            _storage = storage;
            _workshop = new Workshop();
        }

        public async Task MakeCake(string cakeName)
        {
            var availableRecipes = GetAvailableRecipes();
            var recipeForTheCake = availableRecipes.Keys.FirstOrDefault(recipeName => recipeName.ToLower() == cakeName.ToLower());
            if (recipeForTheCake == null)
            {
                throw new Exception("Нет рецепта для такого чуда :(");
            }

            var neededIngredients = availableRecipes.First(recipe => recipe.Key.ToLower() == cakeName.ToLower()).Value;

            // Берем необходимые ингредиенты из Склада
            var ingredients = _storage.TakeIngredients(neededIngredients);

            // Готовим торт
            await Task.Delay(5000);
            var cake = new Cake(cakeName, ingredients);
            
            CakeReady?.Invoke(cake);
        }

        public Dictionary<string, Dictionary<string, int>> GetAvailableRecipes()
        {
            var recipes = _workshop.GetAllRecipes();
            var ingredientsToRecipe = new Dictionary<string, Dictionary<string, int>>();

            foreach (var recipe in recipes)
            {
                try
                {
                    Debug.WriteLine($"Проверяем можем ли мы приготовить: {recipe.Key}");

                    _storage.VerifyIngredientsAvailability(recipe.Value);
                    ingredientsToRecipe.Add(recipe.Key, recipe.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return ingredientsToRecipe;
        }
    }
}
