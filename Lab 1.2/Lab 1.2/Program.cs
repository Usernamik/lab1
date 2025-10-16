namespace Lab_1._2
{
    internal static class Program
    {
        /// <summary>
        ///  Головна точка входу в програму
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Налаштовуємо конфігурацію програми
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}