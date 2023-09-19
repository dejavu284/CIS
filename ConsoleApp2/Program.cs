using ConsoleApp2.Classes;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Film df = new Film(
                name = "fas",
                description = "dafs",
                actors = new List<string>,
                genre = new List<string>);
        }
    }
}