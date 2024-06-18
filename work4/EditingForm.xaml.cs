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
    /// Логика взаимодействия для EditingForm.xaml
    /// </summary>
    public partial class EditingForm : Window
    {
        public string SavedTitle = "";
        public string SavedPages = "";
        public string SavedFee = "";
        public string SavedFirstName = "";
        public string SavedLastName = "";

        private string CurrentTitle = "";
        private string CurrentPages = "";
        private string CurrentFee = "";
        private string CurrentFirstName = "";
        private string CurrentLastName = "";

        public EditingForm(string title, string pages, string fee, string firstName, string lastName)
        {
            InitializeComponent();
            TextBox_Title.Text = SavedTitle = title;
            TextBox_Pages.Text = SavedPages = pages;
            TextBox_Fee.Text = SavedFee = fee;
            TextBox_FirstName.Text = SavedFirstName = firstName;
            TextBox_LastName.Text = SavedLastName = lastName;

        }
        private void TextBox_TextChanged_Title(object sender, TextChangedEventArgs e)
        {
            CurrentTitle = TextBox_Title.Text;
        }

        private void TextBox_TextChanged_Pages(object sender, TextChangedEventArgs e)
        {
            CurrentPages = TextBox_Pages.Text;
        }

        private void TextBox_TextChanged_Fee(object sender, TextChangedEventArgs e)
        {
            CurrentFee = TextBox_Fee.Text;
        }

        private void TextBox_TextChanged_FirstName(object sender, TextChangedEventArgs e)
        {
            CurrentFirstName = TextBox_FirstName.Text;
        }

        private void TextBox_TextChanged_LastName(object sender, TextChangedEventArgs e)
        {
            CurrentLastName = TextBox_LastName.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Title.Text) || string.IsNullOrEmpty(TextBox_Pages.Text) || string.IsNullOrEmpty(TextBox_Fee.Text) || string.IsNullOrEmpty(TextBox_FirstName.Text) || string.IsNullOrEmpty(TextBox_LastName.Text))
            {
                MainWindow.isDataSaved = true;
                MessageBox.Show("Виникла помилка!");
            }
            else
            {
                MainWindow.isDataSaved = true;
                MessageBox.Show("Дані було успішно відредаговані!");
                SavedTitle = CurrentTitle;
                SavedPages = CurrentPages;
                SavedFee = CurrentFee;
                SavedFirstName = CurrentFirstName;
                SavedLastName = CurrentLastName;
                this.Close();
                MainWindow.isDataSaved = false;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MainWindow.ValidateTextInput(sender, e);
        }

        private void TextBox_PreviewNumInput(object sender, TextCompositionEventArgs e)
        {
            MainWindow.ValidateNumInput(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Window_Closing(sender, e);
        }
    }
}