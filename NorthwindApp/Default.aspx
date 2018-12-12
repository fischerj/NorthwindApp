<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NorthwindApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>EDIT SHIPPERS</h1>
    <asp:GridView ID="gvShipper" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>

                <ItemTemplate>
                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Edit" Text="Edit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" />
                    <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnInsert" runat="Server" Text="Insert" CommandName="Insert" UseSubmitBehavior="False" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Id" ControlStyle-Width="10px">

                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>

                <ControlStyle Width="10px"></ControlStyle>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="Company Name" ControlStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:TextBox>
                </EditItemTemplate>

                <ControlStyle Width="100px"></ControlStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Phone" ControlStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:TextBox>
                </EditItemTemplate>

                <ControlStyle Width="100px"></ControlStyle>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>

</asp:Content>
