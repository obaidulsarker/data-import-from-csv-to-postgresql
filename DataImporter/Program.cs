using System;
using Logging;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
           DataAccess obj = new DataAccess();
           LogFilesWritting log = new LogFilesWritting();

            try
            {
                Console.WriteLine("Migration is started");
                log.LogInfo("Migration is started.");
                int status = obj.CsvFileTraverse();
                log.LogInfo("Migration is finished.");
                Console.WriteLine("Migration is finished.");
            }

            catch (Exception ex)
            {
                log.LogInfo("Err:" + ex.Message.ToString());
            }
            
        }
    }
}
