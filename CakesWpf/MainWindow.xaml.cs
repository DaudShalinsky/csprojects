using CakesLibrary.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CakesWpf
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();
        public ObservableCollection<string> Recipes { get; set; } = new ObservableCollection<string>();

        private Storage _storage;
        private Kitchen _kitchen;

        private string _selectedCakeName;
        private List<Ingredient> _selectedIngredients;


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            _storage = new Storage();
            _kitchen = new Kitchen(_storage);
            _kitchen.CakeReady += _kitchen_CakeReady;

            UpdateIngredientsView();
            UpdateRecipesView();
        }

        private void _kitchen_CakeReady(Cake newCake)
        {
            MessageBox.Show($"Ura {newCake.Name} готов, он стоит {newCake.Price} рублей");
            UpdateRecipesView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = new Ingredient(txtName.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToDecimal(txtCost.Text));
            _storage.AddIngredients(ingredient);
            UpdateIngredientsView();
        }

        private void btnTakeOrder_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(_selectedCakeName))
            {
                MessageBox.Show("Вы не ввели название");
            }
            else if (!Recipes.Contains(_selectedCakeName))
            {
                MessageBox.Show("Такой мы не можем сделать, пожалуйста выберите из списка");
            }
            else
            {
                MessageBox.Show($"Заказ принят {_selectedCakeName}");
                try
                {
                    _kitchen.MakeCake(_selectedCakeName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateRecipesView();
                _selectedCakeName = "";
            }
        }

        private void UpdateRecipesView()
        {
            Recipes.Clear();
            var avaibleRecipes = _kitchen.GetAvailableRecipes();

            foreach (var recipe in avaibleRecipes.Keys)
            {
                Recipes.Add(recipe);
            }
        }
        private void UpdateIngredientsView()
        {
            Ingredients.Clear();
            var availableIngredients = _storage.GetAllIngredients();
            foreach (var ingredient in availableIngredients)
            {
                Ingredients.Add((Ingredient)ingredient);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            _selectedCakeName = e.AddedItems[0]!.ToString()!;


        }

        private void lstIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            _selectedIngredients = new List<Ingredient>();
            foreach (var item in e.AddedItems)
            {
                var ingredient = item as Ingredient;
                _selectedIngredients.Add(ingredient);
            }
        }
    }
}