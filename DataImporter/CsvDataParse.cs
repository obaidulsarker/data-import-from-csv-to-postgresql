using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataImporter
{
    public class CsvDataParse
    {
        private String Insert_string_first = "INSERT INTO public.csv_data( longitude, latitude, record_date, record_time, direct_inclined, diffuse_inclined, reflected, global_inclined, direct_horiz, diffuse_horiz, global_horiz, clear_sky, top_of_atmosphere, code, temperature, relative_humidity, pressure, wind_speed, wind_direction, rainfall, snowfall, snow_depth) VALUES ";

        private LogFilesWritting log = new LogFilesWritting();

        public CsvDataParse GetInstance()
        {
            return new CsvDataParse();
        }
        public List<CsvDataModel> GetObjList(String CsvFilePath)
        {

            Double Longitude = 0.0;
            Double Latitude = 0.0;

            List<CsvDataModel> results = null;

            // Fetch rows 
            results = new List<CsvDataModel>();

            using (var reader = new StreamReader(CsvFilePath))
            {
               
                int RowNo = 0;

                while (!reader.EndOfStream)
                {
                    RowNo++;
                    var line = reader.ReadLine();
                    CsvDataModel obj = new CsvDataModel();

                    // Longitude
                    // Example: # Site latitude (positive means North);24.508
                    if (RowNo==4)
                    {
                        var values = line.Split(';');
                        Longitude =Convert.ToDouble(values[1]);
                    }

                    // Latitude
                    // Example: # Site longitude (positive means East);51.132
                    if (RowNo == 5)
                    {
                        var values = line.Split(';');
                        Latitude = Convert.ToDouble(values[1]);

                    }

                    // Actual Data
                    if (RowNo >= 36)
                    {
                        string[] data = line.Split(";");

                        obj.longitude = Longitude;
                        obj.latitude = Latitude;

                        int i = 0;
                        foreach (string item in data)
                        {
                            i++;

                            //Check date, if not empty
                            if (data[0].ToString().Length > 2)
                            {
                                //record_date
                                if (i == 1)
                                {
                                    obj.record_date = item;
                                }
                                else if (i == 2) //record_time
                                {
                                    obj.record_time = item;
                                }
                                else if (i == 3) // direct_inclined
                                {
                                    obj.direct_inclined = Convert.ToDouble(item);
                                }
                                else if (i == 4) //diffuse_inclined
                                {
                                    obj.diffuse_inclined = Convert.ToDouble(item);
                                }
                                else if (i == 5) //reflected
                                {
                                    obj.reflected = Convert.ToDouble(item);
                                }
                                else if (i == 6) //global_inclined
                                {
                                    obj.global_inclined = Convert.ToDouble(item);
                                }
                                else if (i == 7) //direct_horiz
                                {
                                    obj.direct_horiz = Convert.ToDouble(item);
                                }
                                else if (i == 8) //diffuse_horiz
                                {
                                    obj.diffuse_horiz = Convert.ToDouble(item);
                                }
                                else if (i == 9) //global_horiz
                                {
                                    obj.global_horiz = Convert.ToDouble(item);
                                }
                                else if (i == 10) //clear_sky
                                {
                                    obj.clear_sky = Convert.ToDouble(item);
                                }
                                else if (i == 11) //top_of_atmosphere
                                {
                                    obj.top_of_atmosphere = Convert.ToDouble(item);
                                }
                                else if (i == 12) //code
                                {
                                    obj.Code = Convert.ToInt32(item);
                                }
                                else if (i == 13) //temperature
                                {
                                    obj.temperature = Convert.ToDouble(item);
                                }
                                else if (i == 14) //relative_humidity
                                {
                                    obj.relative_humidity = Convert.ToDouble(item);
                                }
                                else if (i == 15) //pressure
                                {
                                    obj.pressure = Convert.ToDouble(item);
                                }
                                else if (i == 16) //wind_speed
                                {
                                    obj.wind_speed = Convert.ToDouble(item);
                                }
                                else if (i == 17) //wind_direction
                                {
                                    obj.wind_direction = Convert.ToDouble(item);
                                }

                                else if (i == 18) // rainfall
                                {
                                    obj.rainfall = Convert.ToDouble(item);
                                }

                                else if (i == 19) // snowfall
                                {
                                    obj.snowfall = Convert.ToDouble(item);
                                }

                                else if (i == 20) //snow_depth
                                {
                                    obj.snow_depth = Convert.ToDouble(item);
                                }
                            }
                        }

                        results.Add(obj);
                    }
                    //
                    //Console.WriteLine(line);
                    
                }
            }


            return results;

        }

        public String GetInsertStm(List<CsvDataModel> data)
        {
            String insert = "";
            int i = 0;

            foreach (CsvDataModel item in data)
            {
                i++;

                if (i == 1)
                {

                }
                else
                {
                    insert = insert + ","+"\n";
                }

                insert = insert+ "(" + item.longitude + "," + item.latitude + ",'" + item.record_date + "','" + item.record_time + "'," + item.direct_inclined + "," + item.diffuse_inclined + "," + item.reflected + "," + item.global_inclined;

                insert = insert + "," + item.direct_horiz + "," + item.diffuse_horiz + "," + item.global_horiz + "," + item.clear_sky + "," + item.top_of_atmosphere + "," + Convert.ToString(item.Code) + "," + item.temperature + "," + item.relative_humidity;

                insert = insert + "," + item.pressure + "," + item.wind_speed + "," + item.wind_direction + "," + item.rainfall + "," + item.snowfall + "," + item.snow_depth + ")";

            }

            // Check empty list
            if (insert.Length > 10)
            {
                insert = Insert_string_first + "\n" + insert + ";";
            }

            return insert;
        }

       public string MoveCsvToBackupDirectory(string SourceCsvFile, String BackupDirectoryName)
        {
            // File Location
            string BackupPath = DatabaseConnection.CsvBackupFileLocation();
            string DesinationDirectory = @BackupPath + "\\" + BackupDirectoryName;

            //Seperate File Name from full path
            string[]  _sourceFileName = SourceCsvFile.Split("\\");

            string fileName = _sourceFileName[_sourceFileName.Length - 1];

            string DesinationFile = DesinationDirectory + "\\" + fileName;

            //Check Backup path
            if (Directory.Exists(@BackupPath) == false)
            {
                System.IO.Directory.CreateDirectory(@BackupPath);
            }

            //Check Designation Exists or not
            if (Directory.Exists(DesinationDirectory)==false)
            {
                System.IO.Directory.CreateDirectory(DesinationDirectory);
            }

            if (Directory.Exists(DesinationDirectory))
            {
               // To move a file or folder to a new location:
                System.IO.File.Move(@SourceCsvFile, DesinationFile);
            }

              return DesinationFile;
        }
    }
}
