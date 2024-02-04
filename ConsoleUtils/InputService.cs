using System.Security.Cryptography.X509Certificates;

namespace ConsoleUtils
{
    public class InputService
    {
        public static char GetOption(string message, Dictionary<char, string> options)
        {
            Console.WriteLine(message);

            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            try
            {
                // Пытаемся получить числовое значение
                ConsoleKeyInfo input = Console.ReadKey(true);
                Console.Clear();
                char mode = input.KeyChar;

                if (!options.ContainsKey(mode) && mode != '\r')
                {
                    // Если ошибка, запускаем текущий метод, заново
                    return GetOption(message, options);
                }

                // Если код дошел сюда, возвращаем значение выбранного режима
                return mode;
            }
            catch (Exception)
            {
                // Если ошибка, запускаем текущий метод, заново
                return GetOption(message, options);
            }
        }
        public static DateTime? GetDate() // метод для ввода даты.
        {
            string? rawDate = Console.ReadLine();
            DateTime date;
            bool dateOk = DateTime.TryParse(rawDate, out date);
            if (!dateOk)
            {
                Console.WriteLine("Неверный ввод.");
            }
            return date;
        }
        public static string? GetString() // метод для ввода текста.
        {
            return Console.ReadLine();
        }
        public static int? GetInt()
        {
            int result;
            string? input = Console.ReadLine();
            bool parsingResult = int.TryParse(input, out result);
            if (!parsingResult)
            {
                Console.WriteLine("Неверный ввод.");
            }
            return result;
        } // метод для ввода числа.
        public static double? GetDouble() // метод для ввода числа с плавающей точкой.
        {
            double result;
            string? input = Console.ReadLine();
            bool parsingResult = double.TryParse(input, out result);
            if (!parsingResult)
            {
                Console.WriteLine("Неверный ввод.");
            }
            return result;
        }
    }
}