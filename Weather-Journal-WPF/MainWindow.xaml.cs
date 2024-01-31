using System.IO;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Weather_Journal_WPF
{

    public partial class MainWindow : Window
    {
        const string PATH = @"Weather.txt";
        const string DATE_FORMAT = "dd.MM.yyyy";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            decimal temperature = Convert.ToDecimal(txtTemperature.Text);
            decimal humidity = Convert.ToDecimal(txtHumidity.Text);
            DateTime date = Convert.ToDateTime(txtData.Text);
            ComboBoxItem selectedItem = (ComboBoxItem)cmbDescription.SelectedItem;
            string description = (string)selectedItem.Content;
            string message = AddLine(temperature, humidity, date, description);
            MessageBox.Show(message);
        }

        private string AddLine(decimal temperature, decimal humidity, DateTime date, string description)
        {
            try
            {
                File.AppendAllText(PATH, $"{date.ToString(DATE_FORMAT)} {temperature} {humidity} {description}\r\n");
            }
            catch (UnauthorizedAccessException)
            {
                return "Нет доступа к указанному пути";
            }
            catch (Exception)
            {
                return "Возникла непонятная ошибка";
            }
            return "Строка успешно добавлена в файл";
        }
    }
}