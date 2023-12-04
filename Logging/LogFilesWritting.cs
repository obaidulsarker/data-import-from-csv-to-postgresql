using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Logging
{
    public class LogFilesWritting
    {
        private readonly string logFileLocation;
        List<String> logString = new List<String>();

        public LogFilesWritting()
        {
            logFileLocation = CreateFile();
        }

        #region Log file maintenance for email notification------------------

        private void LogInfo()
        {
            LogInfo(string.Empty);
        }

        public void LogInfo(string lines)
        {
            string message = string.Format("{0:yyyy-MMM-dd hh:mm:ss tt} - ", DateTime.Now) + lines;

            StreamWriter file = File.AppendText(logFileLocation);
            file.WriteLine(message);

            file.Close();
        }

        private void LogInfo(string format, params object[] args)
        {
            string message = string.Format("{0:yyyy-MM-dd hh:mm:ss tt} - ", DateTime.Now) + (args.Length > 0 ? string.Format(format, args) : format);
            //var ddd = dd.LogFileLocation;
            StreamWriter file = File.AppendText(logFileLocation);

            file.WriteLine(message);

            file.Close();

        }

        public string CreateFile()
        {
           // string FilePath = System.IO.Directory.GetCurrentDirectory();

            string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string FileName = "Migration_"+System.DateTime.Now.ToString("yyyyMMdd_HHmm")+".log";
            string path = FilePath + "\\" + FileName;

            if (!File.Exists(path))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Migration Log...........");
                    sw.WriteLine("************************");
                }
            }

            return path;
        }


        #endregion
    }
}
