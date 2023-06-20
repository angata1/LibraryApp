using DustInTheWind.ConsoleTools.Controls.Tables;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace BooksClasses
{

    public class Root
    {
        public string title { get; set; }
        public string author { get; set; }
        public object rating { get; set; }
        public object voters { get; set; }
        public double price { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string publisher { get; set; }
        public object page_count { get; set; }
        public string generes { get; set; }
        public object ISBN { get; set; }
        public string language { get; set; }
        public object published_date { get; set; }
    }
    public class BookClass
    {
        public List<Root> bookinfo;



        public BookClass()
        {
            bookinfo = JsonConvert.DeserializeObject<List<Root>>(File.ReadAllText(@"books.json"));
        }
        public void AddBook(string title, string author, string description = "", double rating = 0.0, int voters = 0, double price = 0.0, string currency = "", string publisher = "", string language = "", string genres = "", int page_count = 0)
        {
            Root newBook = new Root();
            newBook.author = author;
            newBook.title = title;
            newBook.description = description;
            newBook.rating = rating;
            newBook.voters = voters;
            newBook.price = price;
            newBook.currency = currency;
            newBook.language = language;
            newBook.language = language;
            newBook.publisher = publisher;
            newBook.generes = genres;
            newBook.ISBN = 0;
            newBook.published_date = "";


            string json = JsonConvert.SerializeObject(newBook, Newtonsoft.Json.Formatting.Indented);
            string alljson = File.ReadAllText(@"books.json");

            alljson = alljson.Remove(alljson.LastIndexOf(']'), 1);

            alljson = alljson + "," + json + "]";




            File.WriteAllText(@"books.json", alljson);
            bookinfo = JsonConvert.DeserializeObject<List<Root>>(File.ReadAllText(@"books.json"));
            Console.WriteLine("Book list updated");
        }

        public void SearchTitle(string title)
        {

            List<Root> resList = bookinfo.FindAll(x => x.title == title);



            DataGrid dataGrid = new DataGrid($"Search By Title Results\nTotal count: {resList.Count}");
            dataGrid.DisplayColumnHeaders = true;
            dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            //dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.Columns.Add("book index");
            dataGrid.Columns.Add("Title");
            dataGrid.Columns.Add("Author");
            dataGrid.Columns.Add("Rating");
            //dataGrid.Columns.Add("Publisher");
            dataGrid.Columns.Add("Genre");




            foreach (Root book in resList)
            {
                dataGrid.Rows.Add(bookinfo.IndexOf(book), book.title, book.author, book.rating.ToString(), book.generes);

            }

            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.FooterRow.IsVisible = true;

            dataGrid.Display();

        }

        public void SearchByGenre(string genre)
        {
            List<Root> resList = new List<Root>();

            Regex g = new Regex($@"\b{Regex.Escape(genre)}\b", RegexOptions.IgnoreCase);
            MatchCollection matches;

            for (int i = 0; i < bookinfo.Count; i++)
            {
                matches = g.Matches(bookinfo[i].generes);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        resList.Add(bookinfo[i]);
                        break;
                    }
                }
            }

            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.WriteLine("Search results:");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //for (int i = 0; i < resList.Count; i++)
            //{
            //    Console.ForegroundColor = ConsoleColor.Black;
            //    Console.BackgroundColor = ConsoleColor.White;
            //    Console.WriteLine($"{i}.");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //    Console.Write($" Title: {resList[i].title}, Author: {resList[i].author}, Rating: {resList[i].rating}, Publisher: {resList[i].publisher}, Genre: {resList[i].generes}\n");
            //}

            DataGrid dataGrid = new DataGrid($"Search By Genre Results\nTotal count: {resList.Count}");
            dataGrid.DisplayColumnHeaders = true;
            //dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            dataGrid.Columns.Add("Title");
            dataGrid.Columns.Add("Author");
            dataGrid.Columns.Add("Rating");
            //dataGrid.Columns.Add("Publisher");
            dataGrid.Columns.Add("Genre");


            foreach (Root book in resList)
            {
                dataGrid.Rows.Add(book.title, book.author, book.rating.ToString(), book.generes.Trim());

            }

            dataGrid.Display();
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Remove(string title)
        {
            for (int i = 0; i < bookinfo.Count; i++)
            {
                if (bookinfo[i].title == title)
                {
                    bookinfo.RemoveAt(i);
                }
            }
            string json = JsonConvert.SerializeObject(bookinfo, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(@"books.json", json);

        }
        public void Remove(int number)
        {
            Console.WriteLine($"The book {bookinfo[number].title} was successfully removed");
            bookinfo.RemoveAt(number);
            string json = JsonConvert.SerializeObject(bookinfo, Newtonsoft.Json.Formatting.Indented);


            File.WriteAllText(@"books.json", json);


        }
    }
}
