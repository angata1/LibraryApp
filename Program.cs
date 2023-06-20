using BooksClasses;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Spinners;
using Newtonsoft.Json;

namespace LibraryAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(500, 1000);
            BookClass books = new BookClass();

            Console.WriteLine("\nLIBRARY");
            HorizontalLine line = new HorizontalLine();
            line.Display();
            //books.AddBook("The Art of War","Sun Tsu", "The Art of War is an ancient Chinese military treatise dating from the Late Spring and Autumn Period.",4, genres: "Treatise, Non-fiction");
            books.SearchTitle("The Art of War");
            //books.Remove(1299);
            //books.SearchByGenre("horror");
        }
    }
}