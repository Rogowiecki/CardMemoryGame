
<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ViewScores.aspx.cs" Inherits="CardMemoryGame.ViewScores" %>

    <%-- Содержимое панели навигации --%>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationContent" runat="server">

    <%-- Раскрывающийся список для вошедшего в систему пользователя --%>
    <asp:Menu ID="mnu" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" StaticEnableDefaultPopOutImage="false">
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

<%-- Содержимое для отображения результатов --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table >
        <tr>

            <%-- Отобразить общие 10 лучших результатов --%>
            <td class="box" style="left:35%; position:absolute; transform: translate(-50%,-50%);">
                <h2 class="indexh2" style="text-transform: uppercase">Top Scores</h2> 
                <asp:Table ID="tbl" runat="server" font-size="Large" />
            </td>

           <%-- Отображение 10 лучших результатов текущего пользователя --%>
            <td class="box" style="left: 65%; position:absolute; transform: translate(-50%,-50%);">
                <h2 class="indexh2" style="text-transform: uppercase">Your Top 10 Scores</h2>
                <asp:Table ID="tb2" runat="server" font-size="Large" />
            </td>

        </tr>
    </table>

</asp:Content>
