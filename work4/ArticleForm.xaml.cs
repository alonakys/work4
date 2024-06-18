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
    /// Логика взаимодействия для ArticleForm.xaml
    /// </summary>
    public partial class ArticleForm : Window
    {
        private Magazine magazine;
        private Author author;
        private string selectedMagazine = "";
        private string title = "";
        private string pages = "";
        private string fee = "";
        private string filePathMagazine = @"C:\Users\User\source\repos\work4\magazine.json";
        /*        private string filePathAuthor = @"C:\Users\Legion\Desktop\VS\OOP4\author.json";*/
        private string filePathArticle = @"C:\Users\User\source\repos\work4\article.json";

        public ArticleForm(Author author)
        {
            InitializeComponent();
            this.author = author;
            magazine = new Magazine();
            magazine.LoadFromJSON(filePathMagazine);
            foreach (var magazine in magazine.magazinesList)
            {
                MagazinesComboBox.Items.Add(magazine);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            title = textBox.Text;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            pages = textBox.Text;
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            fee = textBox.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedMagazine) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(pages) || string.IsNullOrWhiteSpace(fee))
            {
                MainWindow.isDataSaved = true;
                MessageBox.Show("Виникла помилка!");
            }
            else
            {
                MainWindow.isDataSaved = true;
                Article article = new Article(selectedMagazine, author, title, pages, fee);
                article.SaveToJSON(filePathArticle);
                MessageBox.Show("Дані були успішно внесені!");
                this.Close();
                MainWindow.isDataSaved = false;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMagazine = (string)MagazinesComboBox.SelectedItem;
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
