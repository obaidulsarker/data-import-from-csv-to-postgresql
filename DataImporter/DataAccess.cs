using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NpgsqlTypes;
using Npgsql;
using System.IO;
using Logging;

namespace DataImporter
{
    public class DataAccess
    {

        private LogFilesWritting log = new LogFilesWritting();

        public static DataAccess GetInstance()
        {
            return new DataAccess();
        }

        public int CsvFileTraverse()
        {
            int status = 0;
            
            // File Location
            string FileLocation = DatabaseConnection.CsvFileLocation();

            //Check File Path
            if (Directory.Exists(FileLocation)==false)
            {
                log.LogInfo("Err: Directory -" + FileLocation + " does not exist.");
                return 0;
            }

            var filePaths = Directory.GetFiles(@FileLocation, "*.csv");

            //BackupDirectoy
            string backupDir = System.DateTime.Now.ToString("yyyyMMdd_HHmm");

            String insert_statment = "";
            CsvDataParse obj = new CsvDataParse();

            try
            {

                foreach (string fileName in filePaths)
                {
                    log.LogInfo("Info - " + fileName + " is started.");
                    //Read Csv File
                    List<CsvDataModel> records = obj.GetObjList(fileName);

                    //Get Insert String
                    insert_statment = obj.GetInsertStm(records);

                    //Save Data
                    status = Save(insert_statment);
                    
                    //Move Csv to backup directory
                    if (status > 0) //success
                    {
                        log.LogInfo("Info - " + fileName + " is saved into database.");
                        string file = obj.MoveCsvToBackupDirectory(fileName, backupDir);
                        log.LogInfo("Info - " + fileName + " is moved to "+ backupDir +" directory.");
                    }
                    else //failed
                    {
                        log.LogInfo("Err - " + fileName + " is not saved into database.");
                    }

                    log.LogInfo("Info - " + fileName + " is completed.");

                }
            }
            catch (Exception ex)
            {
                log.LogInfo("Err : " + ex.Message.ToString());
            }
           
            return status;
        }

        public System.Int32 Save(String stmt)
        {
            int id = 0;

            DataSet dsResult = new DataSet();
            NpgsqlConnection conn = null;

            try
            {
                conn = DatabaseConnection.GetConnection() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call show_cities() procedure
                NpgsqlCommand command = new NpgsqlCommand(stmt, conn);
                command.CommandType = CommandType.Text;

                //command.Parameters.Add(new NpgsqlParameter("sql_stm", NpgsqlDbType.Text));
                //command.Parameters[0].Value = stmt;

                // Execute the procedure and obtain a result set
                int result = (int)command.ExecuteNonQuery();

                tran.Commit();

                id = result;

            }
            catch (Npgsql.NpgsqlException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                string str = "Exception : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException : " + ex.Message;
                }

                throw new Exception(str, ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            return id;
        }
    }
}
