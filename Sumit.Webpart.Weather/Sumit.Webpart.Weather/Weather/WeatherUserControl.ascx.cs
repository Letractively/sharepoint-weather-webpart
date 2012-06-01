using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Animaonline.Weather.WeatherData;
using Animaonline.Weather;
using Animaonline.Globals;
using System.Web.Script.Serialization;

namespace Sumit.Webpart.Weather.Weather
{
    public partial class WeatherUserControl : UserControl
    {
        #region Declarations


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Weather._cityName))
            {
                GoogleWeatherData weather = new GoogleWeatherData();

                try
                {
                    weather = GoogleWeatherAPI.GetWeather(LanguageCode.en_US, Weather._cityName);
                }
                catch
                {
                    ErrorMessage.Text = "Cannot retrieve the City name, please check if the spelling is correct or try to add the state and country name";
                    ErrorMessage.Visible = true;
                    ErrorMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (weather != null)
                {
                    //display location
                    LocationName.Text = weather.ForecastInformation.City;

                    //display Temperature
                    if (Weather._unitTemperature.ToString().Equals("Celsius"))
                    {
                        TempValue.Text = Convert.ToString((weather.CurrentConditions.Temperature).Celsius);
                        TempUnit.Text = "&deg;C";
                    }
                    else
                    {
                        TempValue.Text = Convert.ToString((weather.CurrentConditions.Temperature).Fahrenheit);
                        TempUnit.Text = "&deg;F";
                    }

                    //Display Day and Date
                    Day.Text = "Today";
                    Date.Text = DateTime.Today.ToShortDateString();

                    if (Weather._condition)
                    {
                        ConditionText.Visible = true;
                        ConditionValue.Visible = true;
                        ConditionValue.Text = weather.CurrentConditions.Condition;
                    }
                    if (Weather._conditionImage)
                    {
                        imgCond.Visible = true;
                        imgCond.ImageUrl = "http://www.google.com/" + weather.CurrentConditions.Icon;
                        imgCond.ImageAlign = ImageAlign.Middle;
                    }
                    if (Weather._high)
                    {
                        TempHighText.Visible = true;
                        TempHighValue.Visible = true;

                        if (Weather._unitTemperature.ToString().Equals("Celsius"))
                        {
                            TempHighValue.Text = Convert.ToString(weather.ForecastConditions[0].High.Celsius).Split('.')[0] + "&deg;C";
                        }
                        else
                        {
                            TempHighValue.Text = Convert.ToString(weather.ForecastConditions[0].High.Fahrenheit).Split('.')[0] + "&deg;F";
                        }

                    }
                    if (Weather._low)
                    {
                        TempLowText.Visible = true;
                        TempLowValue.Visible = true;

                        if (Weather._unitTemperature.ToString().Equals("Celsius"))
                        {
                            TempLowValue.Text = Convert.ToString(weather.ForecastConditions[0].Low.Celsius).Split('.')[0] + "&deg;C";
                        }
                        else
                        {
                            TempLowValue.Text = Convert.ToString(weather.ForecastConditions[0].Low.Fahrenheit).Split('.')[0] + "&deg;F";
                        }
                    }
                    if (Weather._humidity)
                    {
                        HumidityText.Visible = true;
                        HumidityValue.Visible = true;

                        HumidityValue.Text = weather.CurrentConditions.Humidity.ToLower().Remove(0, 10);
                    }
                    if (Weather._wind)
                    {
                        WindText.Visible = true;
                        WindValue.Visible = true;

                        WindValue.Text = weather.CurrentConditions.WindCondition.Remove(0, 6);
                    }

                    #region Forecast to be implementd

                    //int forecastDays = Convert.ToInt32(Weather._forecast);

                    //if (forecastDays > 0)
                    //{
                    //    string forecastHtml = string.Empty;
                    //    for (int i = 1; i <= forecastDays; i++)
                    //    {
                    //        forecastHtml = forecastHtml + CreateForecastHtml(weather.ForecastConditions[i], i);
                    //    }

                    //    //Serialize string to pass to the javascript
                    //    string serialized = (new JavaScriptSerializer()).Serialize(forecastHtml);
                    //    serialized = serialized.Remove(0, 1);
                    //    serialized = serialized.Remove(serialized.LastIndexOf('"'), 1);

                    //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowForecastWeather", "ShowForecastWeather('" + serialized + "');", true);

                    //}
                    #endregion
                }
            }
        }

        //private string CreateForecastHtml(ForecastCondition forecastCondition, int addDay)
        //{
        //    string lowTempC = forecastCondition.Low.Celsius.ToString().Split('.')[0] + "&deg;C";
        //    string lowTempF = forecastCondition.Low.Fahrenheit.ToString().Split('.')[0] + "&deg;F";
        //    string highTempC = forecastCondition.High.Celsius.ToString().Split('.')[0] + "&deg;C";
        //    string highTempF = forecastCondition.High.Fahrenheit.ToString().Split('.')[0] + "&deg;F";

        //    StringBuilder html = new StringBuilder();
        //    html.Append("<table width=\"100%\" id=\"Forecast" + forecastCondition.Day + "\" class=\"seperator\">");
        //    html.Append("<tr><td>");
        //    html.Append("<table width=\"100%\">");
        //    html.Append("<tr><td class=\"topRow\">");
        //    //Displays Day
        //    html.Append(forecastCondition.Day);
        //    html.Append("</td></tr>");
        //    html.Append("<tr><td class=\"bottomRow\">");
        //    //displays Date
        //    html.Append(DateTime.Today.AddDays(addDay).ToShortDateString());
        //    html.Append("</td></tr></table></td>");
        //    html.Append("<td>");

        //    //Displays Condition Image
        //    if (Weather._conditionImage)
        //        html.Append("<img src=\"http://www.google.com/" + forecastCondition.Icon + "\" class=\"shdw\" />");
        //    else
        //        html.Append("<asp:Image runat=\"server\" Visible=\"false\" id=\"imgCond" + forecastCondition.Day + "\" CssClass=\"shdw\"/>");

        //    html.Append("</td>");
        //    html.Append("<td><table width=\"100%\"> <tr><td class=\"topRow\">");

        //    //Displays Condition
        //    if (Weather._condition)
        //        html.Append("Condition");
        //    else
        //        html.Append("<asp:Label ID=\"ConditionText" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\" Text=\"Condition\"></asp:Label>");

        //    html.Append("</td></tr>");
        //    html.Append("<tr><td class=\"bottomRow\">");

        //    //Displays Condition Value
        //    if (Weather._condition)
        //        html.Append(forecastCondition.Condition);
        //    else
        //        html.Append("<asp:Label ID=\"ConditionValue" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\"></asp:Label>");

        //    html.Append("</td></tr></table></td>");
        //    html.Append("<td><table width=\"100%\"><tr><td class=\"topRow\">");

        //    //Displays High Temperature
        //    if (Weather._high)
        //        html.Append("High");
        //    else
        //        html.Append("<asp:Label ID=\"TempHighText" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\" Text=\"High\"></asp:Label>");

        //    html.Append("</td></tr>");
        //    html.Append("<tr>");
        //    html.Append("<td class=\"bottomRow\" style=\"color: #880000 !important;\">");

        //    //Displays High Temperature Value
        //    if (Weather._high)
        //    {
        //        if (Weather._unitTemperature.ToString().Equals("Celsius"))
        //            html.Append(highTempC);
        //        else
        //            html.Append(highTempF);
        //    }
        //    else
        //        html.Append("<asp:Label ID=\"TempHighValue" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\"></asp:Label>");

        //    html.Append("</td></tr></table></td>");
        //    html.Append("<td><table width=\"100%\"><tr><td class=\"topRow\">");

        //    //Displays Low Temperature
        //    if (Weather._low)
        //        html.Append("Low");
        //    else
        //        html.Append("<asp:Label ID=\"TempLowText" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\" Text=\"Low\"></asp:Label>");

        //    html.Append("</td></tr>");
        //    html.Append("<tr>");
        //    html.Append("<td class=\"bottomRow\" style=\"color: #0034C3 !important;\">");

        //    //Displays Low Temperature Value
        //    if (Weather._low)
        //    {
        //        if (Weather._unitTemperature.ToString().Equals("Celsius"))
        //            html.Append(lowTempC);
        //        else
        //            html.Append(lowTempF);
        //    }
        //    else
        //        html.Append("<asp:Label ID=\"TempLowValue" + forecastCondition.Day + "\" runat=\"server\" Visible=\"false\"></asp:Label>");

        //    html.Append("</td></tr></table></td>");
        //    html.Append("</tr></table>");

        //    return html.ToString();
        //}
    }
}
