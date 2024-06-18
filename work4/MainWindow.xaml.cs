using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace work4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Periodicity
        {
            Weekly,
            Monthly,
            Quarterly
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Article> articles;
        Magazine magazine = new Magazine();
        public static bool isDataSaved = false;
        List<string> magazineList = new List<string>();
        private string filePathArticle = @"C:\Users\User\source\repos\work4\article.json";
        private string filePathMagazine = @"C:\Users\User\source\repos\work4\magazine.json";

        private void LoadButton_Click(object sender, RoutedEventArgs e)     // Завантажити дані в ListBox
        {
            articles = magazine.GetArticlesFull(filePathArticle);
            ListBox.Items.Clear();
            foreach (var article in articles)
            {
                string itemText = $"Журнал: {article.Magazine}\n\t" +
                                      $"Автор:\n\t\t" +
                                         $"Ім'я: {article.AuthorDTO.FirstName}\n\t\t" +
                                         $"Прізвище: {article.AuthorDTO.LastName}\n\t\t" +
                                         $"Дата народження: {article.AuthorDTO.BirthDate.ToShortDateString()}\n\t" +
                                      $"Стаття: {article.Title}\n\t\t" +
                                         $"Кількість сторінок: {article.Pages}\n\t\t" +
                                         $"Гонорар ($): {article.Fee}\n";

                ListBox.Items.Add(itemText);
            }
        }

        private void AddArticleButton_Click(object sender, RoutedEventArgs e)     // Створити статтю
        {
            AuthorForm autorForm = new AuthorForm();
            autorForm.ShowDialog();
        }

        private void CreateMagazineButton_Click(object sender, RoutedEventArgs e)     // Створити журнал
        {
            MagazineForm magazine = new MagazineForm();
            magazine.ShowDialog();
        }

        // -------------------- Методи редагування --------------------

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedIndex != -1)
            {
                EditButton.IsEnabled = true;
            }
            else
            {
                EditButton.IsEnabled = false;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListBox.SelectedIndex;
            Article selectedArticle = articles[selectedIndex];
            EditingForm editForm = new EditingForm(selectedArticle.Title, selectedArticle.Pages, selectedArticle.Fee, selectedArticle.AuthorDTO.FirstName, selectedArticle.AuthorDTO.LastName);
            editForm.ShowDialog();
            selectedArticle.Title = editForm.SavedTitle;
            selectedArticle.Pages = editForm.SavedPages;
            selectedArticle.Fee = editForm.SavedFee;
            selectedArticle.AuthorDTO.FirstName = editForm.SavedFirstName;
            selectedArticle.AuthorDTO.LastName = editForm.SavedLastName;

            SaveData();
        }


        private void SaveData()
        {
            using (StreamWriter streamWriter = new StreamWriter(filePathArticle))
            {
                foreach (var article in articles)
                {
                    var articleData = new
                    {
                        article.Magazine,
                        Author = new { article.AuthorDTO.FirstName, article.AuthorDTO.LastName, BirthDate = article.AuthorDTO.BirthDate.ToShortDateString() },
                        article.Title, 
                        article.Pages,
                        article.Fee
                    };

                    string jsonString = JsonSerializer.Serialize(articleData);
                    streamWriter.WriteLine(jsonString);
                }
            }

            LoadButton_Click(sender: null, e: null);
        }

        // -------------------- Правила для вписування --------------------

        public static void ValidateTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length >= 25 || !char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        public static void ValidateNumInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
                return;
            }

            string textBoxText = ((TextBox)sender).Text;
            if (textBoxText.Length >= 10 && !textBoxText.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void DownloadAbbreviatedInformationButton_Click(object sender, RoutedEventArgs e)   // Скорочена інформація (кількість сторінок, журнал, і як часто вони виходять)
        {
            magazineList.Clear();
            magazine.magazinesList.Clear();
            magazine.LoadFromJSON(filePathMagazine);
            articles = magazine.GetArticlesFull(filePathArticle);

            foreach (var magazineItem in magazine.magazinesList)
            {
                int countPages = 0;
                int countRegularities = 0;
                foreach (var article in articles)
                {
                    if (article.Magazine == magazineItem)
                    {
                        countPages += int.Parse(article.Pages);
                        countRegularities++;
                    }
                }

                string periodicity;

                if (countRegularities >= 0 && countRegularities <= 2)
                {
                    periodicity = Periodicity.Quarterly.ToString();
                }
                else if (countRegularities > 2 && countRegularities <= 4)
                {
                    periodicity = Periodicity.Monthly.ToString();
                }
                else
                {
                    periodicity = Periodicity.Weekly.ToString();
                }

                string fullInf = $"Жрнал: {magazineItem} - {countPages}c. \n  Випускається: {periodicity}";
                magazineList.Add(fullInf);
            }

            MessageBox.Show(string.Join("\n", magazineList), "Список журналів");
        }

        public static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)   // Закриття форми
        {
            if (!isDataSaved)
            {
                MessageBoxResult result = MessageBox.Show("Бажаєте закрити форму без збереження?", "Попередження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
