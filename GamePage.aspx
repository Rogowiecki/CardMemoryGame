<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GamePage.aspx.cs" Inherits="CardMemoryGame.GamePage" %>

<%-- Содержимое панели навигации --%>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationContent" runat="server">
    
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
                <asp:MenuItem Text="Страница игры" Value ="Game Page"></asp:MenuItem>
                <asp:MenuItem Text="Мой профиль" Value ="My Profile"></asp:MenuItem>
                <asp:MenuItem Text="Смотреть счёт" Value ="View scores"></asp:MenuItem>
                <asp:MenuItem Text="logout" Value="logout"></asp:MenuItem>
            </asp:MenuItem>
        </items>
    </asp:Menu>

</asp:Content>

<%-- Контент для игры --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
    <%-- Содержание инструкции --%>
    <div id="instruction" class=" overlay " OnClick="myFunction()">Инструкция
        <span class="overlay-small"><br>Нажмите на карту, чтобы перевернуть ее.<br>Если две последовательные перевернутые карты не совпадают, <br /> они автоматически переворачиваются обратно.<br>
            Игра заканчивается, когда истечет таймер или будут идентифицированы <br>все совпадающие карты.</span>
        <span class="overlay-small"><br>Начать игру</span>
    </div>
    
    <%-- Проигрыш контент --%>
    <div id="gameover" class="overlay">Время Вышло
         <asp:Button CssClass="overlay-small" style="cursor:pointer" ID="GameOverBtn" runat="server" Text="Сыграть ещё раз" OnClick="GameOver_RedirectToHome"/>
    </div>
    
   <%-- Контент Победа --%>
    <div id="victory" class="overlay">Победа!
         <%--<span class="overlay-small" >Click to Restart</span>--%> 
        <%--<asp:HiddenField ID="VictoryRedirect" runat="server" onclick="Victory_RedirectToHome" />--%>
        <%--<a class="overlay-small" runat="server" href="Default.aspx">Click to Restart</a>--%>   
        <asp:Label runat="server" ID="UserScore" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="UserScoreHidden" runat="server" ClientIDMode="Static"/>
         <asp:Button CssClass="overlay-small" style="cursor:pointer" ID ="VictoryBtn" runat="server" Text="Сыграть ещё раз" OnClick="Victory_RedirectToHome"/>
    </div>
    
   <%-- Содержимое игры --%>
    <div class="game-info-container">
        <div class="game-info">
            Время: <span id="time-remain"></span>
        </div>
        <div class="game-info">
            Повороты: <span id="filps">0</span>
        </div>
    </div>
    
    <div id="blocker"></div>
    <div id="dummygrid" ></div>
    <script type ="text/javascript">
        window.load = startinstruction();
    </script>

</asp:Content>

