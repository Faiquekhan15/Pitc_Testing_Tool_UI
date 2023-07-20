<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pitc_Tool.aspx.cs" Inherits="WebApplication1.Pitc_Testing_Tool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <title>Communication Verification</title>
    <link href="login.css" rel="stylesheet" />
   <link href="animate.css" rel="stylesheet" />
   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
   <script src="script.js"></script>
     <style type="text/css">
         .auto-style5 {
             width: 32px;
             height: 26px;
         }
         .auto-style6 {
             width: 84px;
             height: 44px;
         }
         .auto-style13 {
             height: 44px;
         }
         .auto-style14 {
             width: 48px;
             height: 44px;
         }
         .auto-style17 {
             width: 69px;
             height: 44px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
           <div id="topbar">
        
           <strong style="color:white; margin-left:10%;">  TRANSFOPOWER INDUSTRIES COMMUNICATION VERIFICATION TOOL ©      
               
           </strong>
               <img src="Untitled-1.png" /> <asp:Button ID="logout" CssClass="myButton" Text="LOGOUT" runat="server" OnClick="logout_Click" />
            <br />
        </div>
        <div>
       <!--     <div id='cssmenu' style="margin-left:4%; width:95%;">
             
<ul>
   <li style=margin-left:73% class='#'><a href="WebForm3.aspx"><span>Change Password</span></a></li>
   <li class='last'><a href="WebForm1.aspx"><span>Logout</span></a></li>
</ul>
</div>-->
        <div " class ="transbox1">
            <br />
            <br />
            
        <asp:Timer runat="server" id="UpdateTimer" interval="5000" ontick="UpdateTimer_Tick" Enabled="false" />
        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                <asp:PostBackTrigger ControlID="btnkWhReport" />
            </Triggers>
            <ContentTemplate>
<table border="1" style="width:110%;background-color:whitesmoke; text-align:center; align-self:initial">
  <tr>
    <th class="auto-style17">MSN</th>
    <th class="auto-style13">#</th> 
    <th class="auto-style6">Connection Status</th>
    <th class="auto-style14">Report</th>
  </tr>
  <tr>
      
    <td class="auto-style5">
        <asp:TextBox ID ="Serial" runat="server" MaxLength="10" Width="80px" Visible="false"/>
        <asp:DropDownList ID="DropDownList1" style="max-height:100px; overflow:auto;" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="Button1" CssClass="fb8" runat="server" OnClick="Button1_Click" Text="Select" />
         <ajaxtoolkit:filteredtextboxextender ID="ftbe" runat="server"
    TargetControlID="Serial"         
    FilterType="Numbers"
    InvalidChars="&" />
        </td>


      
    <td class="auto-style5">
        <asp:Label ID="MSNLabel" runat="server" Text="1."></asp:Label>
      </td> 
    <td class="auto-style5">
        <asp:Label ID="TimeStamp" CssClass="animated fadeIn" runat="server" Text="--"></asp:Label>
      </td>
      <td class="auto-style5">
          <asp:Button ID="btnkWhReport" CssClass="fb8" runat="server" OnClick="btnkWhReport_Click" Text="Report" Width="55px" Enabled="false" />
      <asp:TextBox ID ="Stamp_TextBox" runat="server" MaxLength="10" Width="80px" Visible="false"/>
        <asp:DropDownList ID="Selection_List" style="max-height:100px; overflow:auto;" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
         <ajaxtoolkit:filteredtextboxextender ID="Filteredtextboxextender1" runat="server"
    TargetControlID="Serial"         
    FilterType="Numbers"
    InvalidChars="&" />
      </td>

  </tr>
    <asp:TextBox ID="conStatus" runat="server" Visible="false" Text="--"></asp:TextBox>
</table>
            <br />
            <span style="margin-left:100%">
            <asp:Button ID="btnRead" class="myButton" runat="server" Text="Read"  OnClick="btnRead_Click" />
                </span>
                <br />
                <br />
               </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
        
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <div id="bottombar">
        
           <strong style="color:white; margin-left:10%;">  Copyright ©       
               <a style="cursor:pointer" onclick="window.open('http://www.transfopower.pk/', '_self');"> Transfopower Industries (Pvt) Limited</a> &nbsp; &nbsp;
               Powered By : Transfopower R&D
           </strong>
            <br />
        </div>
</asp:Content>
