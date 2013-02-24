<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeatherUserControl.ascx.cs"
    Inherits="Sumit.Webpart.Weather.Weather.WeatherUserControl" %>
<div class="outdv" id="outdv">
    <table width="100%">
        <tr>
            <td class="header">
                <div class="loc">
                    <asp:Label ID="LocationName" runat="server"></asp:Label>
                </div>
                <div class="temp">
                    <asp:Label ID="lblTempValue" runat="server"></asp:Label>
                    <asp:Label ID="lblTempUnit" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <table width="100%" id="Today" class="seperator">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="Day" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow">
                                <asp:Label ID="Date" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="pad">
                    <asp:Image runat="server" Visible="false" ID="imgCond" CssClass="shdw" />
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="ConditionText" runat="server" Visible="false" Text="Condition"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow">
                                <asp:Label ID="ConditionValue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="TempHighText" runat="server" Visible="false" Text="High"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow" style="color: #880000 !important;">
                                <asp:Label ID="TempHighValue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="TempLowText" runat="server" Visible="false" Text="Low"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow" style="color: #0034C3 !important;">
                                <asp:Label ID="TempLowValue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="HumidityText" runat="server" Visible="false" Text="Humidity"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow">
                                <asp:Label ID="HumidityValue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="topRow">
                                <asp:Label ID="WindText" runat="server" Visible="false" Text="Wind"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="bottomRow">
                                <asp:Label ID="WindValue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="lstupd">
                    <asp:Label ID="lblLastUpdated" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="forecast">
        </div>
        <asp:Label ID="ErrorMessage" runat="server" Visible="false"></asp:Label>
    </table>
</div>
<script type="text/javascript">

    function ShowForecastWeather(forecastHtml) {

        document.getElementById('forecast').innerHTML = forecastHtml;

    }
</script>
<style type="text/css">
    .header
    {
        background-color: #888888;
        width: 100%;
        font-size: large !important;
        color: White;
        font-weight: bold;
    }
    .loc
    {
        float: left;
        margin: 5px 10px;
    }
    .temp
    {
        float: right;
        margin: 5px 10px;
    }
    .topRow
    {
        text-align: center;
        color: #888888;
        font-weight: bold;
        font-size: small;
    }
    .bottomRow
    {
        text-align: center;
        font-size: small;
        color: Black;
    }
    .outdv
    {
        padding: 2px;
        width: 400px;
        background: none repeat scroll 0 0 #FFFFFF !important;
        border: 1px solid #999999 !important;
        border-collapse: collapse !important;
        box-shadow: 5px 5px 5px #AAAAAA !important;
    }
    .shdw
    {
        background: none repeat scroll 0 0 #FFFFFF !important;
        border-collapse: collapse !important;
        box-shadow: 5px 5px 5px #AAAAAA !important;
    }
    .seperator
    {
        margin-top: 5px;
        border-bottom: 1px solid #DBD9D9;
        padding-bottom: 5px;
    }
    .pad
    {
        padding-bottom: 5px;
        width: 40px;
    }
    .colr
    {
        color: Red;
    }
    .lstupd
    {
        padding:5px 5px 0px 5px;
        font-size:xx-small !Improtant;
    }
</style>
