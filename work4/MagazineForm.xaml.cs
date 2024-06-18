using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace work4
{
    /// <summary>
    /// Логика взаимодействия для MagazineForm.xaml
    /// </summary>
    public partial class MagazineForm : Window
    {
        Magazine magazine = new Magazine();
        public string magazines = "";
        private string filePathMagazine = @"C:\Users\User\source\repos\work4\magazine.json";

        public MagazineForm()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            magazines = textBox.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(magazines))
            {
                MainWindow.isDataSaved = true;
                MessageBox.Show("Виникла помилка!");
            }
            else
            {
                MainWindow.isDataSaved = true;
                magazine.SaveToJSON(filePathMagazine, magazines);
                MessageBox.Show("Дані були успішно внесені!");
                this.Close();
                MainWindow.isDataSaved = false;
            }
        }

        private void TextBox_LimitTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 30)
            {
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Window_Closing(sender, e);
        }
    }
}
