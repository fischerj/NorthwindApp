<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="NorthwindApp.Acc.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Please enter your email and your password will be sent to you.</h1>
        <table style="width: 100%;">   
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Your Email"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnForgotPassword" runat="server" Text="Send my password" OnClick="btnForgotPassword_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
