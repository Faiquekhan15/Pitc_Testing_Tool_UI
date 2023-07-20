<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pitc_Testing_Tool_Login.aspx.cs" Inherits="WebApplication1.Pitc_Testing_Tool_Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.13.0/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="login1.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog1").html(message);
                $("#dialog1").dialog({
                    title: "Notification!",
                    resizable: false,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

        <div style="margin-left:30%;">
            <br /><br />

          
        <img width="300" height="70" alt="LinkedIn" style="margin-left:25px" src="logo.png" />
            <br />

            <span style=" margin-left:5%; color:black; font-size:17px; text-shadow: 0 0 3px gray; "> Communication Verification Terminal
            </span>
        <br />
            <br />
            <asp:TextBox ID="user" runat="server" style="font-size:large" placeholder="Username" CssClass="txtboxattributes" Width="314px" ></asp:TextBox><br />

            <br /> 
            
            <asp:TextBox ID="passwordbox" TextMode="Password" runat="server" style="font-size:large" placeholder="password" cssclass="txtboxattributes" Width="315px" ></asp:TextBox><br />
            <br />
            
            <asp:CheckBox ID="RememberMeCheckBox" runat="server" Text="Remember Me" />

            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSignIn" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Button ID="btnSignIn" style="color:white;  text-shadow: 0 0 3px black;" Text="Log In" runat="server" cssclass="txtboxattributes" Width="320px" OnClick="btnSignIn_Click" /><br /><br />
                </ContentTemplate>
            </asp:UpdatePanel>
            
              <div id="dialog1" style="display: none">
</div>
            <br /><br />
            </div>
    <br />
    <br />
    <br />
        <div style="background-color:rgba(64, 55, 55, 0.86)">
            <br />
           <strong style="color:white; margin-left:18%;">  Copyright ©       
               <a style="cursor:pointer" onclick="window.open('http://www.transfopower.pk/', '_self');"> Transfopower Industries (Pvt) Limited</a> &nbsp; &nbsp;
               Powered By : Transfopower R&D
           </strong>'
            <br />
            <br />
        </div>
</asp:Content>
