<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CardMemoryGame.Login" %>

<%-- Содержимое панели навигации--%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

    <li class="indexh2"><a href="/Signup.aspx">Регистрация</a></li>

</asp:Content>

<%-- Содержимое для получения данных пользователя для входа в систему --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="box">

      <%-- Получение подробностей от пользователя --%>
        <h2 class="indexh2" style="text-transform: uppercase">login</h2>
        <asp:TextBox ID="Username" runat="server" placeholder="имя пользователя"></asp:TextBox>
        <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="пароль"></asp:TextBox>
        <asp:Label ID="InvalidLabel" runat="server" ForeColor="White"></asp:Label>
        <p style="color: white;">Забыли пароль? <asp:LinkButton ID="LinkButton1" runat="server" OnClick="ResetPassword" ForeColor="White">Восстановить пароль</asp:LinkButton></p>
        
       <%-- Кнопки --%>
        <div class="Btn_Login">
            <asp:Button cssclass="LoginButton" runat="server" Text="Войти" style="margin-right:10px" onclick="LoginButton"/>
            <asp:Button cssclass="GuestLogin" runat="server" Text="Гость" style="margin-left:10px" onclick="PlayAsGuest"/>    
        </div>

    </div>

</asp:Content>

