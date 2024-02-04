namespace CakesLibrary.Models
{
    public class Cake
    {
        public string Name { get; }
        public decimal Price { get; }

        private readonly List<Ingredient> _ingredients;

        public Cake(string name, List<Ingredient> ingredients)
        {
            Name = name;
            _ingredients = ingredients;

            // наценка
            decimal interest = 1.5m;

            Price = _ingredients.Sum(i => i.Cost * interest);
        }
    }
}