<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="CardMemoryGame.MyProfile" %>

<%-- Содержимое панели навигации --%>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationContent" runat="server">
    
   <%-- Раскрывающийся список для вошедшего в систему пользователя --%>
    <asp:Menu ID="mnu" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" 
        StaticEnableDefaultPopOutImage="false">
        <items>
            <asp:MenuItem Text="X" Value="X">
                <asp:MenuItem Text="Главня" Value ="Game Page"></asp:MenuItem>
                <asp:MenuItem Text="Мой профиль" Value ="My Profile"></asp:MenuItem>
                <asp:MenuItem Text="Посмотреть счёт" Value ="View scores"></asp:MenuItem>
                <asp:MenuItem Text="logout" Value="logout"></asp:MenuItem>
            </asp:MenuItem>
        </items>
     </asp:Menu>

</asp:Content>

<%-- Содержимое для отображения и редактирования деталей --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table style="margin-top:5%; margin-left:23%">
        <tr>
        <td class="box" style="left:35%; position:initial; transform: translate(0,0);">
            <h2 class="indexh2" style="text-transform: uppercase">My Profile</h2>
            
           <%-- Таблица для отображения сведений о пользователе --%>
            <table style="margin-left: 15px">

             <%-- Имя пользователя --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label4" runat="server" Text="User Name" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="UserName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="UsernameValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

               <%-- Пароль --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label6" runat="server" Text="E-mail" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="Email" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="EmailValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

            <%-- Адрес электронной почты --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label5" runat="server" Text="Password" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="Password" runat="server" TextMode="Password" 
                        Placeholder="Current Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="PasswordValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

             <%-- Кнопка "Сгенерировать" --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Создать Код" onclick="GenerateOTP"/></td>
                </tr>

                <%-- Код подтвеждения --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label7" runat="server" Text="OTP" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="OtpBox" runat="server" TextMode="Password" 
                        Placeholder="OTP"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="OtpLabel" ID="OtpValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

                <%-- Кнопка --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Update Profile" onclick="UpdateProfileClick"/></td>
                </tr>
            </table>

           <%-- Проверка входных данных --%>
            <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="Email" runat="server" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
            ErrorMessage="Enter valid email address<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>
            <asp:Label ID="UpdateLabel" runat="server" ForeColor="White"></asp:Label>

        </td>
        <td style="padding: 10px"></td>


        <td class="box" style="left: 65%; position:initial; transform: translate(0,0);">
            <h2 class="indexh2" style="text-transform: uppercase">Update password</h2>

          <%-- Таблица для смены пароля --%>
            <table style="margin-left: 15px">

               <%-- Текущий пароль --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label1" runat="server" Text="Password" ></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="OldPassword" runat="server" TextMode="Password" 
                        Placeholder="Current Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="OldPasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

            <%-- Новый пароль --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label2" runat="server" Text="New Password" ></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="NewPassword" runat="server" TextMode="Password" 
                        Placeholder="New Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="NewPasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

                <%-- Повторите новый пароль --%>
                <tr class="profile_section">
                    <td style="text-align:center; max-width:20px;"><asp:Label CssClass="ProfileLabel" ID="Label3" runat="server"
                        Text="Retype New Password"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="ReNewPassword" runat="server" TextMode="Password" 
                        Placeholder="New Password"></asp:TextBox></td>
                </tr>
                    <%-- Проверка входных данных --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:RegularExpressionValidator ID="PasswordValidator" ControlToValidate="NewPassword" runat="server" 
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$" 
                    ErrorMessage="Password should contain minimum eight and<br>maximum 15 characters, at least one uppercase letter,<br>one lowercase letter, 
                    one number and one special character<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="RePasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

                <%-- Кнопка --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Update Password" OnClick="UpdatePassword_Click" /></td>
                </tr>
            </table>
            <asp:Label ID="PasswordUpdateLabel" runat="server" ForeColor="White"></asp:Label>
        </td>
        </tr>
    </table>

</asp:Content>