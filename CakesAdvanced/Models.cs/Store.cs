using ConsoleUtils;

public class Store
{
    internal string Name { get; set; }

    public Kitchen _kitchen;
    public Storage _storage;
    internal Store(string name)
    {
        Name = name;
        _storage = new Storage();
        _kitchen = new Kitchen(_storage);

    }

    public void Open()
    {
        string[] option = { "1 - Менеджер","2 - Клиент" };
        int? mode = InputService.GetOption(option);
        switch (mode)
        {
            case 1:
                ShowManagerOptions();
                break;

            case 2:
                ShowClientOptions();
                break;
        }
    }

    public void ShowClientOptions()
    {
        string[] option = { "1. Показать список возможных тортов", "2. Заказать торт" };
        int? mode = InputService.GetOption(option);
        Console.Clear();
        switch (mode)
        {
            case 1:
                // ShowAvailableCakeOptions();
                break;
            case 2:
                //TakeOrder();
                break;
        }
        Open();
        Console.ReadKey();
        Console.Clear();
    }


    public void ShowManagerOptions()
    {
        string[] option = { "1. Добавить ингредиенты" };
        int? mode = InputService.GetOption(option);
        switch (mode)
        {
            case 1:
                AddIngredients();
                break;
        }
        Open();
        Console.ReadKey();
        Console.Clear();
    }

    public void AddIngredients()
    {
        Console.WriteLine("Введите ингредиенты в формате 'Название:Цена:Количество'");
        string? request = Console.ReadLine();
        string[] ingredientSeparately = request.Split(",");
        try
        {
            foreach (string ingredient in ingredientSeparately)
            {
                string[] partIngredients = ingredient.Split(":");
                Ingredient ingredients = new Ingredient()
                {
                    Name = partIngredients[0],
                    Cost = Convert.ToDecimal(partIngredients[1]),
                    Quantity = Convert.ToInt32(partIngredients[2]),
                };
                _storage.AddIngredient(ingredients);
            }
        }
        catch (Exception)
        {
            _storage.LoadIngredients();
        };
    }

    public Dictionary<string, Dictionary<string, int>> ShowAvailableCakeOptions()
    {
        Dictionary<string, Dictionary<string, int>> avaibleRecipes = _kitchen.GetAvailableRecipes();
        if(avaibleRecipes == null)
        {
            Console.WriteLine("Нет доступных рецептов");
        }
        Console.WriteLine(string.Join(",", avaibleRecipes));
        return avaibleRecipes;
    }

   public Cake TakeOrder()
    {
        Console.WriteLine("Напишите название торта который вы хотеть: ");
        string cake = Console.ReadLine();
        if ( cake == null)
        {
            Console.WriteLine("Вы не ввели название торта!");
        }
        try 
        {
            var newCake = _kitchen.MakeCake(cake);
            Console.WriteLine($"{newCake.Name} {newCake.Price}");
            return newCake;
        }
        catch (Exception)
        {
            throw new Exception("Хьун торт ца ез, хьай диети т1е хаъ!");
        }
        
    }

}