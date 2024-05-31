<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Home.Master" CodeBehind="Play.aspx.cs" Inherits="CardMemoryGame.Play" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class = "indexh1">Card Memory Game</h1>
        <div class="box">
            <h2 class = "indexh1">Choose number of cards</h2>

            <asp:RadioButtonList CssClass="radiobutton" ID="RbSeclector"  runat="server">
                <asp:ListItem Text="12" value="3" Selected ="True"></asp:ListItem>
                <asp:ListItem Text="16" value="4"></asp:ListItem>
                <asp:ListItem Text="20" value="5"></asp:ListItem>
            </asp:RadioButtonList>

            <asp:Button ID="Btn_Start" runat="server" Text="Start" OnClick="Btn_Start_Click" />
        </div>
</asp:Content>
