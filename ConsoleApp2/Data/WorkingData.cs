using CIS.Models;
using CIS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CIS.Data
{
    internal class WorkingData
    {
        public WorkingData(string[] args)
        {
            if (DataIsCorrect(args))
            {
                Args = args;
                CurrentDirectory = $"{Environment.CurrentDirectory}";
                BasketJsonPath = CurrentDirectory + "\\Data\\" + args[1];
            }
        }    
        private string[] Args { get;}
        private string? CurrentDirectory { get; set; }
        public string? CinemasJsonPath { get {return CurrentDirectory + "\\Data\\" + Args[0]; } }
        public string? BasketJsonPath { get; private set; }

        public static bool DataIsCorrect(string[] args)
        {
            if (args.Length == 2)
            {
                return args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public bool TryDeserializ<T>(string? path, ref T element)
            where T : new()
        {
            try
            {
                string textJson = File.ReadAllText(path);
                element = JsonSerializer.Deserialize<T>(textJson)!;
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Не найден файл:\n{0}", path);
                return false;
            }
            catch (JsonException)
            {
                Console.WriteLine("Ошибка в файле:\n{0}", path);
                return false;
            }
            catch
            {
                Console.WriteLine("Ошибка, попробуйте еще раз.", path);
                return false;
            }
        }

        public static void Save<T>(string? path, T element)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize(element, options); 
            File.WriteAllText(path, jsonString); 
        }
    }
}
