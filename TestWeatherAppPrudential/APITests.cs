using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAppPrudential.BAL;
using WeatherAppPrudential.DAL;


namespace TestWeatherAppPrudential
{
    [TestClass]
    public class APITests
    {
        [TestMethod]
        public void SuccessApiCall()
        {
            OpenWeatherMap openWeather = new OpenWeatherMap();
            string city = "2643741";
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap result = bussionessLogic.WeatherApICall(openWeather, city);
            Assert.IsTrue(result.apiResponse.StartsWith("<table class=\"col-md-4\""));
            
        }
        [TestMethod]
        public void FailedApiCall()
        {
            OpenWeatherMap openWeather = new OpenWeatherMap();
            string city = "lkasjd";
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap result = bussionessLogic.WeatherApICall(openWeather, city);
            Assert.AreEqual(openWeather, result);
        }
        [TestMethod]
        public void NullInutApiCall()
        {
            OpenWeatherMap openWeather=null;
            string city = null;
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap result = bussionessLogic.WeatherApICall(openWeather, city);
            Assert.AreEqual(openWeather, result);
        }
        [TestMethod]
        public void SpecialCharInputApiCall()
        {
            OpenWeatherMap openWeather = new OpenWeatherMap();
            string city = "ÆÅÉÌÐ";
            BussionessLogic bussionessLogic = new BussionessLogic();
            OpenWeatherMap result = bussionessLogic.WeatherApICall(openWeather, city);
            Assert.AreEqual(openWeather, result);
        }
    }
}
