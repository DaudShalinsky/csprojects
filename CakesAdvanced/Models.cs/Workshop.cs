internal class Workshop
    {
        Dictionary<string, Dictionary<string, int>> _recipes = new Dictionary<string, Dictionary<string, int>>();

        internal Dictionary<string, Dictionary<string, int>> GetAllRecipes() 
        {
            return _recipes; 
        }
        internal Workshop()
        {
            Dictionary<string, int> Medovik = new Dictionary<string, int>()
           {
               {"Мука", 5 },
               {"Мёд", 3 },
               {"Сахар", 5 }
           };
            Dictionary<string, int> Napaleon = new Dictionary<string, int>()
            {
                {"Мука", 10},
                {"Сливочное масло", 7 },
                {"Вода", 1 }
            };
            _recipes.Add("Medovik", Medovik);
            _recipes.Add("Napaleon", Napaleon);
        }
    } 