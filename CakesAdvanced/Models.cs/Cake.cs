public class Cake
{
    public string Name { get; set; }
    public decimal Price { get;  set; }

    internal List<Ingredient> _ingredients = new List<Ingredient>();

    internal Cake(string name, List<Ingredient> ingredients)
    {
        Name = name;
        _ingredients = ingredients;

        Price = _ingredients.Sum(i => (i.Cost * 0.5m) * i.Quantity);
    }
}