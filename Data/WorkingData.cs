using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using CinemaModel;

namespace Data
{
    public class WorkingData
    {
        public WorkingData(string[] fileNames)
        {
            if (fileNames.Length != 2)
                throw new Exception("Неверное количество переданных загрузочных файлов");
            else if (!fileNames.All(x => x.Contains(".json")))
                throw new Exception("Неверное расширение у загрузочных файлов");
            else
            {
                CurrentDirectory = $"{Environment.CurrentDirectory}";
                FileNames = fileNames;
            }
        }
        private string[] FileNames;
        private string CurrentDirectory { get;}
        private string CinemasJsonPath { get { return CurrentDirectory + "\\JSON\\" + FileNames[0]; } }
        private string BasketJsonPath { get { return CurrentDirectory + "\\JSON\\" + FileNames[1]; } }

        public CinemaChain GetCinemaChain()
        {
            CinemaChain cinemaChain;
            ItemDeserializ(CinemasJsonPath, out cinemaChain);
            return cinemaChain;
        }
        private void ItemDeserializ<T>(string pathJson,out T element)
        {
            try
            {
                string textJson = File.ReadAllText(pathJson);
                element = JsonSerializer.Deserialize<T>(textJson)!;
            }
            catch (FileNotFoundException)
            {
                throw new BootDataException("Не найден загрузочный файл", pathJson);
            }
            catch (JsonException)
            {
                throw new BootDataException("Ошибка в при получении данных из загрузочного файла", pathJson);
            }
            catch (ArgumentException ex)
            {
                throw new BootDataException($"Ошибка при соблюдении инвариантов: {ex.Message}", pathJson); ;
            }
            catch(InvalidOperationException ex)
            {
                throw new BootDataException(ex.Message, pathJson);
            }
            catch (Exception ex)
            {
                throw new BootDataException(ex.Message, pathJson);
            }
        }
        private string ChoicePath<T>(T element)
        {
            if (element is CinemaChain)
                return CinemasJsonPath;
            else if (element is Basket)
                return BasketJsonPath;
            else if(element == null)
                throw new Exception("Нельзя сохранить значение равное NULL");
            else
                throw new Exception($"Тип сохраняемого элемента {element.GetType()}  не поддерживается");
        }
        public void Save<T>(T element)
        {
            string path = ChoicePath(element);

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
