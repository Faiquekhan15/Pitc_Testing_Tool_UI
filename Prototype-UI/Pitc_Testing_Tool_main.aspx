<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pitc_Testing_Tool_main.aspx.cs" Inherits="WebApplication1.WebFormSide_Menu" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <style>
    body 
	{
		font-family: 'Segoe UI', sans-serif;
		background-color: #f7f7f7;
		margin: 0;
		padding: 0;
	}
	#topbar 
	{
		background-color: #333;
		color: #fff;
		display: flex;
		align-items: center;
		justify-content: space-between;
		padding: 10px;
		font-size: 16px;
	}
	#topbar img 
	{
		margin-left: 10px;
	}
	#logout 
	{
		background-color: transparent;
		border: none;
		color: #fff;
		font-size: 16px;
		cursor: pointer;
		transition: all 0.3s ease;
	}
	#logout:hover 
	{
		background-color: #fff;
		color: #333;
	}
	.tab-container 
	{
		background-color: #fff;
		margin: 0 auto;
		padding: 20px;
		box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
		margin-top: 20px;
		border-radius: 4px;
	}
	.tabs 
	{
		display: flex;
		justify-content: space-between;
		margin-bottom: 20px;
		border-bottom: 2px solid #ccc;
	}
	.tab 
	{
		display: none;
	}
	.tab.active 
	{
		display: block;
	}
	.tabs button 
	{
		background-color: transparent;
		border: none;
		padding: 10px 20px;
		font-size: 16px;
		cursor: pointer;
		color: #333;
		transition: all 0.3s ease;
	}
	.tabs button:hover 
	{
		background-color: #333;
		color: #fff;
	}
	/* Global styles */
	* 
	{
		box-sizing: border-box;
		margin: 0;
		padding: 0;
	}
	body
    {
		font-family: Arial, sans-serif;
		font-size: 16px;
		line-height: 1.4;
	}
	button 
	{
        width:30%;
        border-radius:50px;
        border:none;
		font-family: Arial, sans-serif;
		font-size: 16px;
		line-height: 1.4;
	}
	img 
	{
		max-width: 100%;
		height: auto;
	}
	/* Header styles */
	#topbar strong 
	{
		margin-left: 10%;
	}
	/* Tabs styles */
	.tabs 
	{
		background-color: lightgray;
	}
	.tabs table 
	{
		border-collapse: collapse;
		width: 100%;
		margin: 0 auto;
	}
	.tabs td 
	{
		text-align: center;
		padding: 10px;
		width: 50%;
	}
	/* Content styles */
	.content 
	{
		display: none;
		padding: 10px;
	}
	.content.active 
	{
		display: block;
	}

	/* Meter styles */
	table 
	{
		border-collapse: collapse;
		width: 100%;
	}
	th,td 
	{
		text-align: center;
		padding: 10px;
	}
	tr 
	{
		background-color:white;
    }
	th 
	{
	   background-color: white;
	}
	.msn 
	{
		width: 69px;
        height: 44px;
    }
	.number 
	{
        height: 44px;
    }
	.connection-status 
	{
        width: 84px;
        height: 44px;
    }
	.report 
	{
        width: 48px;
        height: 44px;
    }
	.myButton 
	{
        background-color: #276873 !important;
        border: 1px solid #787878;
        color:white;
        cursor: pointer;
        font-size: 0.9em;
        font-weight: 600;
        padding: 7px;
        width: auto;
        border-radius: 2px !important;
    }
	.myButton:hover 
	{
		background-color: #fff;
	}
	.auto-style1 
	{
        width: 305px;
    }
	   .fb8 {
           background: linear-gradient(to bottom, #ff6b6b, #8f0d0d);

           color:white;
        border: 1px solid #787878;
        cursor: pointer;
        font-size: 0.9em;
        font-weight: 600;
        padding: 7px;
        width: auto;
        border-radius: 50px !important;
       }
	   .auto-style5 {
background: linear-gradient(to bottom, #ff6b6b, #8f0d0d);
           color: white;
           border: 1px solid #787878;
           cursor: pointer;
           font-size: 0.9em;
           font-weight: 600;
           padding: 7px;
           border-radius: 50px !important;
       }
	   .auto-style8 {
           width: 271px;
       }
       .auto-style10 {
           width: 458px;
       }
       .auto-style11 {
           width: 313px;
       }
       button.active {
           background-color: #276873 !important;
           background: linear-gradient(to bottom, #276873, #1b4150);

           color: white;
           border:none;

       }
.flash {
  animation: flash 1s infinite;
  color: red;
  font-size: 1.5rem;
  margin-left: 10px;
}

@keyframes flash {
  0% {
    opacity: 1;
  }
  50% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
	   .auto-style12 {
           overflow: auto;
           margin-left: 0;
       }
	</style>
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />

	</head>
	<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<div id="topbar">
	<strong style="margin-left:20%; font-family: 'Lobster', cursive;
  font-size: 1rem;
  letter-spacing: 0.05em;"> 
            TRANSFOPOWER INDUSTRIES 
            COMMUNICATION VERIFICATION TOOL © 2023<i class="fas fa-bolt flash"></i></strong> 
        
        <asp:Button ID="logout" CssClass="myButton" Text="LOGOUT" runat="server" OnClick="logout_Click" />
            <br />
        </div>
        <div style="padding: 12px;">
            <table class="t1">
                <tr>
                      <td><button type="button" onclick="openTab(event, 'tab1')" class="active">Normal Meters</button></td>
<td><button type="button" onclick="openTab(event, 'tab2')">Net Meters</button></td>

                  </tr>

            </table>

          </div>
        <div id="tab1" class="tab active">
            <div>
                <div id = "n1" class ="transbox1" style="
    padding: 50px;
">
            <br />
            <br />
			<asp:Timer runat="server" id="UpdateTimer" interval="5000" ontick="UpdateTimer_Tick" Enabled="false" />
			<asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
			<Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                <asp:PostBackTrigger ControlID="btnkWhReport" />
            </Triggers>
            <ContentTemplate>
                <table border="0" style="background-color:whitesmoke; text-align:center; align-self:initial">
                    <tr>
                        <th class="auto-style1">MSN</th>
                        <th class="auto-style18">#</th> 
                        <th class="auto-style8">Connection Status</th>
                        <th class="auto-style11">Report</th>
                        <th class="auto-style10">Duration</th>
                        <th class="auto-style10">Test Name</th>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:TextBox ID ="Serial" runat="server" MaxLength="10" Width="80px" Visible="false"/>
                            <asp:DropDownList ID="DropDownList1" style="max-height:100px; overflow:auto;" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">

                            </asp:DropDownList>
                            <asp:Button ID="Connection_Build" CssClass="fb8" runat="server" OnClick="Connection_Build_Click" Text="Connect" />
                            <ajaxtoolkit:filteredtextboxextender ID="ftbe" runat="server" TargetControlID="Serial" FilterType="Numbers" InvalidChars="&" />

                        </td>
                        <td>
                            
                            <asp:Label ID="MSNLabel" runat="server" Text="1."></asp:Label>

                        </td> 
                        <td class="auto-style8">
                            <asp:Label ID="TimeStamp" CssClass="animated fadeIn" runat="server" Text="--"></asp:Label>

                        </td>
                        <td class="auto-style11">
                            <asp:Button ID="btnkWhReport" CssClass="auto-style5" runat="server" OnClick="btnkWhReport_Click" Text="download " Width="109px" Enabled="false" />
                            
                            </td>
                        <td class="auto-style10">
                                  
                        <asp:TextBox ID ="Stamp_TextBox1" runat="server" MaxLength="10" Width="80px" Visible="False"/>
     
                            <asp:DropDownList ID="Selection_List" style="max-height:100px; overflow:auto;" runat="server">
        </asp:DropDownList>
                        </td>
                        <td class="auto-style11">
                                  
                            <asp:TextBox ID="SecretBox1" runat="server" MaxLength="10" Visible="False" Width="80px" />
                                  
                            <asp:DropDownList ID="Test_List" runat="server" CssClass="auto-style12" style="max-height:100px; ">
                            </asp:DropDownList>
     
                        </td>


                    </tr>
                    <asp:TextBox ID="conStatus" runat="server" Visible="false" Text="--"></asp:TextBox>

                </table>
                 <br />
                 <span style="margin-left:90%">
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
              
        
        </div>
            <div id="tab2" class="tab">
                <div>
                <div id = "n2" class ="transbox1"style="
    padding: 50px;
">
            <br />
            <br />
                  <asp:Timer runat="server" id="Timer1" interval="5000" ontick="UpdateTimer_Tick1" Enabled="false" />
                   <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                       <Triggers>
                           <asp:AsyncPostBackTrigger controlid="Timer1" eventname="Tick" />
                           <asp:PostBackTrigger ControlID="btnkWhReport1" />
                       </Triggers>
                       <ContentTemplate>
                           <table style="background-color:whitesmoke;">
                    <tr>
                        <th class="auto-style1">MSN</th>
                        <th class="auto-style18">#</th> 
                        <th class="auto-style8">Connection Status</th>
                        <th class="auto-style11">Report</th>
                        <th class="auto-style10">Duration</th>
                        <th class="auto-style10">Test Name</th>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:TextBox ID ="Serial1" runat="server" MaxLength="10" Width="80px" Visible="false"/>
                            <asp:DropDownList ID="DropDownList2" style="max-height:100px; overflow:auto;" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">

                            </asp:DropDownList>
                            <asp:Button ID="Button2" CssClass="fb8" runat="server" OnClick="Button2_Click" Text="Connect" />
                            <ajaxtoolkit:filteredtextboxextender ID="Filteredtextboxextender1" runat="server" TargetControlID="Serial1" FilterType="Numbers" InvalidChars="&" />

                        </td>
                        <td>
                            
                            <asp:Label ID="MSNLabel1" runat="server" Text="1."></asp:Label>

                        </td> 
                         <td class="auto-style8">
                            <asp:Label ID="TimeStamp1" CssClass="animated fadeIn" runat="server" Text="--"></asp:Label>

                        </td>
                        
                        <td class="auto-style11">
                            <asp:Button ID="btnkWhReport1" CssClass="auto-style5" runat="server" OnClick="btnkWhReport_Click1" Text="download " Width="109px" Enabled="false" />
                            
                            </td>

                        <td class="auto-style10">
                            <asp:TextBox ID ="Stamp_TextBox" runat="server" MaxLength="10" Width="80px" Visible="false"/>
                            <asp:DropDownList ID="Selection_List1" style="max-height:100px; overflow:auto;" runat="server">
        </asp:DropDownList>
                        </td>
                          <td class="auto-style11">
                              <asp:TextBox ID="SecretBox2" runat="server" MaxLength="10" Visible="False" Width="80px" />
                                 
                            <asp:DropDownList ID="Test_List1" runat="server" CssClass="auto-style12" style="max-height:100px; ">
                            </asp:DropDownList>
     
                        </td>

                    </tr>
                    <asp:TextBox ID="conStatus1" runat="server" Visible="false" Text="--"></asp:TextBox>

                </table>


                           <br />
            <span style="margin-left:90%">
                             <asp:Button ID="btnRead1" class="myButton" runat="server" Text="Read"  OnClick="btnRead_Click1" />

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
        </div>
        <div id="bottombar" style="background-color: #333;padding: 10px;">
             <strong style="color:white; margin-left:25%;">  Copyright ©       
               <a style="cursor:pointer" onclick="window.open('http://www.transfopower.pk/', '_self');"> Transfopower Industries (Pvt) Limited</a> &nbsp; &nbsp;
               Powered By : Transfopower R&D
           </strong>
            <br />
        </div>
            
    </form>
    <script>
        function openTab(evt, tabName) {
            var i, tabcontent, tablinks;

            tabcontent = document.getElementsByClassName("tab");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            tablinks = document.getElementsByTagName("button");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].classList.remove("active"); // remove active class from all buttons
            }

            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.classList.add("active"); // add active class to clicked button
        }

       
    </script>
</body>
</html>
