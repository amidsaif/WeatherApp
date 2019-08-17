using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WeatherAppPrudential.DAL;

namespace WeatherAppPrudential.BAL
{
    public class BussionessLogic
    {


        public OpenWeatherMap WeatherApICall(OpenWeatherMap openWeatherMap, string cities)
        {

            if (cities != null)
            {
                try
                {
                    /*Calling API http://openweathermap.org/api */
                    string apiKey = "aa69195559bd4f88d79f9aadeb77a8f6";
                    HttpWebRequest apiRequest =
                    WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" +
                    cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                    string apiResponse = "";
                    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        apiResponse = reader.ReadToEnd();
                    }
                    /*End*/

                    /*http://json2csharp.com*/
                    ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class=\"col-md-4\" ><tr><th>Weather Description</th></tr>");
                    sb.Append("<tr><td>City:</td><td>" +
                    rootObject.name + "</td></tr>");
                    sb.Append("<tr><td>Country:</td><td>" +
                    rootObject.sys.country + "</td></tr>");
                    sb.Append("<tr><td>Wind:</td><td>" +
                    rootObject.wind.speed + " Km/h</td></tr>");
                    sb.Append("<tr><td>Current Temperature:</td><td>" +
                    rootObject.main.temp + " °C</td></tr>");
                    sb.Append("<tr><td>Humidity:</td><td>" +
                    rootObject.main.humidity + "</td></tr>");
                    sb.Append("<tr><td>Weather:</td><td>" +
                    rootObject.weather[0].description + "</td></tr>");
                    sb.Append("</table>");
                    openWeatherMap.apiResponse = sb.ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    
                }
                
            }
            return openWeatherMap;



        }
        /// <summary>
        /// This function will generate the files and will store them at the specified location.
        /// Folder with current date time will be created and .HTML files will be saved inside the folder
        /// </summary>
        /// <param name="apiResponse"></param>
        /// Inside parameter City Name is passed as key to that it can be used as file name
        public void GenerateFileResult(KeyValuePair<string, OpenWeatherMap> apiResponse)
        {
            string curDateTime = DateTime.Now.ToString();
            curDateTime = curDateTime.Replace(':', '-');
            curDateTime = curDateTime.Replace('/', '-');
            // curDateTime.Replace(' ','');
            try
            {
                string path = @"C:\WeatherData\" + curDateTime + "\\" + apiResponse.Key + ".html";
            //string path = @"C:\WeatherData\new.txt";  

           
                string root = @"C:\WeatherData\" + curDateTime;
                // If directory does not exist, create it. 
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(Convert.ToString(apiResponse.Value.apiResponse));
                    // Add some weather information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        /// <summary>
        /// This function is used to read the contents of the file.
        /// The Key for City and Name of the City will be taken from uploaded file
        /// The values will be stored as a dictionary Key Value Pair.
        /// The inputs of the file will be considered only when ID and Name of City are seperated by '=' sign.
        /// It will reject the input if the condition is not matched and will go for the next line
        /// There will not be any exception for invalid Input. 
        /// All the data of cities will get generated who have correct IDs seperated by = in file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>openWeatherMap</returns>
        public OpenWeatherMap GetFileContents(string fileName)
        {
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.cities = new Dictionary<string, string>();
            string line;
            try
            {
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"c:\" + fileName);
                while ((line = file.ReadLine()) != null)
                {
                    string[] res = line.Split('=');
                    if(res.Length==2)
                        openWeatherMap.cities.Add(res[0], res[1]);
                }
                file.Close();
                File.Delete(@"c:\" + fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return openWeatherMap;

        }
    }

}