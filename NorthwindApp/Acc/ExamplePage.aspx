<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamplePage.aspx.cs" Inherits="NorthwindApp.Acc.ExamplePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <form>
            <div class="form">
                <div class="form-group">
                    <label for="txtUsername">Username</label>
                    <asp:TextBox runat="server" placeholder="Username" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox runat="server" placeholder="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </form>
    </div>

    <br />

    <div class="row">

        <form class="form-horizontal">
            <div class="form-group">
                <label for="txtExample1" class="col-sm-2">Example 1</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" placeholde="Example"></asp:TextBox>
                </div>
            </div>
        </form>

    </div>

    <br />
    <br />
    <br />
    <div class="col-sm-6">
        <label class="label label-danger">Test Label - Danger</label>
    </div>

    <label class="label label-default">Test Label - Default</label>
    <label class="label label-primary">Test Label - Primary</label>
    <label class="label label-info">Test Label - Info</label>


</asp:Content>
