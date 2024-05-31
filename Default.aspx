<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardMemoryGame.Default" %>

<%-- Содержимое панели навигации --%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

 <%-- Раскрывающийся список для гостя --%>
    <asp:Menu ID="Guest" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" 
        StaticEnableDefaultPopOutImage="false">
        <items>
            <asp:MenuItem Text="Гость" Value="Guest">
                <asp:MenuItem Text="Войти" Value="Login"></asp:MenuItem>
            </asp:MenuItem>
        </items>
        </asp:Menu>

  <%-- Раскрывающийся список для вошедшего в систему пользователя --%>
    <asp:Menu ID="mnu" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" 
        StaticEnableDefaultPopOutImage="false">
        <items>
            <asp:MenuItem Text="X" Value="X">
                <asp:MenuItem Text="Game Page" Value ="Game Page"></asp:MenuItem>
                <asp:MenuItem Text="My Profile" Value ="My Profile"></asp:MenuItem>
                <asp:MenuItem Text="View scores" Value ="View scores"></asp:MenuItem>
                <asp:MenuItem Text="logout" Value="logout"></asp:MenuItem>
            </asp:MenuItem>
        </items>
    </asp:Menu>

</asp:Content>

<%-- Содержимое для выбора количества карт для игры --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="box">
            <h2 class = "indexh1">Выберите Количество Карт</h2>

            <asp:RadioButtonList CssClass="radiobutton" ID="RbSeclector"  runat="server">
                <asp:ListItem Text="12" value="3" Selected ="True"></asp:ListItem>
                <asp:ListItem Text="16" value="4"></asp:ListItem>
                <asp:ListItem Text="20" value="5"></asp:ListItem>
            </asp:RadioButtonList>

            <asp:Button ID="Btn_Start" runat="server" Text="Начать" OnClick="Btn_Start_Click" />
        </div>

</asp:Content>  