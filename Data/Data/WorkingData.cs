using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Model.Models;

namespace Data.Data
{
    public class WorkingData
    {
        public WorkingData(string[] args)
        {
            CurrentDirectory = $"{Environment.CurrentDirectory}";
            if (DataIsCorrect(args))
            {
                Args = args;
            }
        }
        private string[] Args;
        private string CurrentDirectory { get; set; }
        private string CinemasJsonPath { get {return CurrentDirectory + "\\Data\\" + Args[0]; } }
        private string BasketJsonPath { get { return CurrentDirectory + "\\Data\\" + Args[1]; } }

        public static bool DataIsCorrect(string[] args)
        {
            if (args.Length == 2)
            {
                return args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public List<Cinema> CinemasDeserializ()
        {
            try
            {
                string textJson = File.ReadAllText(CinemasJsonPath);
                List<Cinema> element = JsonSerializer.Deserialize<List<Cinema>>(textJson)!;
                return element;
            }
            catch (FileNotFoundException)
            {
                throw new DataException("Не найден файл", CinemasJsonPath);
            }
            catch (JsonException)
            {
                throw new DataException("Ошибка в файле", CinemasJsonPath);
            }
            catch
            {
                throw new DataException("Ошибка", CinemasJsonPath);
            }
        }


        public void Save<T>(T element)
        {
            string path;
            if (element is Cinema)
                path = CinemasJsonPath;
            else if (element is Basket)
                path = BasketJsonPath;
            else
                path = CurrentDirectory;

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
