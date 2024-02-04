using Newtonsoft.Json;

namespace CakesLibrary.Models
{
    public class Storage
    {
        private List<Ingredient> _allIngredients = new List<Ingredient>();

        public Storage()
        {
            LoadIngredients();
        }

        // Метод для добавления ингредиентов в склад
        public void AddIngredients(List<Ingredient> ingredients)
        {
            foreach (var ingredientToAdd in ingredients)
            {
                AddIngredients(ingredientToAdd);
            }
        }

        // Метод для добавления ингредиента в склад
        public void AddIngredients(Ingredient ingredient)
        {
            Ingredient? existingIngredient = FindIngredientByName(ingredient.Name);
            if (existingIngredient != null)
            {
                existingIngredient.Quantity += ingredient.Quantity;
            }
            else
            {
                _allIngredients.Add(ingredient);
            }

            SaveIngredients();
        }

        // Метод для взятия необходимых ингредиентов в нужном количестве
        public List<Ingredient> TakeIngredients(Dictionary<string, int> neededIngredients)
        {
            // Проверяем есть ли нужные ингредиенты в нужном количестве
            VerifyIngredientsAvailability(neededIngredients);

            // Берём необходимое количество ингредиентов в виде копий объектов на складе
            var ingredientsToReturn = new List<Ingredient>();
            foreach (var neededIngredient in neededIngredients)
            {
                // Берем нужный ингредиент
                Ingredient foundIngredient = GetIngredientByName(neededIngredient.Key)!;

                // Обновляем количество ингредиента на складе
                foundIngredient.Quantity -= neededIngredient.Value;

                // Добавляем в новый список для того чтобы вернуть
                var ingredientToReturn = new Ingredient(neededIngredient.Key, neededIngredient.Value, foundIngredient.Cost);
                ingredientsToReturn.Add(ingredientToReturn);
            }

            // Cохраняет состояние склада,
            SaveIngredients();

            // Возвращаем нужные ингредиенты
            return ingredientsToReturn;
        }

        // Метод для проверки доступности на Складе указанных ингредиентов и их количества
        public void VerifyIngredientsAvailability(Dictionary<string, int> neededIngredients)
        {
            foreach (var neededIngredient in neededIngredients)
            {
                Ingredient? foundIngredient = FindIngredientByName(neededIngredient.Key);
                if (foundIngredient == null)
                {
                    throw new Exception($"Нет у нас такого ингредиента {neededIngredient.Key}");
                }
                else if (foundIngredient.Quantity < neededIngredient.Value)
                {
                    throw new Exception($"Недостаточное количество ингредиента {neededIngredient.Key}");
                }
            }
        }

        // Метод для загрузки данных о текущих объектов на складе
        public void LoadIngredients()
        {
            if (File.Exists(Constants.PATH_INGREDIENTS))
            {
                string serializedData = File.ReadAllText(Constants.PATH_INGREDIENTS);
                List<Ingredient> loadedIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(serializedData);

                if (loadedIngredients != null)
                {
                    _allIngredients = loadedIngredients;
                }
            }
        }

        // Метод для сохранения данных о текущих объектов на складе
        private void SaveIngredients()
        {
            string serializedIngredients = JsonConvert.SerializeObject(_allIngredients, Formatting.Indented);
            File.WriteAllText(Constants.PATH_INGREDIENTS, serializedIngredients);
        }

        // Метод для поиска ингредиента, если не найдется вернет null
        private Ingredient? FindIngredientByName(string name)
        {
            return _allIngredients.Find(i => i.Name.ToLower() == name.ToLower());
        }

        // Метод для взятия ингредиента, если такого элемента нет, выкинет ошибку
        private Ingredient GetIngredientByName(string name)
        {
            return _allIngredients.First(i => i.Name.ToLower() == name.ToLower());
        }
        
        public List<Ingredient> GetAllIngredients()
        {
            return _allIngredients;
        }
    }
}
