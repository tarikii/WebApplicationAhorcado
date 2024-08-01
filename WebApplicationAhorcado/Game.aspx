<%@ Page Title="Juego del Ahorcado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="WebApplicationAhorcado.Game" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <link href="Styles/Game.css" rel="stylesheet" type="text/css" />
        
        <div>
            <h2 id="title"><%: Title %>.</h2>
        </div>

        <div>
            <asp:Panel ID="BoxesPanel" runat="server"></asp:Panel>
        </div>

        <br />
        <br />

        <div>
            <asp:Panel ID="LettersPanel" runat="server"></asp:Panel>
            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
        </div>

        <br />
        <br />

        <div>
            <asp:Label CssClass="attempts-label" ID="AttemptsLabel" runat="server" Text="Attempts: "></asp:Label>
        </div>

    </main>
</asp:Content>