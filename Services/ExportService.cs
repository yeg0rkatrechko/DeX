using CsvHelper;
using Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using DbModels;

namespace Services;

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

    public async Task ExportClientToCsv(List<Client> clients)
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
                    await Task.Run(() =>
                    {
                        writer.WriteField("Id");
                        writer.WriteField("Name");
                        writer.WriteField("PassportID");
                        writer.WriteField("DateOfBirth");

                        writer.NextRecord();

                        foreach (Client client in clients)
                        {
                            writer.WriteField(client.ID);
                            writer.WriteField(client.Name);
                            writer.WriteField(client.PassportID);
                            writer.WriteField(client.DateOfBirth);
                            writer.NextRecord();
                        }

                        writer.Flush();
                    });
                }
            }
        }
    }

    public async Task<List<Client>> ReadClientFromCsv(string path, string fileName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(_path);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
        string fullPath = Path.Combine(_path, _csvName);

        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
            {
                using (var reader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                {
                    return await Task.Run(() => reader.GetRecords<Client>().ToList());
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