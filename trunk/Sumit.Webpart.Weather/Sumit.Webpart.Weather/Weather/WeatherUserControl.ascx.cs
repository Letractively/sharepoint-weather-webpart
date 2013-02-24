using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Animaonline.Weather.WeatherData;
using Animaonline.Weather;
using Animaonline.Globals;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Sumit.Webpart.Weather.Entity;
using System.Xml;
using System.Xml.Linq;

namespace Sumit.Webpart.Weather.Weather
{
    public partial class WeatherUserControl : UserControl
    {
        #region Declarations
        public string TempUnit
        {
            get
            {
                if (Weather._unitTemperature == Weather.TempUnitType.Celsius)
                    return "c";
                else
                    return "f";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Weather._cityName))
            {
                string WOEID = GetWOEID(Weather._cityName.Replace(" ", "%20"));

                if (!string.IsNullOrEmpty(WOEID))
                {
                    WeatherProfile Profile = GetWeatherDetails(WOEID);

                    LocationName.Text = Profile.Location;
                    lblTempValue.Text = Profile.Temperature + "&deg;" + TempUnit.ToUpper();
                    Day.Text = "Today";
                    Date.Text = DateTime.Today.ToShortDateString();

                    if (Weather._updateInfo)
                    {
                        lblLastUpdated.Visible = true;
                        lblLastUpdated.Text = Profile.PublishedDate;
                    }

                    if (Weather._condition)
                    {
                        ConditionText.Visible = true;
                        ConditionValue.Visible = true;
                        ConditionValue.Text = Profile.Condition;
                    }
                    if (Weather._conditionImage)
                    {
                        imgCond.Visible = true;
                        imgCond.ImageUrl = Profile.ImagePath;
                        imgCond.ImageAlign = ImageAlign.Middle;
                    }
                    if (Weather._high)
                    {
                        TempHighText.Visible = true;
                        TempHighValue.Visible = true;
                        TempHighValue.Text = Profile.HighTemperature;
                    }
                    if (Weather._low)
                    {
                        TempLowText.Visible = true;
                        TempLowValue.Visible = true;
                        TempLowValue.Text = Profile.LowTemperature;
                    }
                    if (Weather._humidity)
                    {
                        HumidityText.Visible = true;
                        HumidityValue.Visible = true;

                        HumidityValue.Text = Profile.Humidity;
                    }
                    if (Weather._wind)
                    {
                        WindText.Visible = true;
                        WindValue.Visible = true;

                        WindValue.Text = Profile.Wind + "km/h"; ;
                    }
                }
                else
                {
                    Weather.ErrorMessage = "Cannot retrieve the City name, please check if the spelling is correct or try to add the state and country name";
                    DisplayError();
                }
            }
        }

        /// <summary>
        /// Gets the Weather details from Yahoo API in XML Format
        /// </summary>
        /// <param name="WOEID"></param>
        /// <returns></returns>
        private WeatherProfile GetWeatherDetails(string WOEID)
        {
            XDocument Response = null;
            WeatherProfile objProfile = new WeatherProfile();
            string URL = string.Format("http://weather.yahooapis.com/forecastrss?w=" + WOEID + "&u=" + TempUnit);
            bool forecast = false;

            try
            {
                Response = XDocument.Load(URL);

                if (Response != null && Response.Root != null)
                {
                    XmlDocument weatherDetails = new XmlDocument();
                    weatherDetails.LoadXml(Response.ToString());

                    foreach (XmlNode objNode in weatherDetails.ChildNodes[0].ChildNodes[0])
                    {
                        if (objNode.Name.ToLower().Equals("yweather:location"))
                        {
                            foreach (XmlAttribute objAttribute in objNode.Attributes)
                            {
                                if (objAttribute.Name.ToLower().Equals("city")) { objProfile.Location = objAttribute.Value; }
                                else if (objAttribute.Name.ToLower().Equals("region")) { objProfile.Location = objProfile.Location + ", " + objAttribute.Value; }
                                else if (objAttribute.Name.ToLower().Equals("country")) { objProfile.Location = objProfile.Location + ", " + objAttribute.Value; }
                            }
                        }

                        else if (objNode.Name.ToLower().Equals("yweather:atmosphere"))
                        {
                            foreach (XmlAttribute objAttribute in objNode.Attributes)
                            {
                                if (objAttribute.Name.ToLower().Equals("humidity")) { objProfile.Humidity = objAttribute.Value; break; }
                            }
                        }

                        else if (objNode.Name.ToLower().Equals("yweather:wind"))
                        {
                            foreach (XmlAttribute objAttribute in objNode.Attributes)
                            {
                                if (objAttribute.Name.ToLower().Equals("speed")) { objProfile.Wind = objAttribute.Value; break; }
                            }
                        }

                        if (objNode.Name.ToLower().Equals("item"))
                        {
                            foreach (XmlNode objItemNode in objNode.ChildNodes)
                            {
                                if (objItemNode.Name.ToLower().Equals("yweather:condition"))
                                {
                                    foreach (XmlAttribute objAttribute in objItemNode.Attributes)
                                    {
                                        if (objAttribute.Name.ToLower().Equals("text")) { objProfile.Condition = objAttribute.Value; }
                                        else if (objAttribute.Name.ToLower().Equals("temp")) { objProfile.Temperature = objAttribute.Value; }
                                        else if (objAttribute.Name.ToLower().Equals("code")) { objProfile.ImagePath = "http://l.yimg.com/a/i/us/we/52/" + objAttribute.Value + ".gif"; }
                                    }
                                }
                                else if (objItemNode.Name.ToLower().Equals("pubdate"))
                                {
                                    objProfile.PublishedDate = "Last Updated: " + objItemNode.InnerText;
                                }
                                else if (objItemNode.Name.ToLower().Equals("yweather:forecast"))
                                {
                                    if (!forecast)
                                    {
                                        foreach (XmlAttribute objAttribute in objItemNode.Attributes)
                                        {
                                            if (objAttribute.Name.ToLower().Equals("low")) { objProfile.LowTemperature = objAttribute.Value; }
                                            else if (objAttribute.Name.ToLower().Equals("high")) { objProfile.HighTemperature = objAttribute.Value; }
                                        }
                                        forecast = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return objProfile;
        }


        /// <summary>
        /// Gets the WOEID using Yahoo Query Langague (YQL)
        /// </summary>
        /// <param name="CityName"></param>
        /// <returns></returns>
        private string GetWOEID(string CityName)
        {
            string WOEID = string.Empty;
            string URL = String.Format("http://query.yahooapis.com/v1/public/yql?q=select%20woeid%20from%20geo.places%20where%20text%3D%22" + CityName + "%22&diagnostics=true");

            WebRequest request = WebRequest.Create(URL) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string retVal = reader.ReadToEnd();
                if (retVal.Contains("<woeid>") && retVal.Contains("</woeid>"))
                    WOEID = retVal.Substring(retVal.IndexOf("<woeid>") + 7, retVal.IndexOf("</woeid>") - (retVal.IndexOf("<woeid>") + 7));
            }

            return WOEID;
        }

        /// <summary>
        /// Displays the Error Message
        /// </summary>
        private void DisplayError()
        {
            ErrorMessage.Text = Weather.ErrorMessage;
            ErrorMessage.Visible = true;
            ErrorMessage.ForeColor = System.Drawing.Color.Red;
            Weather.ErrorMessage = string.Empty;
        }
    }
}
