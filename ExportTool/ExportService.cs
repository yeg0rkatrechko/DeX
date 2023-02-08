using CsvHelper;
using System.Globalization;
using System.Text;
using Models;
using DbModels;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ExportTool
{
    public class ExportService
    {
        private string _path { get; set; }

        private string _csvName { get; set; }
        public BankContext dbContext;

        public ExportService(string path, string csvName)
        {
            _path = path;
            _csvName = csvName;
            dbContext = new BankContext();
        }

        public void ExportClientToCsv(List<ClientDB> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            string fullPath = Path.Combine(_path, _csvName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    using (var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public List<ClientDB> ReadClientFromCsv(string path, string fileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            string fullPath = Path.Combine(_path, _csvName);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var clients = reader.EnumerateRecords(new ClientDB());
                        dbContext.AddRange(clients);
                        dbContext.SaveChanges();
                        return clients.ToList();
                    }
                }
            }
        }

        public async Task PersonSerializationExport<T>(List<T> person, string path, string fileName) where T : Person
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(path, fileName);
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                string personSerialize = JsonConvert.SerializeObject(person);
                await writer.WriteAsync(personSerialize);
            }
        }
        public async Task<List<T>> PersonDeserializationImport<T>(string path, string fileName) where T : Person
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(path, fileName);
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string personSerialize = await reader.ReadToEndAsync();
                List<T> personDeserialize = JsonConvert.DeserializeObject<List<T>>(personSerialize);
                return personDeserialize;
            }

        }
    }
}