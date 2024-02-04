namespace CakesLibrary.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; } = 0;
        public Ingredient(string name, int quantity, decimal cost = 0)
        {
            Name = name;
            Quantity = quantity;
            Cost = cost;
        }
    }
}
