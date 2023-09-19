using ConsoleApp2.Classes;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var a = new Film
            (
                "fas",
                "dafs",
                new List<string> { },
                new List<string> { }
            );
            Console.WriteLine("name: ");
            Console.WriteLine(a.name);
            Console.ReadLine();
        }
    }
}