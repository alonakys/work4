using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace work4
{
    internal class Article
    {
        public string Magazine { get; set; }
        public Author AuthorDTO { get; set; }
        public string Title { get; set; }
        public string Pages { get; set; }
        public string Fee { get; set; }

        public List<Article> articleList { get; set; }

        public Article()
        {
            articleList = new List<Article>();
        }

        public Article(string magazine, Author author, string title, string pages, string fee)
        {
            Magazine = magazine;
            AuthorDTO = author;
            Title = title;
            Pages = pages;
            Fee = fee;
        }

        public void SaveToJSON(string fileName)
        {
            var articleData = new
            {
                Magazine,
                Author = new { AuthorDTO.FirstName, AuthorDTO.LastName, BirthDate = AuthorDTO.BirthDate.ToShortDateString() },
                Title,
                Pages,
                Fee
            };

            var jsonString = JsonSerializer.Serialize(articleData);

            using (StreamWriter streamWriter = new StreamWriter(fileName, append: true))
            {
                streamWriter.WriteLine(jsonString);
            }
        }
                
        public void LoadFromJSON(string line)
        {

            using (JsonDocument document = JsonDocument.Parse(line))
            {
                JsonElement root = document.RootElement;
                string magazine = root.GetProperty("Magazine").GetString();

                JsonElement authorData = root.GetProperty("Author");
                string firstName = authorData.GetProperty("FirstName").GetString();
                string lastName = authorData.GetProperty("LastName").GetString();
                string birthDate = authorData.GetProperty("BirthDate").GetString();

                string title = root.GetProperty("Title").GetString();
                string pages = root.GetProperty("Pages").GetString();
                string fee = root.GetProperty("Fee").GetString();
                Magazine = magazine;
                AuthorDTO = new Author(firstName, lastName, DateTime.Parse(birthDate));
                Title = title;
                Pages = pages;
                Fee = fee;

                var newArticle = new Article
                {
                    Magazine = Magazine,
                    AuthorDTO = AuthorDTO,
                    Title = Title,
                    Pages = Pages,
                    Fee = Fee
                };

                articleList.Add(newArticle);
            }

        }
    }
}
