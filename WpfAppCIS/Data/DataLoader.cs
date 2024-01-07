using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfAppCIS.Data
{
    public class DataLoader
    {
        public static string[] LoadData()
        {
            string[] result;

            // Загрузка данных из файла
            try
            {
                using (var stream = new FileStream("Data\\Data.xml", FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(List<string>));
                    var data = (List<string>)serializer.Deserialize(stream);
                    result = data.ToArray();
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("не удалось найти загрузочые файлы");
            }
        }
    }
}
