<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div><%: Model.Message %></div>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" />
    <asp:FileUpload runat="server" ID="FileUpload1" />
    <asp:Button runat="server" ID="Button1" Text="Upload File" OnClick="UploadFile"/>
</asp:Content>
