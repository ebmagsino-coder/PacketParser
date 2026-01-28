namespace NTTPacketParser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Test mode for console output
            if (args.Length > 0 && args[0] == "--test")
            {
                TestParser.TestSampleData();
                Console.ReadKey();
                return;
            }

            // Normal GUI mode
            ApplicationConfiguration.Initialize();
            Application.Run(new InputForm());
        }
    }
}