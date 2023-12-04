using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Npgsql;

namespace DataImporter
{
    public static class DatabaseConnection
    {
        public static IDbConnection GetConnection()
        {
            IDbConnection objIdbConnection = null;

            var xmlpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Config.xml";
            var doc = new System.Xml.XPath.XPathDocument(xmlpath);
            var navigator = doc.CreateNavigator();

            var serverName = navigator.SelectSingleNode("//appsettings/servername");
            var port = navigator.SelectSingleNode("//appsettings/port");
            var username = navigator.SelectSingleNode("//appsettings/username");
            var password = navigator.SelectSingleNode("//appsettings/password");
            var database = navigator.SelectSingleNode("//appsettings/database");

            objIdbConnection = new NpgsqlConnection("Server=" + serverName.Value + ";Port=" + port.Value + ";Database=" + database.Value + ";User Id=" + username.Value + ";Password=" + password.Value + ";CommandTimeout=300");

            return objIdbConnection;

            // IDbConnection test = new NpgsqlConnection("Server=localhost;Port=5432;Database=Enovative;User Id=postgres;Password=sa1234;CommandTimeout=300");
        }

        public static string CsvFileLocation()
        {
            var xmlpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Config.xml";
            var doc = new System.Xml.XPath.XPathDocument(xmlpath);
            var navigator = doc.CreateNavigator();

            var csvpath = navigator.SelectSingleNode("//appsettings/csv-file-path").Value;

            return csvpath;
        }

        public static string CsvBackupFileLocation()
        {
            var xmlpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Config.xml";
            var doc = new System.Xml.XPath.XPathDocument(xmlpath);
            var navigator = doc.CreateNavigator();

            var csvpath = navigator.SelectSingleNode("//appsettings/csv-file-backup-path").Value;

            return csvpath;
        }
    }


}
