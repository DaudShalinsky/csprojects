using Newtonsoft.Json;

public class Storage
{
    const string INGREDIENTS_PATH = "ingredients.json";

    //Приватный список ингредиентов, где хранятся все ингредиенты на складе.
    public List<Ingredient> _allingredients = new List<Ingredient>();

    //Сериализует текущий список ингредиентов и сохраняет его в файл.
    public void SaveIngredients()
    {
        var serializedIngredients = JsonConvert.SerializeObject(_allingredients);
        File.WriteAllText(INGREDIENTS_PATH, serializedIngredients);
    }

    //Загружает ингредиенты из файла. Если файл существует, десериализует данные и обновляет список ингредиентов.
    public void LoadIngredients()
    {
        if (File.Exists(INGREDIENTS_PATH))
        {
            var serializedIngredients = File.ReadAllText(INGREDIENTS_PATH);
            _allingredients = JsonConvert.DeserializeObject<List<Ingredient>>(serializedIngredients);
            return;
        }
            Console.WriteLine("Такого файла не существует!");
        
    }
    internal Storage()
    {
        LoadIngredients();
    }

    // Ищет ингредиент по названию. Возвращает найденный ингредиент или null, если таковой не найден.
    internal Ingredient? FindIngredientByName(string Name)
    {
        return _allingredients.Find(x => x.Name?.ToLower() == Name?.ToLower());
    }

    //Возвращает ингредиент по названию. Вызывает ошибку, если ингредиент не найден.
    internal Ingredient GetIngredientByName(string Name)
    {
        try
        {
            return _allingredients.First(x => x.Name.ToLower() == Name.ToLower());
        }
        catch
        {
            throw new Exception("Ингредиент не найден");
        }
    }

    // Добавляет один ингредиент на склад. Если ингредиент уже есть на складе, увеличивает его количество.
    // После добавления, сохраняет текущее состояние склада в файл.
    internal void AddIngredient(Ingredient ingredient)
    {
        var existingIngredient = FindIngredientByName(ingredient.Name);
        if (existingIngredient != null)
        {
            existingIngredient.Quantity += ingredient.Quantity;
        }
        else
        {
            _allingredients.Add(ingredient);
        }
        SaveIngredients();
    }

    //Позволяет добавить список ингредиентов на склад. 
    //Для каждого ингредиента в списке вызывается метод AddIngredient(Ingredient).
    internal void AddIngredients(List<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            AddIngredient(ingredient);
        }
    }

    internal void VerifyIngredientsAvailability(Dictionary<string, int> neededIngredients)
    {

        foreach (var ingredient in neededIngredients)
        {
            Ingredient? existingIngredient = FindIngredientByName(ingredient.Key);
            if (existingIngredient == null)
            {
                throw new Exception("Ингредиент не найден.");
            }
            else if (existingIngredient.Quantity > ingredient.Value)
            {
                throw new Exception("Недостаточное количество");
            }
        }
    }

    internal List<Ingredient> TakeIngredients(Dictionary<string, int> neededIngredients)
    {
        VerifyIngredientsAvailability(neededIngredients);
        List<Ingredient> ingredientsToReturn = new List<Ingredient>();
        foreach (var i in neededIngredients)
        {
            string ingredientName = i.Key;
            int ingredientQuanity = i.Value;
            Ingredient getIngredient = GetIngredientByName(ingredientName);
            getIngredient.Quantity = getIngredient.Quantity - ingredientQuanity;
            Ingredient newIngredient = new Ingredient
            {
                Name = getIngredient.Name,
                Cost = getIngredient.Cost,
                Quantity = ingredientQuanity,
            };
            ingredientsToReturn.Add(newIngredient);

        }
        SaveIngredients();
        return ingredientsToReturn;
    }
}