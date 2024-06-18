using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace work4
{
    class Magazine
    {
        public List<Article> articles { get; private set; }
        public List<string> magazinesList { get; private set; }

        public Magazine()
        {
            magazinesList = new List<string>();
            articles = new List<Article>();
        }

        public void SaveToJSON(string fileName, string magazine)
        {
            var authorData = new
            {
                Magazine = magazine
            };
            var jsonString = JsonSerializer.Serialize(authorData);

            using (StreamWriter streamWriter = new StreamWriter(fileName, append: true))
            {
                streamWriter.WriteLine(jsonString);
            }
        }

        public void LoadFromJSON(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(line);
                if (jsonObject.TryGetValue("Magazine", out string magazine))
                {
                    magazinesList.Add(magazine);
                }
            }
            return;
        }
        public List<Article> GetArticlesFull(string filePath)
        {
            List<Article> articles = new List<Article>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    Article article = new Article(); // Створюємо новий екземпляр Article для кожного рядка JSON
                    article.LoadFromJSON(line);
                    articles.Add(article);
                }
                return articles;
            }
            else
            {
                MessageBox.Show("Наразі немає даних для виведення!");
            }

            return articles;

        }
    }
}
