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
    /// Логика взаимодействия для AuthorForm.xaml
    /// </summary>
    public partial class AuthorForm : Window
    {
        private DateTime selectedDate;
        private string firstName = "";
        private string lastName = "";

        public AuthorForm()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            firstName = textBox.Text;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            lastName = textBox.Text;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(firstName))
            {
                MainWindow.isDataSaved = true;
                MessageBox.Show("Виникла помилка!");
            }
            else
            {
                MainWindow.isDataSaved = true;
                Author author = new Author(firstName, lastName, selectedDate);
                ArticleForm articleForm = new ArticleForm(author);
                Window currentWindow = Window.GetWindow(this);
                currentWindow.Close();
                MainWindow.isDataSaved = false;
                articleForm.ShowDialog();
            }
        }

        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = MyDatePicker.SelectedDate ?? DateTime.Now;

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MainWindow.ValidateTextInput(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Window_Closing(sender, e);
        }
        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
