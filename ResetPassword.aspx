
<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="CardMemoryGame.ResetPassword" %>
<%-- Содержимое панели навигации --%>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationContent" runat="server">
    <li class="indexh2"><a href="/Signup.aspx">Sign up</a></li>
    <li class="indexh2"><a href="/Login.aspx">Log in</a></li>
</asp:Content>

<%-- Содержимое для получения сведений о пользователе --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box">

      <%-- Получение подробностей от пользователя --%>
        <h2 class="indexh2" style="text-transform: uppercase">Forgot Password</h2>
        <asp:TextBox ID="Username" runat="server" placeholder="user name"></asp:TextBox>
        <asp:TextBox runat="server" placeholder="E-mail" ID="Email"></asp:TextBox>
        <asp:Label ID="InvalidLabel" runat="server" ForeColor="White"></asp:Label>
        
        <%-- Кнопки --%>
        <div class="Btn_Login">
            <asp:Button runat="server" Text="Generate Password" onclick="GenerateNewPassword"/>   
        </div>

    </div>
</asp:Content>
