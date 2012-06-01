﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace Sumit.Webpart.Weather.Weather
{
    [ToolboxItemAttribute(false)]
    public class Weather : Microsoft.SharePoint.WebPartPages.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/Sumit.Webpart.Weather/Weather/WeatherUserControl.ascx";


        public enum TempUnit
        {
            Celsius = 0,
            Fahrenheit
        };


        #region WebPart Properties

        public static string _cityName = "Agra , U.P , India";
        [WebBrowsable(true),
        WebDisplayName("City , State , Country"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Enter the City name"),
        Category("Weather WebPart Settings")]
        public string CityName
        {
            get
            {
                return _cityName;
            }
            set
            {
                _cityName = value;
            }
        }

        

        public static bool _condition = true;
        [WebBrowsable(true),
        WebDisplayName("Display Condition"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display Condition"),
        Category("Weather WebPart Settings")]
        public bool Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
        }


        public static bool _conditionImage = true;
        [WebBrowsable(true),
        WebDisplayName("Display Condition Image"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display Condition Image"),
        Category("Weather WebPart Settings")]
        public bool ConditionImage
        {
            get
            {
                return _conditionImage;
            }
            set
            {
                _conditionImage = value;
            }
        }

        public static bool _high = true;
        [WebBrowsable(true),
        WebDisplayName("Display High Temperature"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display High Temperature"),
        Category("Weather WebPart Settings")]
        public bool High
        {
            get
            {
                return _high;
            }
            set
            {
                _high = value;
            }
        }

        public static bool _low = true;
        [WebBrowsable(true),
        WebDisplayName("Display Low Temperature"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display Low Temperature"),
        Category("Weather WebPart Settings")]
        public bool Low
        {
            get
            {
                return _low;
            }
            set
            {
                _low = value;
            }
        }

        public static TempUnit _unitTemperature = TempUnit.Celsius;
        [WebBrowsable(true),
        WebDisplayName("Unit For Temperature"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Select the temperature unit"),
        Category("Weather WebPart Settings")]
        public TempUnit UnitTemperature
        {
            get
            {
                return _unitTemperature;
            }
            set
            {
                _unitTemperature = value;
            }
        }

        public static bool _humidity = false;
        [WebBrowsable(true),
        WebDisplayName("Display Humidity"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display Humidity"),
        Category("Weather WebPart Settings")]
        public bool Humidity
        {
            get
            {
                return _humidity;
            }
            set
            {
                _humidity = value;
            }
        }

        public static bool _wind = false;
        [WebBrowsable(true),
        WebDisplayName("Display Wind"),
        Personalizable(PersonalizationScope.Shared),
        WebPartStorage(Storage.Shared),
        WebDescription("Check if want to display Wind"),
        Category("Weather WebPart Settings")]
        public bool Wind
        {
            get
            {
                return _wind;
            }
            set
            {
                _wind = value;
            }
        }

        //public static string _forecast = "0";
        //[WebBrowsable(true),
        //WebDisplayName("Display Forecast (Enter number of days between 0 and 3)"),
        //Personalizable(PersonalizationScope.Shared),
        //WebPartStorage(Storage.Shared),
        //WebDescription("Select the number of forecast days"),
        //Category("Weather WebPart Settings")]
        //public string Forecast
        //{
        //    get
        //    {
        //        return _forecast;
        //    }
        //    set
        //    {
        //        if (Convert.ToInt32(value) > 3)
        //        {
        //            throw new WebPartPageUserException("Limit for forecast days is 0-3");
        //        }
        //        _forecast = value;
        //    }
        //}

        public static string ErroMessage { get; set; }

        #endregion


        protected override void CreateChildControls()
        {
            try
            {
                Control control = Page.LoadControl(_ascxPath);
                Controls.Add(control);
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}