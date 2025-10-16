using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDataProvider
    {
        CustomerFeedbackData ReadData(string filePath);
        void WriteData(CustomerFeedbackData data, string filePath);
    }
}
