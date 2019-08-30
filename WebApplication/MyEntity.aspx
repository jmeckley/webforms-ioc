<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyEntity.aspx.cs" Inherits="WebApplication.MyEntity" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" />
    <asp:Label Id="EntityIdLabel" runat="server"><%# EntityId?.ToString() ?? "New"  %></asp:Label>
    <asp:TextBox runat="server" ID="Name" placeholder="Full Name" MaxLength="50" Text="<%# Model.Name %>"></asp:TextBox>
    <asp:TextBox runat="server" ID="Age" placeholder="Age" type="number" min="0" Text="<%# Model.Age %>"></asp:TextBox>
    <asp:Button runat="server" ID="Button1" Text="Save" OnClick="Save"/>
</asp:Content>
