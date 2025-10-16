namespace Data.Logging
{
    // Клас для запису логів у файл
    public class Logger
    {
        private readonly string _logFilePath;

        // Створюємо логер і перевіряємо, чи існує папка для логів
        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
            var directory = Path.GetDirectoryName(_logFilePath);
            // Якщо папки немає, створюємо її
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(string message, string level = "INFO")
        {
            try
            {
                var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch
            {
            }
        }

        public void LogError(string message, Exception ex)
        {
            Log($"{message}: {ex.Message}", "ERROR");
        }

        // Записуємо інформацію про імпорт файлу
        public void LogImport(string filePath, int recordCount)
        {
            Log($"Imported {recordCount} records from {filePath}", "INFO");
        }

        public void LogExport(string filePath, int recordCount)
        {
            Log($"Exported {recordCount} records to {filePath}", "INFO");
        }
    }
}
