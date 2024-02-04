namespace CakesLibrary.Models
{
    public class Workshop
    {
        private Dictionary<string, Dictionary<string, int>> _recipes = new Dictionary<string, Dictionary<string, int>>();

        public Workshop()
        {
            _recipes.Add("Medovik", new Dictionary<string, int>()
            {
                ["Орехи"] = 20,
                ["Яйца"] = 3,
                ["Мёд"] = 10
            });

            _recipes.Add("Napoleon", new Dictionary<string, int>()
            {
                ["Мука"] = 1,
                ["Яйца"] = 3,
                ["Молоко"] = 1,
                ["Сахар"] = 2,
            });

            
        }

        public Dictionary<string, Dictionary<string, int>> GetAllRecipes()
        {
            return _recipes;
        }
    }
}
