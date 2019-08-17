<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeatherAppPrudential._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Weather Forcast</h1>
        <p class="lead">This project Generates the Weather information of the mentioned cities. Please upload a file with City Name to get the city weather forcast</p>
        <p><asp:FileUpload class="btn btn-primary btn-lg" ID="CityFile" runat="server" /></p>
        <p><asp:Button class="btn btn-primary btn-lg"  ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Submit" /></p>
    </div>

    <div class="row" runat="server" id="WeatherData">
    </div>
</asp:Content>
