<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadImages.aspx.cs" Inherits="NorthwindApp.UploadImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    <br />
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <asp:Repeater ID="rptImages" runat="server">
        <ItemTemplate>
            <asp:Image ID="imgImage" runat="server" ImageUrl='<%# Eval("RelativeFilePath") %>' />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
