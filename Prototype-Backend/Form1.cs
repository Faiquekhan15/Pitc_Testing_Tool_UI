using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Data.Odbc;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace MDC_Server
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private DateTime DT;
        private System.Timers.Timer ConnectionCheck;
        private System.Timers.Timer mode2Initiate;
        private System.Timers.Timer LoadProfileReadTime;
        private Dictionary<UInt32, NetworkStream> Stream_Object_Dict = new Dictionary<UInt32, NetworkStream>();

        private string myconn = "DRIVER={MySQL ODBC 3.51 Driver};Database=world;Server=localhost;Port=2142;UID=root;PWD=TRANSFOPOWER@123@321;";
        public Form1()
        {
            InitializeComponent();
            SetConStatTimer();
            setMode2Initiate();
            setLoadProfileReadingTimer();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPBox.Items.Add(ip.ToString());
                }
            }
        }
        private void setLoadProfileReadingTimer()
        {
            LoadProfileReadTime = new System.Timers.Timer(10000);
            LoadProfileReadTime.AutoReset = true;
            LoadProfileReadTime.Enabled = true;
        }
        private void setMode2Initiate()
        {
            mode2Initiate = new System.Timers.Timer(10000);
            mode2Initiate.Elapsed += mode2Initiate_Elapsed;
            mode2Initiate.AutoReset = true;
            mode2Initiate.Enabled = true;
        }
        private void SetConStatTimer()
        {
            ConnectionCheck = new System.Timers.Timer(600000);
            ConnectionCheck.Elapsed += ConnectionCheck_Elapsed;
            ConnectionCheck.AutoReset = true;
            ConnectionCheck.Enabled = true;
        }
        protected bool IsDigitsOnly(string str)
        {
            if (str.Length < 1)
            {
                return false;
            }
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        private void ExecuteNonQurey(string query)
        {
            DBGetSet db = new DBGetSet();
            db.Query = query;
            db.ExecuteNonQuery();
        }
        private DataTable ExecuteReader(string query)
        {
            DBGetSet db = new DBGetSet();
            db.Query = query;
            return db.ExecuteReader();
        }

        private void mode2Initiate_Elapsed(Object source, ElapsedEventArgs e)
        {
            try
            {
                byte[] aarq_request = { 0, 1, 0, 48, 0, 1, 0, 56, 96, 54, 161, 9, 6, 7, 96, 133, 116, 5, 8, 1, 1, 138, 2, 7, 128, 139, 7, 96, 133, 116, 5, 8, 2, 1, 172, 10, 128, 8, 49, 50, 51, 52, 53, 54, 55, 56, 190, 16, 4, 14, 1, 0, 0, 0, 6, 95, 31, 4, 0, 0, 126, 31, 4, 176 };
                List<UInt32> msnList = new List<UInt32>();
                int count = 0;
                List<UInt32> trylist = new List<UInt32>();
                using (OdbcConnection connection = new OdbcConnection(myconn))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT `serial`, try FROM world.meter WHERE (connected = 1 and mode = 1 and reading = 0) and (`read` = 1 or retry = 1);", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            msnList.Add(UInt32.Parse(dr["serial"].ToString()));
                            trylist.Add(UInt32.Parse(dr["try"].ToString()));
                            count++;
                        }
                        dr.Close();
                    }
                    connection.Close();
                }
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        if (Stream_Object_Dict.ContainsKey(msnList[i]) && Stream_Object_Dict[msnList[i]].CanWrite)
                        {
                            Stream_Object_Dict[msnList[i]].Write(aarq_request, 0, aarq_request.Length);

                        }
                        else
                        {
                            //ExecuteNonQurey("update world.meter set connected = 0 where `serial` = '" + msnList[i] + "';");
                        }
                    }
                    catch (Exception exp)
                    {

                        continue;
                    }

                    if (trylist[i] > 8)
                    {
                        ExecuteNonQurey("UPDATE world.meter SET `read` = 0, demand = 0, retry=0,  try = 0 where `serial` = " + msnList[i] + ";");
                    }
                    else
                    {
                        ExecuteNonQurey("UPDATE world.meter SET try = '" + (trylist[i] + 1) + "' where `serial` = " + msnList[i] + ";");
                    }
                }
            }
            catch (Exception ex)
            {
                var LineNumber = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();

            }
        }
        private void ConnectionCheck_Elapsed(Object source, ElapsedEventArgs e)
        {
            try
            {
                List<string> msns = new List<string>();
                using (OdbcConnection connection = new OdbcConnection(myconn))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT * FROM meter;", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string timecomp = dr["times"].ToString();
                            DateTime comp = DateTime.Parse(timecomp);
                            TimeSpan diff = TimeSpan.Parse("00:10:20.9896330");
                            if (DateTime.Now.Subtract(comp) > diff)
                            {
                                msns.Add((string)dr["serial"]);
                            }
                        }
                        dr.Close();
                    }
                    connection.Close();
                }

                foreach (string i in msns)
                {
                    ExecuteNonQurey("UPDATE meter SET connected = 0 WHERE msn = '" + i + "'");
                    ExecuteNonQurey("update meter set connected = 0 where lastRead < '" + DateTime.Now.AddDays(-1).ToString("yyyy/M/d HH:mm:ss") + "';");
                }
            }
            catch (Exception ex)
            {
                var LineNumber = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();

            }
        }
        private void StartAccept()
        {
            try
            {
                server.BeginAcceptTcpClient(HandleAsyncConnection, server);
            }
            catch (Exception ex)
            {

            }
        }
        private void HandleAsyncConnection(IAsyncResult res)
        {
            try
            {
                StartAccept(); //listen for new connections again
                TcpClient client = server.EndAcceptTcpClient(res);
                string remoteEndPoint = client.Client.RemoteEndPoint.ToString();
                //proceed
                this.BeginInvoke(new Action(() => {
                    try
                    {
                        labelClient.Text = remoteEndPoint + " @ " + DateTime.Now.ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                }));
                string clientItem = DateTime.Now + ": " + remoteEndPoint;
                this.BeginInvoke(new Action(() => {
                    listBox_ConnectedClients.Items.Add(clientItem);
                }));
                HandleClient(client);
                this.BeginInvoke(new Action(() => {
                    listBox_ConnectedClients.Items.Remove(clientItem);
                }));
            }
            catch (Exception ex)
            {

            }
        }
        private string convertr(byte[] ndata)
        {
            string str2 = "";
            for (int index = 0; index < ndata.Length; ++index)
            {
                str2 = str2 + ndata[index].ToString("X2") + " ";
            }
            return str2;
        }
        private void HandleClient(object client)
        {
            using (TcpClient tcpClient = client as TcpClient)
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    string remoteEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
                    bool isitMode2 = true;
                    int faliures = 0;
                    UInt32 MSN = new UInt32();
                    do
                    {
                        bool aarqStatus = false;
                        byte[] data = new byte[1024];
                        bool Valid_MSN = false;
                        DataTable dt = new DataTable();

                        try
                        {
                            MSN = PushEventsCheck(stream, remoteEndPoint, isitMode2, ref aarqStatus);
                            int count = 0;
                            byte[] readSerial = "00 01 00 30 00 01 00 0D C0 01 81 00 01 00 00 60 01 00 FF 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();

                            if (MSN == 0)
                            {
                                if (stream.DataAvailable)
                                {
                                    count = stream.Read(data, 0, data.Length);
                                }
                                aarqStatus = EstablishAARQ(aarqStatus, stream);
                                stream.Write(readSerial, 0, readSerial.Length);
                                count = stream.Read(data, 0, data.Length);

                                if (data[8] == 14)
                                {
                                    stream.Close();
                                    tcpClient.Close();
                                    return;
                                }
                                MSN = UInt32.Parse((data[count - 10] - 48).ToString() + (data[count - 9] - 48).ToString() + (data[count - 8] - 48).ToString() + (data[count - 7] - 48).ToString() + (data[count - 6] - 48).ToString() + (data[count - 5] - 48).ToString() + (data[count - 4] - 48).ToString() + (data[count - 3] - 48).ToString() + (data[count - 2] - 48).ToString() + (data[count - 1] - 48).ToString());
                                Valid_MSN = true;
                                this.BeginInvoke(new Action(() =>
                                {

                                    labelClient.Text = MSN.ToString();
                                }));
                            }
                            this.BeginInvoke(new Action(() =>
                            {
                                labelClient.Text = tcpClient.Client.RemoteEndPoint.ToString() + " @ " + DateTime.Now.ToString();
                            }));


                            if (MSN > 1 && MSN < 4200000000)
                            {
                                Valid_MSN = true;
                            }
                            this.BeginInvoke(new Action(() =>
                            {
                                labelClient.Text = MSN.ToString();
                            }));
                            if (MSN < 1)
                            {
                                stream.Close();
                                tcpClient.Close();
                                return;
                            }
                            aarqStatus = EstablishAARQ(aarqStatus, stream);

                            ExecuteNonQurey("UPDATE meter SET connected ='1', times ='" + DateTime.Now.ToString() + "' WHERE `serial` ='" + MSN + "';");



                            int comm = 0, isSupposedToRead = 0;
                            string checker = "";

                            if (Valid_MSN)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    labelClient.Show();
                                    labelClient.Text = MSN.ToString();
                                }));
                                ExecuteNonQurey("UPDATE meter SET times ='" + DateTime.Now.ToString() + "', reading ='" + isSupposedToRead + "', retry ='" + isSupposedToRead + "', try ='" + isSupposedToRead + "', job ='" + '1' + "' WHERE `serial` ='" + MSN + "';");
                                isSupposedToRead = 1;
                                if (isSupposedToRead == 1)
                                {
                                    dt = ExecuteReader("SELECT retry FROM world.meter where serial = '" + MSN + "';");
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        comm = (int)(dr["retry"]);
                                    }
                                }
                                comm = 1;
                                if (!Stream_Object_Dict.ContainsKey(MSN))
                                {
                                    Stream_Object_Dict.Add(MSN, stream);
                                }
                                else
                                {
                                    Stream_Object_Dict[MSN] = stream;
                                }
                                /**********************************************************************/
                                /**************************Comm Verification Portion***************************/
                                /**********************************************************************/
                                /****************************Bidirectional Check*******************/
                                if (comm == 1)
                                {
                                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                                    string logFilePath = Path.Combine(desktopPath, "Prototype_Logs.txt");

                                    // Check if log file exists, create it if it doesn't
                                    if (!File.Exists(logFilePath))
                                    {
                                        File.Create(logFilePath).Dispose();
                                    }

                                    string dutttame = String.Format("{0:yyyy/M/d HH:mm:ss}", DateTime.Now);
                                    byte[] Net_Mode_Reply = "00 01 00 30 00 01 00 0D C0 01 81 00 01 00 00 5E 42 1E FF 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();
                                    byte[] Normal_Mode_Reply = "00 01 00 30 00 01 00 0D C0 01 81 00 01 00 00 5E 42 1E FF 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();

                                    bool isNetMode = false;
                                    bool isNormalMode = false;

                                    // Send the net mode reply to check if the mode is net mode
                                    stream.Write(Net_Mode_Reply, 0, Net_Mode_Reply.Length);
                                    count = stream.Read(data, 0, data.Length);
                                    if (data[13] == 2)
                                    {
                                        isNetMode = true;
                                        string q=$"Update meter SET mode = '{data[13]}' where serial = '{MSN}';";
                                        ExecuteNonQurey(q);
                                    }
                                    else
                                    {
                                        // Send the normal mode reply to check if the mode is normal mode
                                        stream.Write(Normal_Mode_Reply, 0, Normal_Mode_Reply.Length);
                                        count = stream.Read(data, 0, data.Length);
                                        if (data[13] == 1)
                                        {
                                            isNormalMode = true;
                                            string q = $"Update meter SET mode = '{data[13]}' where serial = '{MSN}';";
                                            ExecuteNonQurey(q);
                                        }
                                    }

                                    if (isNormalMode)
                                    {
                                        // Send the active energy request
                                        byte[] Active_Energy = { 0, 1, 0, 48, 0, 1, 0, 13, 192, 1, 129, 0, 3, 1, 0, 15, 8, 0, 255, 2, 0 };
                                        stream.Write(Active_Energy, 0, Active_Energy.Length);
                                        count = stream.Read(data, 0, data.Length);
                                        if (data[8] == 14)
                                        {
                                            throw new Exception("Data Error in Scaler Read.");
                                        }
                                        float activeEnergy = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0));
                                        // Send the reactive energy request
                                        byte[] Reactive_Energy = { 0, 1, 0, 48, 0, 1, 0, 13, 192, 1, 129, 0, 3, 1, 0, 128, 8, 0, 255, 2, 0 };
                                        stream.Write(Reactive_Energy, 0, Reactive_Energy.Length);
                                        count = stream.Read(data, 0, data.Length);
                                        float reactiveEnergy = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0)) ;
                                        ExecuteNonQurey("INSERT INTO `comm-verification` (serial,active,reactive,Time_stamp) VALUES (" + MSN + ",'" + activeEnergy / 1000 + "','" + reactiveEnergy / 1000 + "','" + dutttame + "');");
                                        ExecuteNonQurey("UPDATE meter SET lastRead = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE serial = '" + MSN + "'");
                                        using (StreamWriter sw = File.AppendText(logFilePath))
                                        {
                                            sw.WriteLine("\n" + "Log Date and Time" + "\t" + dutttame + "\n");
                                            sw.WriteLine("Meter Mode : Normal Mode" + "\n");
                                            sw.WriteLine("Meter Serial Number : " + MSN + "\t" + "Total Active Energy = " + activeEnergy + "\tTotal Reactive Energy = " + reactiveEnergy + "\n");
                                        }
                                        ExecuteNonQurey("UPDATE meter SET reading = '0', retry = '0', `try` = '0', job = 0 WHERE `serial` = '" + MSN + "'");
                                    }
                                    else if (isNetMode == true)
                                    {
                                        byte[] Total_Active_Energy_import = "00 01 00 30 00 01 00 0D C0 01 81 00 03 01 00 01 08 00 ff 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();
                                        byte[] Total_Active_Energy_export = "00 01 00 30 00 01 00 0D C0 01 81 00 03 01 00 02 08 00 ff 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();
                                        byte[] Total_Reactive_Energy_import = "00 01 00 30 00 01 00 0D C0 01 81 00 03 01 00 03 08 00 ff 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();
                                        byte[] Total_Reactive_Energy_export = "00 01 00 30 00 01 00 0D C0 01 81 00 03 01 00 04 08 00 ff 02 00".Split().Select(s => Convert.ToByte(s, 16)).ToArray();

                                        stream.Write(Total_Active_Energy_import, 0, Total_Active_Energy_import.Length);
                                        count = stream.Read(data, 0, data.Length);

                                        float totalActiveEnergyImport = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0));

                                        stream.Write(Total_Active_Energy_export, 0, Total_Active_Energy_export.Length);
                                        count = stream.Read(data, 0, data.Length);

                                        float totalActiveEnergyExport = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0));

                                        stream.Write(Total_Reactive_Energy_import, 0, Total_Reactive_Energy_import.Length);
                                        count = stream.Read(data, 0, data.Length);

                                        float totalReactiveEnergyImport = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0));

                                        stream.Write(Total_Reactive_Energy_export, 0, Total_Reactive_Energy_export.Length);
                                        count = stream.Read(data, 0, data.Length);

                                        float totalReactiveEnergyExport = (UInt32)((data[count - 4] << 24) | (data[count - 3] << 16) | (data[count - 2] << 8) | (data[count - 1] << 0));


                                        ExecuteNonQurey("INSERT INTO `world`.`ver` (serial, active_import, active_export, reactive_import, reactive_export, Time_stamp) VALUES ('" + MSN + "', '" + totalActiveEnergyImport / 1000 + "', '" + totalActiveEnergyExport / 1000 + "', '" + totalReactiveEnergyImport / 1000 + "', '" + totalReactiveEnergyExport / 1000 + "', '" + dutttame + "');");

                                        ExecuteNonQurey("UPDATE meter SET lastRead = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE serial = '" + MSN + "'");

                                        using (StreamWriter sw = File.AppendText(logFilePath))
                                        {
                                            sw.WriteLine("\n" + "Log Date and Time" + "\t" + dutttame + "\n");
                                            sw.WriteLine("Meter Mode : Birdirection Mode" + "\n");
                                            sw.WriteLine("Meter Serial Number :" + MSN + "\t" + "Total Active Energy Import = " + totalActiveEnergyImport + "\tTotal Active Energy Export = " + totalActiveEnergyExport + "\tTotal Reactive Energy Import = " + totalReactiveEnergyImport + "\tTotal Reactive Energy Export = " + totalReactiveEnergyExport + "\n");
                                        }


                                        ExecuteNonQurey("UPDATE meter SET reading = '0', retry = '0', `try` = '0', job = 0 WHERE `serial` = '" + MSN + "'");


                                    }
                                }
                                if (aarqStatus)
                                {
                                    byte[] AARE = { 0, 1, 0, 48, 0, 1, 0, 5, 98, 3, 128, 1, 0 };
                                    stream.Write(AARE, 0, AARE.Length);
                                    count = stream.Read(data, 0, data.Length);

                                    aarqStatus = false;

                                    if (!isitMode2)
                                    {
                                        PushEventsCheck(stream, remoteEndPoint, isitMode2, ref aarqStatus);
                                    }
                                }
                                this.BeginInvoke(new Action(() => {

                                    label5.Text = MSN + ": Handshake Released at:" + DateTime.Now.TimeOfDay.ToString();
                                }));

                                if (isitMode2)
                                {
                                    stream.ReadTimeout = 1800000;
                                }
                                else
                                {
                                    continue;
                                }
                                faliures = 0;
                            }
                        }

                        catch (Exception ex)
                        {

                            faliures++;
                            if (faliures > 8)
                            {
                                stream.Close();
                                tcpClient.Close();
                                isitMode2 = false;
                            }
                        }
                    }
                    while (isitMode2);

                }

            }
        }
        private dynamic PushEventsCheck(NetworkStream stream, string remoteEndPoint, bool mode2, ref bool aarq)
        {
            uint MSN = 0;
            byte[] data = new byte[1024];
            byte[] eventAck = { 0, 1, 0, 16, 0, 1, 0, 1, 219 };
            int count = 0;
            bool isEvent = false;

            try
            {
                if (mode2)
                {
                    stream.ReadTimeout = 1800000;
                }
                else
                {
                    stream.ReadTimeout = 5000;
                }

                do
                {
                    isEvent = false;
                    count = stream.Read(data, 0, data.Length);
                    string str2 = "";
                    for (int index = 0; index < count; ++index)
                    {
                        str2 = str2 + data[index].ToString("X2") + " ";
                    }

                    if (data[count - 1] == 0x07 && data[count - 2] == 0x00 && data[count - 3] == 0x2C)
                    {
                        aarq = true;
                    }

                    if (data[1] == 4)
                    {
                        MSN = (UInt32)((data[count - 1] << 24) | (data[count - 2] << 16) | (data[count - 3] << 8) | (data[count - 4] << 0));
                        byte[] keepAliveAck = { 218 };
                        string my_str = this.convertr(keepAliveAck);
                        stream.Write(keepAliveAck, 0, keepAliveAck.Length);

                    }

                    if (data[count - 1] == 0 && data[count - 2] == 9)
                    {
                        isEvent = true;
                        stream.Write(eventAck, 0, eventAck.Length);
                        string my_str = this.convertr(eventAck);

                        UInt16 eventCode = (UInt16)((data[count - 4] << 8) | (data[count - 3] << 0));
                        string Serial = (data[count - 28] - 48).ToString() + (data[count - 27] - 48).ToString() + (data[count - 26] - 48).ToString() + (data[count - 25] - 48).ToString() + (data[count - 24] - 48).ToString() + (data[count - 23] - 48).ToString() + (data[count - 22] - 48).ToString() + (data[count - 21] - 48).ToString() + (data[count - 20] - 48).ToString() + (data[count - 19] - 48).ToString();
                        string occurTime = data[count - 12] + ":" + data[count - 11] + ":" + data[count - 10];
                        UInt16 dateoccur = (UInt16)((data[count - 17] << 8) | (data[count - 16] << 0));
                        string occurance = dateoccur + "/" + data[count - 15] + "/" + data[count - 14] + " " + occurTime;
                        MSN = UInt32.Parse(Serial);


                    }
                }
                while (isEvent);
            }
            catch (Exception) { }

            return MSN;
        }
        private bool EstablishAARQ(bool aarq, NetworkStream stream)
        {
            if (!aarq)
            {
                int count = 0;
                byte[] data = new byte[1000];
                byte[] aarq_request = { 0, 1, 0, 48, 0, 1, 0, 56, 96, 54, 161, 9, 6, 7, 96, 133, 116, 5, 8, 1, 1, 138, 2, 7, 128, 139, 7, 96, 133, 116, 5, 8, 2, 1, 172, 10, 128, 8, 49, 50, 51, 52, 53, 54, 55, 56, 190, 16, 4, 14, 1, 0, 0, 0, 6, 95, 31, 4, 0, 0, 126, 31, 4, 176 };
                stream.Write(aarq_request, 0, aarq_request.Length);
                count = stream.Read(data, 0, data.Length);

                if (!(data[count - 1] == 0x07 && data[count - 2] == 0x00 && data[count - 3] == 0x2C))
                {
                    //stream.Write(cosem_release, 0, cosem_release.Length);
                    //count = stream.Read(data, 0, data.Length);

                    stream.Write(aarq_request, 0, aarq_request.Length);
                    count = stream.Read(data, 0, data.Length);

                    if (!(data[count - 1] == 0x07 && data[count - 2] == 0x00 && data[count - 3] == 0x2C))
                    {
                        throw new Exception("Association Faliure");
                    }
                }
            }
            return true;
        }
        private void start_Click(object sender, EventArgs e)
        {
            try
            {
                //IPAddress poi = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Last(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                if (IPBox.Text.Contains('.') && serverPort.Text.Length > 0)
                {
                    server = new TcpListener(IPAddress.Parse(IPBox.Text), int.Parse(serverPort.Text));
                    server.Start();
                    labelServer.Show();
                    labelServer.Text = "Server Started : " + server.Server.LocalEndPoint.ToString() + " @ " + DateTime.Now.ToString();
                    IPBox.Enabled = false;
                    serverPort.Enabled = false;
                    //test();
                }
                else
                {
                    MessageBox.Show("Please select a Valid IP and Port", "T-Collector", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                var LineNumber = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                MessageBox.Show("Line #: " + LineNumber + " - " + "Line #: " + LineNumber + " - " + ex.Message);
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                server.Stop();
                labelServer.Hide();
            }
            catch (Exception ex)
            {
                var LineNumber = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                MessageBox.Show("Line #: " + LineNumber + " - " + ex.Message);
            }
        }
        private void btnClientService_Click(object sender, EventArgs e)
        {
            StartAccept();
            label1.Text = "Listining for Clients...";
            label1.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => {
                labelServer.Hide();

            }));
            this.BeginInvoke(new Action(() =>
            {

                labelClient.Hide();
            }));
            this.BeginInvoke(new Action(() =>
            {

                label1.Hide();
            }));

        }
        private void Clear_Logs_Click(object sender, EventArgs e)
        {
            listBox_ConnectedClients.Items.Clear();
            labelServer.Hide();
            label1.Hide();
            labelClient.Hide();
        }
    }
}

