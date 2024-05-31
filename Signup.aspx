<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CardMemoryGame.Signup" %>

<%-- Содержимое панели навигации --%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

    <li class="indexh2"><a href="/Login.aspx">Войти</a></li>

</asp:Content>

<%-- Содержимое для получения данных пользователя для регистрации --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="box">
        <h2 class="indexh2" style="text-transform: uppercase">Signup</h2>

        <%-- Имя пользователя --%>
        <asp:TextBox runat="server" placeholder="имя пользователя" ID="UserName"></asp:TextBox>
        <asp:Label ID="UsernameValidate" runat="server" ForeColor="White"></asp:Label>

       <%-- Адрес электронной почты --%>
        <asp:TextBox runat="server" placeholder="E-mail" ID="Email"></asp:TextBox>
        <asp:Label ID="EmailValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="Email" runat="server" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ErrorMessage="Enter valid email address<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>

     <%-- Кнопка "Сгенерировать" --%>
        <div class="Btn_Login" style="margin-left:0;">
                <asp:Button runat="server" Text="Получить Код" OnClick="GenerateOTP"/>
        </div>

        <%-- Пароль --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="пароль" ID="Password"></asp:TextBox>
        <asp:Label ID="PasswordValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:RegularExpressionValidator ID="PasswordValidator" ControlToValidate="Password" runat="server" 
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$" 
            ErrorMessage="Пароль должен содержать минимум восемь и<br>максимум 15 символов, минимум одна заглавная буква,<br>
            одна строчная буква, одна цифра и один специальный символ<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>

     <%-- Повторите пароль --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="повторить пароль" ID="RePassword"></asp:TextBox>
        <asp:Label ID="RePasswordValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:CompareValidator ID="ComparePassword" runat="server" ControlToValidate="RePassword" 
            ControlToCompare="Password" ErrorMessage="Password and Retype password must be same<br>" Display="Dynamic" ForeColor="White"></asp:CompareValidator>

        <%-- Код подтверждения --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="код подтверждения" ID="OTPbox"></asp:TextBox>
        <asp:Label ID="OtpInvalidLabel" runat="server" ForeColor="White"></asp:Label>

       <%-- Кнопка регистрации --%>
        <div class="Btn_Login" style="margin-left:0;">
            <asp:Button runat="server" Text="Регистрация" onclick="SignupButton"/>
        </div>
        <asp:Label ID="SignUpValidate" runat="server" ForeColor="White"></asp:Label>

    </div>
   
</asp:Content>
