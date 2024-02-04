using CakesLibrary.Models;
using ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CakesAdvanced.Models
{
    public class Store
    {
        private readonly Kitchen _kitchen;
        private readonly Storage _storage;

        public string Name { get; }
        public Store(string name)
        {
            Name = name;

            _storage = new Storage();
            _kitchen = new Kitchen(_storage);
            _kitchen.CakeReady += _kitchen_CakeReady;
        }

        private void _kitchen_CakeReady(Cake cake)
        {
            Console.WriteLine($"{cake.Name} готов");
            Console.WriteLine($"он стоит {cake.Price} рублей");
        }

        public void Open()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Марша вог1ийла");
            Console.ForegroundColor = ConsoleColor.White;

            Dictionary<char, string> options = new Dictionary<char, string>{
                { '1', "Менеджер" },
                { '2', "Клиент" }
            };
            char selectedOption = InputService.GetOption("Выберите режим", options);

            switch (selectedOption)
            {
                case '1':
                    ShowManagerOptions();
                    break;
                case '2':
                    ShowClientOptions();
                    break;
                default:
                    Open();
                    return;
            }

            Console.ReadLine();
            Open();
        }
        internal void ShowManagerOptions()
        {
            Dictionary<char, string> options = new Dictionary<char, string>{
                { '1', "Добавить ингредиенты" },
                { '2', "Показать список имеющихся ингредиентов" }

            };
            char selectedOption = InputService.GetOption("Выберите дествие", options);

            switch (selectedOption)
            {
                case '1':
                    AddIngredients();
                    break;

                case '2':
                    ShowIngredients();
                    break;
                default:
                    Open();
                    return;
            }
        }

        private void AddIngredients()
        {
            Console.WriteLine("Введите игредиенты в формате \"Название:Цена:Количество\"");
            Console.WriteLine("Например \"Орехи:150:10, Мука:200:2\" и тп");
            string? userInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Вы ничего не ввели!");
                Console.ReadKey();
                Open();
                return;
            }

            // Даже если запятой нет, т.е. только один элемент, он будет помещен в массив
            string[] rawIngredients = userInput.Split(",");

            var ingredientsToAdd = new List<Ingredient>();
            try
            {
                // Создаем из строковой информации объекты типа Ingredient
                foreach (var rawIngredient in rawIngredients)
                {
                    string[] rawIngredientParts = rawIngredient.Trim().Split(":");

                    if (rawIngredientParts.Length != 3)
                    {
                        Console.WriteLine($"Неправильный ввод ингредиента {rawIngredient}! " +
                            $"Введите в соответствии с примером!");
                        continue;
                    }

                    string name = rawIngredientParts[0].Trim();
                    decimal cost = decimal.Parse(rawIngredientParts[1]);
                    int quantity = int.Parse(rawIngredientParts[2]);

                    Ingredient ingredient = new Ingredient(name, quantity, cost);
                    ingredientsToAdd.Add(ingredient);
                }

                // Чтобы в случае ошибки не добавлять ничего, добавляем когда всё спарсено
                _storage.AddIngredients(ingredientsToAdd);
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, восстанавливаем ранее сохраненные игредиенты
                _storage.LoadIngredients();
            }
        }



        public void ShowClientOptions()
        {
            Dictionary<char, string> options = new Dictionary<char, string>{
                { '1', "Показать список возможных тортов" },
                { '2', "Заказать новый торт" }
            };
            char selectedOption = InputService.GetOption("Выберите дествие", options);
            Console.Clear();

            switch (selectedOption)
            {
                case '1':
                    ShowAvailableCakeOptions();
                    break;
                case '2':
                    TakeOrder();
                    break;
                default:
                    Open();
                    return;
            }
        }

        private void ShowAvailableCakeOptions()
        {
            var available = _kitchen.GetAvailableRecipes();
            if (available.Count == 0)
            {
                Console.WriteLine("К сожалению мы не можем ничего приготовить :(");
                return;
            }

            foreach (var recipe in available)
            {
                Console.WriteLine($"{recipe.Key}");
            }
        }
        private void TakeOrder()
        {
            Console.Write("Напишите название нужного торта: ");
            string cakeName = InputService.GetString();
            Console.WriteLine(cakeName);
            if (cakeName == null)
            {
                Console.WriteLine("Цхьа нормальны движени язйе");
                return;
            }

            try
            {
                Console.Write("Готовим, ждите!");
                _kitchen.MakeCake(cakeName);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ShowIngredients()
        {
            var allIngredients = _storage.GetAllIngredients();

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("      Ингредиенты на складе ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------");
            Console.WriteLine("| Название |  Цена  | Количество |");
            foreach (var ingredient in allIngredients)
            {
                Console.Write("| ");
                Console.Write(ingredient.Name.PadRight(9));
                Console.Write("|  ");
                Console.Write(ingredient.Cost.ToString().PadRight(6));
                Console.Write("| ");
                Console.Write(ingredient.Quantity.ToString().PadRight(10));
                Console.Write(" |");
                Console.WriteLine();

            }
            Console.WriteLine("----------------------------------");
        }
    }
}
