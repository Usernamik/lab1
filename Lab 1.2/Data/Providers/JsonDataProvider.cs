using System.Text;
using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Providers
{
    public class JsonDataProvider : IDataProvider
    {
        public CustomerFeedbackData ReadData(string filePath)
        {
            return ReadData(filePath, null);
        }

        public CustomerFeedbackData ReadData(string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            var jsonString = File.ReadAllText(filePath, encoding);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<CustomerFeedbackData>(jsonString, options);
            if (data == null)
                return new CustomerFeedbackData();
            return data;
        }

        public void WriteData(CustomerFeedbackData data, string filePath)
        {
            WriteData(data, filePath, null);
        }

        public void WriteData(CustomerFeedbackData data, string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString, encoding);
        }
    }
}
