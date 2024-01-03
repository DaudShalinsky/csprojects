namespace ConsoleUtils
{
    public static class InputService
    {
        const string DateFormat = "DD.mm.YYYY";

        public static int? GetOption(string[] modeDescriptions)
        {
            Console.WriteLine("Выберите режим: ");

            foreach (var modeDescription in modeDescriptions)
            {
                Console.WriteLine(modeDescription);
            }

            try
            {
                // Пытаемся получить числовое значение
                int? mode = Convert.ToInt32(Console.ReadLine());

                if (mode < 0 || mode > modeDescriptions.Length)
                {
                    // Если ошибка, запускаем текущий метод, заново
                    return GetOption(modeDescriptions);
                }

                // Если код дошел сюда, возвращаем значение выбранного режима
                return mode;
            }
            catch (Exception)
            {
                // Если ошибка, запускаем текущий метод, заново
                return GetOption(modeDescriptions);
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

