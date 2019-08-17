using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherAppPrudential.BAL;
using WeatherAppPrudential.DAL;

namespace WeatherAppPrudential
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            String savePath = @"c:\";
            if (CityFile.HasFile)
            {
                savePath += CityFile.FileName;
                CityFile.SaveAs(savePath);
                BussionessLogic bs = new BussionessLogic();
                OpenWeatherMap data = bs.GetFileContents(CityFile.FileName);
                Dictionary<string,OpenWeatherMap> result = new Dictionary<string, OpenWeatherMap>();
                foreach (var item in data.cities)
                {
                    result.Add(item.Value, bs.WeatherApICall(data, item.Key));
                }
                foreach (var item in result)
                {
                    WeatherData.InnerHtml += item.Value.apiResponse;
                    bs.GenerateFileResult(item);
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('Please upload the file');</script>");
            }
        }
    }
}