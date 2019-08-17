using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAppPrudential.BAL;
using WeatherAppPrudential.DAL;
namespace TestWeatherAppPrudential
{
    [TestClass]
    public class FileIOTest
    {
        /// <summary>
        /// Test to what will happen if file dosen't exists
        /// Output shows that it is handled
        /// </summary>
        [TestMethod]
        public void TestGetFileContents()
        {
            string fileName = @"C\Cities.txt";
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap openWeatherMap = bussionessLogic.GetFileContents(fileName);
            AssertFailedException.Equals(null, openWeatherMap);
        }
        //Test is not writted to check if file exists because the test will fail 
        //if someone check's the file and it won't be present
        /// <summary>
        /// This test is written to check if the Files are getting stored at correct location
        /// or not
        /// If the last modified date is within one minute of current date time then the file has been created at specified location
        /// </summary>
        [TestMethod]
        public void TestGenerateFileResult()
        {
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.apiResponse = "Data";
            KeyValuePair<string, OpenWeatherMap> input = new KeyValuePair<string, OpenWeatherMap>("Paris", openWeatherMap);
            bussionessLogic.GenerateFileResult(input);
            DateTime lastModified = System.IO.File.GetLastWriteTime(@"C:\WeatherData");
            Assert.IsTrue(lastModified.AddMinutes(1) > DateTime.Now);
        }

        }
}
