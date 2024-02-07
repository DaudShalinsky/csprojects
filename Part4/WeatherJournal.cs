using ConsoleUtils;
using JournalLibrary;
using JournalLibrary.Models;

public class WeatherJournal
{
    const string PATH = @"Weather.txt";
    const string DATE_FORMAT = "dd.MM.yyyy";



    private static int? SelectMode()
    {
        // Просим ввести
        Console.Clear();
        Console.WriteLine("Выберите режим: ");
        Console.WriteLine("1 - Чтение");
        Console.WriteLine("2 - Запись");

        try
        {
            // Пытаемся получить числовое значение
            int? mode = Convert.ToInt32(Console.ReadLine());

            if (mode != 1 && mode != 2)
            {
                // Если ошибка, запускаем текущий метод, заново
                return SelectMode();
            }

            // Если код дошел сюда, возвращаем значение выбранного режима
            return mode;
        }
        catch (Exception ex)
        {
            // Если ошибка, запускаем текущий метод, заново
            return SelectMode();
        }
    }

    private static string GetDiscription()
    {
        Console.Write("Описание: ");
        string? description = Console.ReadLine();

        bool flajok = false;

        string[] descriptionWords = { "солнечно", "облачно", "дождь", "снег", "туман" };

        //foreach (string word in descriptionWords)
        //{
        if (descriptionWords.Contains(description.ToLower()))
        {
            return description;
        }
        else
        {
            return GetDiscription();
        }
        // }

    }
    public static void Start()
    {
        JournalStorage journalStorage = new JournalStorage();
        // Выбор режима
        int? mode = SelectMode();

        switch (mode)
        {
            case 1:
                Console.WriteLine("Введите дату:");
                string? selectedDate = Console.ReadLine();
                string? contents;
                try
                {
                    contents = File.ReadAllText(PATH);
                }
                catch (Exception)
                {
                    Console.WriteLine("Не удалось прочитать файл, проверь свой компьютер, очисти от пыли");
                    Console.ReadKey();
                    Start();
                    return;
                }

                string[] entries = contents.Split("\r\n");

                bool found = false;

                for (int i = 0; i < entries.Length - 1; i++)
                {
                    string entry = entries[i];

                    if (entry.Contains(selectedDate))
                    {
                        found = true;
                        string[] data = entry.Split(" ");
                        Console.WriteLine($"Дата: {data[0]}, Погода: {data[3]}, Температура: {data[1]}°C, Влажность: {data[2]}%.");
                    }
                }

                if (found == false)
                {
                    Console.WriteLine("Нет данных.");
                }

                Console.ReadKey();
                Start();

                break;
            case 2:
                Console.WriteLine("Введите информацию о погоде:");

                DateTime date = (DateTime)InputService.GetDate()!;
                decimal temperature = InputService.GetTemperature();
                decimal humidity = InputService.GetHumidity();
                string description = GetDiscription();

                JournalEntry newEntry = new JournalEntry()
                {
                    Date = date,
                    Description = description,
                    Humidity = humidity,
                    Temperature = temperature,
                };

                try
                {
                    journalStorage.AddJournalEntry(newEntry);
                    Console.WriteLine("Строка успешно добавлена в файл");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
                Start();

                break;
        }
    }
}
