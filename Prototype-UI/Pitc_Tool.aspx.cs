using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Timers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace WebApplication1
{
    public partial class Pitc_Testing_Tool : System.Web.UI.Page
    {
        private float kWh_Import = 0, kVARh_Import = 0 , kWh_Export = 0, kVARh_Export = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Selection_List.Items.Count < 1)
                {
                    Selection_List.Items.Add("Before");
                    Selection_List.Items.Add("Mid");
                    Selection_List.Items.Add("After");
                }
                // set the default selection
                
            }
            try
            {
                if (!((bool)(Session["BoolV"])))
                {
                    Response.Redirect("Pitc_Testing_Tool_Login.aspx");
                }
            }
            catch (Exception) { Response.Redirect("Pitc_Testing_Tool_Login.aspx"); }

            btnkWhReport.Enabled = false;

            try
            {
                if (DropDownList1.Items.Count < 1)
                {
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand("SELECT serial FROM meter1 order by serial;", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DropDownList1.Items.Add((string)(dr["serial"]));
                            }
                            dr.Close();
                        }
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.Message;
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
                    using (OdbcCommand command = new OdbcCommand("SELECT retry FROM meter.meter where `serial` = '" + Serial.Text + "' and connected = 1;", connection))
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
                        using (OdbcCommand command = new OdbcCommand("SELECT active_import,active_export,reactive_import,reactive_export, Time_stamp FROM meter.`ver` where `serial` = '" + Serial.Text + "' and Time_stamp > '" + DateTime.Now.AddMinutes(-1).ToString("yyyy/M/d HH:mm:ss") + "' order by Time_stamp desc limit 1;", connection))
                        using (OdbcDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                kWh_Import = (float)(dr["active_import"]);
                                kWh_Export = (float)(dr["active_export"]);
                                kVARh_Import = (float)(dr["reactive_import"]);
                                kVARh_Export = (float)(dr["reactive_export"]);

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
                        Session["kVARhVal_import"] = kVARh_Import;
                        Session["kVARhVal_export"] = kVARh_Export;
                        Session["kWhVal_export"] = kWh_Export;
                        Session["kWhVal_import"] = kWh_Import;

                        using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                        {
                            connection.Open();
                            using (OdbcCommand command = new OdbcCommand("SELECT Time_stamp FROM `ver` WHERE serial=" + Serial.Text + " order by Time_stamp desc limit 1;", connection))
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
                                using (OdbcCommand command = new OdbcCommand("delete from `ver` where serial='" + Serial.Text + "' and Time_stamp < '" + DateTime.Now.AddMinutes(-2).ToString("yyyy/M/d HH:mm:ss") + "';", connection))
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

                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("insert into communicationlog1(username,action,serial,Time_stamp) values('" + ((string)(Session["username"])) + "','Communication Verification','" + Serial.Text + "','" + String.Format("{0:yyyy/M/d HH:mm:ss}", DateTime.Now) + "');", connection))
                        command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                TimeStamp.Text = "Not Connected";
            }
        }

        protected void btnkWhReport_Click(object sender, EventArgs e)
        {
            DateTime DT;
            try
            {
                if (!(bool)Session["read_Available"])
                {
                    TimeStamp.Text = "Reading Unavailable.";
                    return;
                }
                Stamp_TextBox.Text = Selection_List.SelectedValue;

                DT = (DateTime)(Session["T"]);
                Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
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
                cell.BorderWidth = 1f; //Add a border around the cell
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                
                table.AddCell(cell);
                table.HorizontalAlignment = 1;
                table.AddCell("Time Stamp");
                table.AddCell((string)(Session["date"]));
                table.AddCell("Meter Serial Number");
                table.AddCell((string)(Session["Serial"]));

                

                table.AddCell("kWh Import");
                table.AddCell(((float)(Session["kWhVal_import"])).ToString());
                table.AddCell("kWh Export");
                table.AddCell(((float)(Session["kWhVal_export"])).ToString());
                table.AddCell("kVARh Import");
                table.AddCell(((float)(Session["kVARhVal_import"])).ToString());
                table.AddCell("kVARh Export");
                table.AddCell(((float)(Session["kVARhVal_export"])).ToString());



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

        protected void btnReconnect_Unload(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
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
                    using (OdbcCommand command = new OdbcCommand("SELECT `serial` FROM meter.meter;", connection))
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Serial.Text = DropDownList1.Text;
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session["BoolV"] = null;
            Response.Redirect("Pitc_Testing_Tool_Login.aspx");
        }
    }
}