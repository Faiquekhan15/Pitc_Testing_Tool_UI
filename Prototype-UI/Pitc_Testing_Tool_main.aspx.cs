using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    
    public partial class WebFormSide_Menu : System.Web.UI.Page
    {
        private float kWh_Import = 0, kVARh_Import = 0, kWh_Export = 0, kVARh_Export = 0;
        private float kWh= 0, kVARh = 0;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!((bool)(Session["BoolV"])))
                {
                    Response.Redirect("Pitc_Testing_Tool_Login.aspx");
                }
            }
            catch (Exception) { Response.Redirect("Pitc_Testing_Tool_Login.aspx"); }
            try
            {
                if (!IsPostBack)
                {

                    testlist();

                    if (Selection_List.Items.Count < 1)
                    {
                        Selection_List.Items.Add("Before");
                        Selection_List.Items.Add("During");
                        Selection_List.Items.Add("After");
                        if (Selection_List1.Items.Count < 1)
                        {
                            Selection_List1.Items.Add("Before");
                            Selection_List1.Items.Add("During");
                            Selection_List1.Items.Add("After");
                        }

                    }
                    


                        // set the default selection

                    }
                if (DropDownList1.Items.Count < 1 )
                {
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT serial FROM meter1 WHERE mode = 1 ORDER BY serial;", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DropDownList1.Items.Add((string)(dr["serial"]));
                            }
                            dr.Close();
                        }
                        if (DropDownList2.Items.Count < 1)
                        {
                            using (OdbcCommand command = new OdbcCommand("SELECT serial FROM meter1 WHERE mode = 2 ORDER BY serial;", connection))
                            using (OdbcDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    DropDownList2.Items.Add((string)(dr["serial"]));
                                }
                                dr.Close();
                            }
                        }

                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.Message;
                TimeStamp1.Text = ex.Message;
            }

            int co = DropDownList1.Items.Count;
            if (Serial.Text.Length < 1)
            {
                if (co > 0)
                {
                    DropDownList1.SelectedIndex = 0;
                    Serial.Text = DropDownList1.SelectedValue;
                }
            }

            int co1 = DropDownList2.Items.Count;
            if (Serial1.Text.Length < 1)
            {
                if (co1 > 0)
                {
                    DropDownList2.SelectedIndex = 0;
                    Serial1.Text = DropDownList2.SelectedValue;
                }
            }


        }


        public void testlist()
        {
            if (Test_List.Items.Count < 1)
            {
                Test_List.Items.Add("Impulse Voltage Test");
                Test_List.Items.Add("AC Voltage Test");
                Test_List.Items.Add("Insulation Resistance Test");
                Test_List.Items.Add("Test of Accuracy Due To Variation of Current");
                Test_List.Items.Add("Test of Meter Constant");
                Test_List.Items.Add("Test of Starting Condition");
                Test_List.Items.Add("Test of No-load Condition");
                Test_List.Items.Add("Ambient Temperature Variation");
                Test_List.Items.Add("Voltage Variation");
                Test_List.Items.Add("Frequency Variation");
                Test_List.Items.Add("Harmonic Components In The Current & Voltage Circuit (5th Harmonics)");
                Test_List.Items.Add("Sub-Harmonics in the AC Current Circuit");
                Test_List.Items.Add("DC and Even Harmonics in the AC Current Circuit");
                Test_List.Items.Add("Odd Harmonics in the AC Current Circuit");
                Test_List.Items.Add("Continuous Magnetic Induction of External Origin");
                Test_List.Items.Add("Magnetic Induction of External Origin 0.5mT");
                Test_List.Items.Add("Test of Power Consumption of Voltage & Current Circuit");
                Test_List.Items.Add("Test of Influence of Supply Voltage");
                Test_List.Items.Add("Test of Influence of Short Time Over-Current");
                Test_List.Items.Add("Test of Influence of Self Heating");
                Test_List.Items.Add("Test of Influence of Heating");
                Test_List.Items.Add("Radio Interference Suppression");
                Test_List.Items.Add("Fast Transient Burst Test");
                Test_List.Items.Add("Test of immunity to Electromagnetic RF Field");
                Test_List.Items.Add("Test of Immunity to Conducted Disturbance, induced by radio frequency field.");
                Test_List.Items.Add("Immunity to Electrostatic Discharges");
                Test_List.Items.Add("Surge Immunity Test");
                Test_List.Items.Add("Dry Heat Test");
                Test_List.Items.Add("Cold Test");
                Test_List.Items.Add("Damp Heat Cyclic Test");
                Test_List.Items.Add("Solar Radiation Test");
                Test_List.Items.Add("Vibration Test");
                Test_List.Items.Add("Shock Test");
                Test_List.Items.Add("Spring Hammer test");
                Test_List.Items.Add("Protection Against Penetration of Dust");
                Test_List.Items.Add("Protection against Penetration of Water");
                Test_List.Items.Add("Impact Test");
                Test_List.Items.Add("Influence of Radar Magnet Test(0.7 Tesla)");
                Test_List.Items.Add("Influence of Mobile Phone");
                Test_List.Items.Add("Influence of CD Drive");
                Test_List.Items.Add("Initial Startup of Energy Meter");
                Test_List.Items.Add("Verification of (Temperature) Limit Range of Operation(-25 C to 80 C)");
                Test_List.Items.Add("Influence of Extreme Temperature Conditions(80 C for 4-Hours)");
                Test_List.Items.Add("Energy Recording With & Without Neutral");
                Test_List.Items.Add("Energy Recording in Different Quadrants");
                Test_List.Items.Add("Software Functionality, Security Features and Diagnostics");
                Test_List.Items.Add("Meter Cover Opened Detection");
                Test_List.Items.Add("Clearance & Creepage Measurement");
                Test_List.Items.Add("Battery Life Test");
                Test_List.Items.Add("Super Capacitor verification test for RTC");
                Test_List.Items.Add("Verification Of Component");
                Test_List.Items.Add("Resistance to Heat and Fire");
                Test_List.Items.Add("Terminal Block Material (ISO-75)Test (Thermal Deflections)");
                Test_List.Items.Add("Neoprene Rubber Gas Kit Verification & Hardness Test");
                Test_List.Items.Add("Electrical Conductivity Test of Fixed and Moving Parts including Screws of terminals");
                Test_List.Items.Add("Thickness of Tin Coating Measurement");
                Test_List.Items.Add("Verification of Dimensions");
            }
            if (Test_List1.Items.Count < 1)
            {
                Test_List1.Items.Add("Impulse Voltage Test");
                Test_List1.Items.Add("AC Voltage Test");
                Test_List1.Items.Add("Insulation Resistance Test");
                Test_List1.Items.Add("Test of Accuracy Due To Variation of Current");
                Test_List1.Items.Add("Test of Meter Constant");
                Test_List1.Items.Add("Test of Starting Condition");
                Test_List1.Items.Add("Test of No-load Condition");
                Test_List1.Items.Add("Ambient Temperature Variation");
                Test_List1.Items.Add("Voltage Variation");
                Test_List1.Items.Add("Frequency Variation");
                Test_List1.Items.Add("Harmonic Components In The Current & Voltage Circuit (5th Harmonics)");
                Test_List1.Items.Add("Sub-Harmonics in the AC Current Circuit");
                Test_List1.Items.Add("DC and Even Harmonics in the AC Current Circuit");
                Test_List1.Items.Add("Odd Harmonics in the AC Current Circuit");
                Test_List1.Items.Add("Continuous Magnetic Induction of External Origin");
                Test_List1.Items.Add("Magnetic Induction of External Origin 0.5mT");
                Test_List1.Items.Add("Test of Power Consumption of Voltage & Current Circuit");
                Test_List1.Items.Add("Test of Influence of Supply Voltage");
                Test_List1.Items.Add("Test of Influence of Short Time Over-Current");
                Test_List1.Items.Add("Test of Influence of Self Heating");
                Test_List1.Items.Add("Test of Influence of Heating");
                Test_List1.Items.Add("Radio Interference Suppression");
                Test_List1.Items.Add("Fast Transient Burst Test");
                Test_List1.Items.Add("Test of immunity to Electromagnetic RF Field");
                Test_List1.Items.Add("Test of Immunity to Conducted Disturbance, induced by radio frequency field.");
                Test_List1.Items.Add("Immunity to Electrostatic Discharges");
                Test_List1.Items.Add("Surge Immunity Test");
                Test_List1.Items.Add("Dry Heat Test");
                Test_List1.Items.Add("Cold Test");
                Test_List1.Items.Add("Damp Heat Cyclic Test");
                Test_List1.Items.Add("Solar Radiation Test");
                Test_List1.Items.Add("Vibration Test");
                Test_List1.Items.Add("Shock Test");
                Test_List1.Items.Add("Spring Hammer test");
                Test_List1.Items.Add("Protection Against Penetration of Dust");
                Test_List1.Items.Add("Protection against Penetration of Water");
                Test_List1.Items.Add("Impact Test");
                Test_List1.Items.Add("Influence of Radar Magnet Test(0.7 Tesla)");
                Test_List1.Items.Add("Influence of Mobile Phone");
                Test_List1.Items.Add("Influence of CD Drive");
                Test_List1.Items.Add("Initial Startup of Energy Meter");
                Test_List1.Items.Add("Verification of (Temperature) Limit Range of Operation(-25 C to 80 C)");
                Test_List1.Items.Add("Influence of Extreme Temperature Conditions(80 C for 4-Hours)");
                Test_List1.Items.Add("Energy Recording With & Without Neutral");
                Test_List1.Items.Add("Energy Recording in Different Quadrants");
                Test_List1.Items.Add("Software Functionality, Security Features and Diagnostics");
                Test_List1.Items.Add("Meter Cover Opened Detection");
                Test_List1.Items.Add("Clearance & Creepage Measurement");
                Test_List1.Items.Add("Battery Life Test");
                Test_List1.Items.Add("Super Capacitor verification test for RTC");
                Test_List1.Items.Add("Verification Of Component");
                Test_List1.Items.Add("Resistance to Heat and Fire");
                Test_List1.Items.Add("Terminal Block Material (ISO-75)Test (Thermal Deflections)");
                Test_List1.Items.Add("Neoprene Rubber Gas Kit Verification & Hardness Test");
                Test_List1.Items.Add("Electrical Conductivity Test of Fixed and Moving Parts including Screws of terminals");
                Test_List1.Items.Add("Thickness of Tin Coating Measurement");
                Test_List1.Items.Add("Verification of Dimensions");
            }
        }


        protected void btnRead_Click(object sender, EventArgs e)
        {
            if (conStatus.Text == "Connected")
            {
                UpdateTimer.Enabled = true;
                TimeStamp.Text = "Reading....";

                int n = 0;
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT msn FROM meter1 WHERE serial = '" + Serial.Text + "';", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            n = (int)(dr["msn"]);
                        dr.Close();
                    }
                    connection.Close();
                }

                Session["msn"] = n;

                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("UPDATE meter.meter SET retry ='1' WHERE `serial` ='" + Serial.Text + "';", connection))
                        command.ExecuteNonQuery();
                    connection.Close();
                }

                
            }
            else
            {
                TimeStamp.Text = "Not Connected";
            }
        }


        protected void Connection_Build_Click(object sender, EventArgs e)
        {
            string timecomp = "";

            try
            {
                string[] MSNList = new string[1024];
                int meterCount = 0;
                bool flag = false;
                int n = 0;
                int mode = 0;

                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT `serial` FROM world.meter;", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MSNList[meterCount] = (string)(dr["serial"]);
                            meterCount++;
                        }
                        dr.Close();
                    }
                    connection.Close();
                }

                for (int i = 0; i < meterCount; i++)
                {
                    if (Serial.Text == MSNList[i])
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    Session["Serial"] = Serial.Text;
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT msn,mode FROM meter1 WHERE serial = '" + Serial.Text + "';", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                n = (int)(dr["msn"]);
                                mode = int.Parse(dr["mode"].ToString());
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }

                    Session["msn"] = n;

                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT times FROM meter1 WHERE serial = '" + Serial.Text + "';", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                timecomp = (string)(dr["times"]);
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }
                    DateTime comp = DateTime.Parse(timecomp);
                    TimeSpan diff = TimeSpan.Parse("00:04:20.9896330");
                    if (DateTime.Now.Subtract(comp) > diff)
                    {
                        TimeStamp.Text = "Disconnected";
                        conStatus.Text = "Disconnected";
                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("UPDATE meter1 SET connected ='0' WHERE serial = '" + Serial.Text + "'", connection))
                                command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else
                    {
                        TimeStamp.Text = "Connected";
                        conStatus.Text = "Connected";
                    }
                    Session["retry"] = 0;
                }
                else { TimeStamp.Text = "Not Registered."; conStatus.Text = "Not Registered."; }
            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.ToString();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string timecomp = "";

            try
            {
                string[] MSNList = new string[1024];
                int meterCount = 0;
                bool flag = false;
                int n = 0;
                int mode = 0;

                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT `serial` FROM world.meter;", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MSNList[meterCount] = (string)(dr["serial"]);
                            meterCount++;
                        }
                        dr.Close();
                    }
                    connection.Close();
                }

                for (int i = 0; i < meterCount; i++)
                {
                    if (Serial1.Text == MSNList[i])
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    Session["Serial1"] = Serial1.Text;
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT msn,mode FROM meter1 WHERE serial = '" + Serial1.Text + "';", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                n = (int)(dr["msn"]);
                                mode = int.Parse(dr["mode"].ToString());
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }

                    Session["msn1"] = n;

                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT times FROM meter WHERE serial = '" + Serial1.Text + "';", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                timecomp = (string)(dr["times"]);
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }
                    DateTime comp = DateTime.Parse(timecomp);
                    TimeSpan diff = TimeSpan.Parse("00:04:20.9896330");
                    if (DateTime.Now.Subtract(comp) > diff)
                    {
                        TimeStamp1.Text = "Disconnected";
                        conStatus1.Text = "Disconnected";
                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("UPDATE meter1 SET connected ='0' WHERE serial = '" + Serial.Text + "'", connection))
                                command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else
                    {
                        TimeStamp1.Text = "Connected";
                        conStatus1.Text = "Connected";
                    }
                    Session["retry1"] = 0;
                }
                else { TimeStamp1.Text = "Not Registered."; conStatus1.Text = "Not Registered."; }
            }
            catch (Exception ex)
            {
                TimeStamp1.Text = ex.ToString();
            }
        }

        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Session["Serial"] = Serial.Text;
                int n = (int)(Session["msn"]);
                int job = 1;
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT retry FROM world.meter where `serial` = '" + Serial.Text + "' and connected = 1;", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            job = (int)(dr["retry"]);
                        }
                        dr.Close();
                    }
                    connection.Close();
                }

                if (job == 0)
                {
                    bool read_available = false;
                    Session["read_available"] = read_available;
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT active, reactive, Time_stamp FROM world.`comm-verification` where `serial` = '" + Serial.Text + "' and Time_stamp > '" + DateTime.Now.AddMinutes(-1).ToString("yyyy/M/d HH:mm:ss") + "' order by Time_stamp desc limit 1;", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                kWh = (float)(dr["active"]);
                                kVARh = (float)(dr["reactive"]);
                                Session["date"] = dr["Time_stamp"].ToString();
                                read_available = true;
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }

                    if (read_available)
                    {
                        Session["read_available"] = read_available;
                        Session["kVARhVal"] = kVARh;
                        Session["kWhVal"] = kWh;

                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("SELECT Time_stamp FROM `comm-verification1` WHERE serial=" + Serial.Text + " order by Time_stamp desc limit 1;", connection))
                            using (OdbcDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {

                                }
                                dr.Close();
                            }
                            connection.Close();
                        }

                        long id = 0;
                        int komy = 0;

                        if (komy > 1)
                        {
                            using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                            {
                                connection.Open();
                                using (OdbcCommand command = new OdbcCommand("delete from `comm-verification1` where serial='" + Serial.Text + "' and Time_stamp < '" + DateTime.Now.AddMinutes(-2).ToString("yyyy/M/d HH:mm:ss") + "';", connection))
                                    command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                        Session["T"] = DateTime.Now;
                        btnkWhReport.Enabled = true;
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Read Complete.');</script>");
                        TimeStamp.Text = "Read Complete.";
                        UpdateTimer.Enabled = false;
                        Session["retry"] = 0;
                    }
                }
                else
                {
                    int o = (int)(Session["retry"]);
                    o++;
                    Session["retry"] = o;
                    TimeStamp.Text = "Reading...";
                    if (o > 8)
                    {
                        UpdateTimer.Enabled = false;
                        TimeStamp.Text = "Read Unsuccessful.";
                        Session["retry"] = 0;
                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("UPDATE meter.meter SET retry ='0' WHERE `serial` = '" + Serial.Text + "'", connection))
                                command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.ToString();
                btnkWhReport.Enabled = false;
                UpdateTimer.Enabled = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Serial.Text = DropDownList1.Text;
        }

        protected void btnRead_Click1(object sender, EventArgs e)
        {
            if (conStatus1.Text == "Connected")
            {
                Timer1.Enabled = true;
                TimeStamp1.Text = "Reading....";

                int n = 0;
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT msn FROM meter1 WHERE serial = '" + Serial.Text + "';", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            n = (int)(dr["msn"]);
                        dr.Close();
                    }
                    connection.Close();
                }

                Session["msn1"] = n;

                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("UPDATE world.meter SET retry ='1' WHERE `serial` ='" + Serial.Text + "';", connection))
                        command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                TimeStamp1.Text = "Not Connected";
            }

        }
        protected void btnkWhReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(bool)Session["read_Available"])
                {
                    TimeStamp.Text = "Reading Unavailable.";
                    return;
                }

                SecretBox1.Text = Test_List.SelectedValue;
                Stamp_TextBox1.Text = Selection_List.SelectedValue;
                Paragraph spaces = new Paragraph("\n\n\n");
                Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                Chunk horizontalSpace1 = new Chunk("                 ", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                Paragraph events1 = new Paragraph();
                events1.Add(horizontalSpace1);
                events1.Add(new Phrase(SecretBox1.Text));
                events1.Font.Size = 12f;
                Paragraph spaces2 = new Paragraph("\n");


                Paragraph pa = new Paragraph("Transfopower Communication Verification Tool\n\n\n");
                string imageURL = Server.MapPath(".") + "/trafo.jpg";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(140f, 60f);
                jpg.SpacingBefore = 10f;
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_RIGHT;
                Paragraph credits = new Paragraph("Transfopower Industries (Pvt) Ltd.\nCopyright @ Transfopower R&D Department 2023.");
                credits.Alignment = Element.ALIGN_RIGHT;
                credits.Font.Color = BaseColor.BLUE;
                credits.Font.SetFamily(Font.FontFamily.COURIER.ToString());
                credits.Font.Size = 8f;

                Chunk horizontalSpace = new Chunk("                 ", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                Paragraph events = new Paragraph();
                events.Add(horizontalSpace);
                events.Add(new Phrase(Selection_List.SelectedValue)); // Use Selection_List.SelectedValue instead of Stamp_TextBox.Text
                events.Font.Size = 12f;
                events.SpacingAfter = 10f; // Add spacing after the events paragraph

                pa.Alignment = Element.ALIGN_CENTER;
                pa.Font.SetStyle(Font.BOLD);
                pa.Font.Size = 14f;

                PdfPTable table = new PdfPTable(2);
                PdfPCell cell = new PdfPCell(new Phrase("TPI-34G Online Communication Verification Results"));
                cell.Colspan = 2;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.PaddingLeft = 5f;
                cell.PaddingRight = 5f;
                cell.HorizontalAlignment = 1;

                table.AddCell(cell);
                table.HorizontalAlignment = 1;
                table.AddCell("Time Stamp");
                table.AddCell((string)(Session["date"]));
                table.AddCell("Meter Serial Number");
                table.AddCell((string)(Session["Serial"]));
                table.AddCell("kWh Total");
                table.AddCell(((float)(Session["kWhVal"])).ToString());
                table.AddCell("kVARh Total");
                table.AddCell(((float)(Session["kVARhVal"])).ToString());



                doc.Add(pa);
                doc.Add(events1);
                doc.Add(spaces2);
                doc.Add(events);
                doc.Add(table);
                doc.Add(spaces);
                doc.Add(jpg);
                doc.Add(credits);

                doc.Close();


                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString() + ".pdf");
                Response.Write(doc);
                Response.End();

            }
            catch (Exception ex)
            {
                // Handle exception
            }

        }

        protected void btnkWhReport_Click1(object sender, EventArgs e)
        {
            DateTime DT;
            try
            {
                if (!(bool)Session["read_Available1"])
                {
                    TimeStamp1.Text = "Reading Unavailable.";
                    return;
                }
                Stamp_TextBox.Text = Selection_List1.SelectedValue;
                SecretBox2.Text = Test_List1.SelectedValue;
                
                DT = (DateTime)(Session["T"]);
                Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();

                Chunk horizontalSpace1 = new Chunk("                 ", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                Paragraph events1 = new Paragraph();
                events1.Add(horizontalSpace1);
                events1.Add(new Phrase(SecretBox2.Text));
                events1.Font.Size = 12f;
                Paragraph spaces2 = new Paragraph("\n");

                Paragraph pa = new Paragraph("Transfopower Communication Verification Tool\n \n \n \n");
                string imageURL = Server.MapPath(".") + "/trafo.jpg";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //Resize image depend upon your need
                jpg.ScaleToFit(140f, 60f);
                //Give space before image
                jpg.SpacingBefore = 10f;
                //Give some space after the image
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_RIGHT;
                Paragraph credits = new Paragraph("Transfopower Industries (Pvt) Ltd.\n Copyright @ Transfopower R&D Department 2023.");
                Paragraph spaces = new Paragraph("\n\n\n");

                credits.Alignment = Element.ALIGN_RIGHT;
                credits.Font.Color = BaseColor.BLUE;
                credits.Font.SetFamily(Font.FontFamily.COURIER.ToString());
                credits.Font.Size = 8f;

                // Add event label
                Chunk horizontalSpace = new Chunk("                 ", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                Paragraph events = new Paragraph();
                events.Add(horizontalSpace);
                events.Add(new Phrase(Stamp_TextBox.Text));
                events.Font.Size = 12f;
                Paragraph spaces1 = new Paragraph("\n");

                pa.Alignment = Element.ALIGN_CENTER;
                pa.Font.SetStyle(Font.BOLD);
                pa.Font.Size = 14f;

                PdfPTable table = new PdfPTable(2);


                Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                // create a font for the brackets
                Font bracketFont = FontFactory.GetFont(FontFactory.HELVETICA, 14);

                // create the phrase with the text and the brackets
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk("TPI-34G ", textFont));
                phrase.Add(new Chunk("(", bracketFont));
                phrase.Add(new Chunk("NET METER", textFont));
                phrase.Add(new Chunk(") ", bracketFont));
                phrase.Add(new Chunk("Online Communication Verification Results", textFont));

                PdfPCell cell = new PdfPCell(phrase);
                cell.Colspan = 2;
                cell.PaddingTop = 5f; //Add some padding to the top of the cell
                cell.PaddingBottom = 5f; //Add some padding to the bottom of the cell
                cell.PaddingLeft = 5f; //Add some padding to the left of the cell
                cell.PaddingRight = 5f; //Add some padding to the right of the cell
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);
                table.HorizontalAlignment = 1;
                table.AddCell("Time Stamp");
                table.AddCell((string)(Session["date"]));
                table.AddCell("Meter Serial Number");
                table.AddCell((string)(Session["Serial1"]));



                table.AddCell("kWh Import");
                table.AddCell(((float)(Session["kWhVal_import"])).ToString());
                table.AddCell("kWh Export");
                table.AddCell(((float)(Session["kWhVal_export"])).ToString());
                table.AddCell("kVARh Import");
                table.AddCell(((float)(Session["kVARhVal_import"])).ToString());
                table.AddCell("kVARh Export");
                table.AddCell(((float)(Session["kVARhVal_export"])).ToString());


                doc.Add(events1);
                doc.Add(spaces2);
                doc.Add(pa);

                doc.Add(events);
                doc.Add(spaces1);

                doc.Add(table);
                //doc.Add(paragraph);

                doc.Add(spaces);
                doc.Add(jpg);
                doc.Add(credits);
                doc.Close();

                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + DT.ToString() + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(doc);
                Response.End();
            }
            catch (Exception ex)
            { }

        }

       

        protected void UpdateTimer_Tick1(object sender, EventArgs e)
        {
            try
            {
                Session["Serial1"] = Serial1.Text;
                int n = (int)(Session["msn1"]);
                int job = 1;
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT retry FROM world.meter where `serial` = '" + Serial1.Text + "' and connected = 1;", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            job = (int)(dr["retry"]);
                        }
                        dr.Close();
                    }
                    connection.Close();
                }

                if (job == 0)
                {
                    bool read_available = false;
                    Session["read_available1"] = read_available;
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT active_import,active_export,reactive_import,reactive_export, Time_stamp FROM world.`ver` where `serial` = '" + Serial1.Text + "' and Time_stamp > '" + DateTime.Now.AddMinutes(-1).ToString("yyyy/M/d HH:mm:ss") + "' order by Time_stamp desc limit 1;", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                kWh_Import = (float)(decimal)(dr["active_import"]);
                                kWh_Export = (float)(decimal)(dr["active_export"]);
                                kVARh_Import = (float)(decimal)(dr["reactive_import"]);
                                kVARh_Export = (float)(decimal)(dr["reactive_export"]);

                                Session["date"] = dr["Time_stamp"].ToString();
                                read_available = true;
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }

                    if (read_available)
                    {
                        Session["read_available1"] = read_available;
                        Session["kVARhVal_import"] = kVARh_Import;
                        Session["kVARhVal_export"] = kVARh_Export;
                        Session["kWhVal_export"] = kWh_Export;
                        Session["kWhVal_import"] = kWh_Import;

                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("SELECT Time_stamp FROM `ver` WHERE serial=" + Serial1.Text + " order by Time_stamp desc limit 1;", connection))
                            using (OdbcDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {

                                }
                                dr.Close();
                            }
                            connection.Close();
                        }

                        long id = 0;
                        int komy = 0;

                        if (komy > 1)
                        {
                            using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                            {
                                connection.Open();
                                using (OdbcCommand command = new OdbcCommand("delete from `ver` where serial='" + Serial1.Text + "' and Time_stamp < '" + DateTime.Now.AddMinutes(-2).ToString("yyyy/M/d HH:mm:ss") + "';", connection))
                                    command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                        Session["T"] = DateTime.Now;
                        btnkWhReport1.Enabled = true;
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Read Complete.');</script>");
                        TimeStamp1.Text = "Read Complete.";
                        Timer1.Enabled = false;
                        Session["retry1"] = 0;
                    }
                }
                else
                {
                    int o = (int)(Session["retry1"]);
                    o++;
                    Session["retry1"] = o;
                    TimeStamp.Text = "Reading...";
                    if (o > 8)
                    {
                        Timer1.Enabled = false;
                        TimeStamp1.Text = "Read Unsuccessful.";
                        Session["retry"] = 0;
                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("UPDATE world.meter SET retry ='0' WHERE `serial` = '" + Serial.Text + "'", connection))
                                command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TimeStamp1.Text = ex.ToString();
                btnkWhReport1.Enabled = false;
                Timer1.Enabled = false;
            }
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            Session["BoolV"] = null;
            Response.Redirect("Pitc_Testing_Tool_Login.aspx");
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Serial1.Text = DropDownList2.Text;
        }



    }
}