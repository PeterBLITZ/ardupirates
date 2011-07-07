using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Net;

namespace ArdupilotMega.GCSViews
{
    class FirmwareMP : UserControl
    {
        private System.Windows.Forms.PictureBox pictureBoxQuad;
        private System.Windows.Forms.PictureBox pictureBoxHexa;
        private System.Windows.Forms.PictureBox pictureBoxTri;
        private System.Windows.Forms.PictureBox pictureBoxY6;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private MyButton ACSetup;
        public Boolean Is_FFIMU;
        private OpenFileDialog openFileDialog;
        private PictureBox pictureBoxOcto;
        private Label label7;
        private Label label1;
        private Label label5;
        private Label label4;
        private Label label3;
        private MyButton btn_manuel;
        private String Is_IMU = "";
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirmwareMP));
            this.pictureBoxQuad = new System.Windows.Forms.PictureBox();
            this.pictureBoxHexa = new System.Windows.Forms.PictureBox();
            this.pictureBoxTri = new System.Windows.Forms.PictureBox();
            this.pictureBoxY6 = new System.Windows.Forms.PictureBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxOcto = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ACSetup = new ArdupilotMega.MyButton();
            this.btn_manuel = new ArdupilotMega.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHexa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxY6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOcto)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxQuad
            // 
            this.pictureBoxQuad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxQuad.Image = global::ArdupilotMega.Properties.Resources.MegaPirates_01;
            resources.ApplyResources(this.pictureBoxQuad, "pictureBoxQuad");
            this.pictureBoxQuad.Name = "pictureBoxQuad";
            this.pictureBoxQuad.TabStop = false;
            this.pictureBoxQuad.Click += new System.EventHandler(this.pictureBoxQuad_Click);
            // 
            // pictureBoxHexa
            // 
            this.pictureBoxHexa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxHexa.Image = global::ArdupilotMega.Properties.Resources.MegaPirates_05;
            resources.ApplyResources(this.pictureBoxHexa, "pictureBoxHexa");
            this.pictureBoxHexa.Name = "pictureBoxHexa";
            this.pictureBoxHexa.TabStop = false;
            this.pictureBoxHexa.Click += new System.EventHandler(this.pictureBoxHexa_Click);
            // 
            // pictureBoxTri
            // 
            this.pictureBoxTri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTri.Image = global::ArdupilotMega.Properties.Resources.MegaPirates_03;
            resources.ApplyResources(this.pictureBoxTri, "pictureBoxTri");
            this.pictureBoxTri.Name = "pictureBoxTri";
            this.pictureBoxTri.TabStop = false;
            this.pictureBoxTri.Click += new System.EventHandler(this.pictureBoxTri_Click);
            // 
            // pictureBoxY6
            // 
            this.pictureBoxY6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxY6.Image = global::ArdupilotMega.Properties.Resources.MegaPirates_09;
            resources.ApplyResources(this.pictureBoxY6, "pictureBoxY6");
            this.pictureBoxY6.Name = "pictureBoxY6";
            this.pictureBoxY6.TabStop = false;
            this.pictureBoxY6.Click += new System.EventHandler(this.pictureBoxY6_Click);
            // 
            // lbl_status
            // 
            resources.ApplyResources(this.lbl_status, "lbl_status");
            this.lbl_status.Name = "lbl_status";
            // 
            // progress
            // 
            resources.ApplyResources(this.progress, "progress");
            this.progress.Name = "progress";
            this.progress.Step = 1;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // pictureBoxOcto
            // 
            this.pictureBoxOcto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxOcto.Image = global::ArdupilotMega.Properties.Resources.MegaPirates_06;
            resources.ApplyResources(this.pictureBoxOcto, "pictureBoxOcto");
            this.pictureBoxOcto.Name = "pictureBoxOcto";
            this.pictureBoxOcto.TabStop = false;
            this.pictureBoxOcto.Click += new System.EventHandler(this.pictureBoxOCTA_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ACSetup
            // 
            resources.ApplyResources(this.ACSetup, "ACSetup");
            this.ACSetup.Name = "ACSetup";
            this.ACSetup.UseVisualStyleBackColor = true;
            this.ACSetup.Click += new System.EventHandler(this.ACSetup_Click);
            // 
            // btn_manuel
            // 
            resources.ApplyResources(this.btn_manuel, "btn_manuel");
            this.btn_manuel.Name = "btn_manuel";
            this.btn_manuel.UseVisualStyleBackColor = true;
            this.btn_manuel.Click += new System.EventHandler(this.btn_manuel_Click);
            // 
            // FirmwareMP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_manuel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxOcto);
            this.Controls.Add(this.ACSetup);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.pictureBoxY6);
            this.Controls.Add(this.pictureBoxTri);
            this.Controls.Add(this.pictureBoxHexa);
            this.Controls.Add(this.pictureBoxQuad);
            this.MinimumSize = new System.Drawing.Size(1008, 461);
            this.Name = "FirmwareMP";
            this.Load += new System.EventHandler(this.FirmwareVisual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHexa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxY6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOcto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        List<software> softwares = new List<software>();
        bool flashing = false;

        public struct software
        {
            public string url;
            public string url2560;
            public string name;
            public string desc;
        }

        public FirmwareMP()
        {
            InitializeComponent();
            WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        private void FirmwareVisual_Load(object sender, EventArgs e)
        {
            string url = "";
            string url2560 = "";
            string name = "";
            string desc = "";
            if (Is_FFIMU)
                Is_IMU = "f";
            else
                Is_IMU = "a";
            software temp = new software();
            return;

            try
            {

                //using (XmlTextReader xmlreader = new XmlTextReader("http://ardupilot-mega.googlecode.com/svn/Tools/trunk/ArdupilotMegaPlanner/Firmware/firmware2.xml"))
                using (XmlTextReader xmlreader = new XmlTextReader("http://ardupirates.googlecode.com/svn/branches/Syberian/firmware/"))
                {
                    while (xmlreader.Read())
                    {
                        xmlreader.MoveToElement();
                        switch (xmlreader.Name)
                        {
                            case "url":
                                url = xmlreader.ReadString();
                                break;
                            case "url2560":
                                url2560 = xmlreader.ReadString();
                                break;
                            case "name":
                                name = xmlreader.ReadString();
                                break;
                            case "desc":
                                desc = xmlreader.ReadString();
                                break;
                            case "Firmware":
                                if (!url.Equals("") && !name.Equals("") && !desc.Equals("Please Update"))
                                {
                                    temp.desc = desc;
                                    temp.name = name;
                                    temp.url = url;
                                    temp.url2560 = url2560;

                                    softwares.Add(temp);
                                }
                                url = "";
                                url2560 = "";
                                name = "";
                                desc = "";
                                temp = new software();
                                break;
                            default:
                                break;
                        }
                    }
                }

                List<string> list = new List<string>();

            }
            catch (Exception ex) { MessageBox.Show("Failed to get Firmware List : " + ex.Message); }
        }

        void findfirmware(string findwhat)
        {
            foreach (software temp in softwares)
            {
                if (temp.url.ToLower().Contains(findwhat.ToLower()))
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to upload " + temp.name + "?", "Continue", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        update(temp);
                    }
                    return;
                }
            }

            MessageBox.Show("The requested firmware was not found.");
        }

        bool downloadFirmware(string filename)
        {
            WebResponse response= null;
            Stream dataStream = null; //= request.GetRequestStream();
            WebRequest request = null;
            try
            {
                String baseurl = "http://ardupirates.googlecode.com/svn/branches/Syberian/firmware/" + filename;
                // Create a request using a URL that can receive a post. 
                request = WebRequest.Create(baseurl);
                request.Timeout = 10000;
                // Set the Method property of the request to POST.
                request.Method = "GET";
                // Get the request stream.
                // Get the response.
                response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            long bytes = response.ContentLength;
            long contlen = bytes;

            byte[] buf1 = new byte[1024];

            FileStream fs = new FileStream(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + filename, FileMode.Create);

            lbl_status.Text = "Downloading from Internet";

            this.Refresh();
            Application.DoEvents();
            while (dataStream.CanRead && bytes > 0)
            {
                try
                {
                    progress.Value = (int)(((float)(response.ContentLength - bytes) / (float)response.ContentLength) * 100);
                    this.progress.Refresh();
                }
                catch { }
                int len = dataStream.Read(buf1, 0, 1024);
                bytes -= len;
                fs.Write(buf1, 0, len);
            }

            fs.Close();
            dataStream.Close();
            response.Close();

            progress.Value = 100;
            this.Refresh();
            Application.DoEvents();
            Console.WriteLine("Downloaded");
            return true;
        }

        private void pictureBoxAPM_Click(object sender, EventArgs e)
        {
            //findfirmware("mp_plane_" + Is_IMU);
            downloadFirmware("mp_plane_" + Is_IMU + ".hex");
        }

        private void pictureBoxAPMHIL_Click(object sender, EventArgs e)
        {
            //findfirmware("APM2HIL-");
            MessageBox.Show("Doesn't work");
        }

        private void pictureBoxQuad_Click(object sender, EventArgs e)
        {
            string firmwarename = "mp_quad_" + Is_IMU + ".hex";
            if ( downloadFirmware(firmwarename) )
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void pictureBoxHexa_Click(object sender, EventArgs e)
        {
            //findfirmware("mp-hexa_" + Is_IMU);
            string firmwarename = "mp_hexa_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);

        }

        private void pictureBoxTri_Click(object sender, EventArgs e)
        {
            //findfirmware("mp-tri_" + Is_IMU);
            string firmwarename = "mp_tri_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void pictureBoxY6_Click(object sender, EventArgs e)
        {
            //findfirmware("mp-y6_" + Is_IMU);
            string firmwarename = "mp_y6_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void pictureBoxOCTA_Click(object sender, EventArgs e)
        {
            //findfirmware("mp_octa_" + Is_IMU);
            string firmwarename = "mp_octa_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void pictureBoxQuadx_Click(object sender, EventArgs e)
        {
            string firmwarename = "mp_quadx_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void pictureBoxHexax_Click(object sender, EventArgs e)
        {
            string firmwarename = "mp_hexax_" + Is_IMU + ".hex";
            if (downloadFirmware(firmwarename))
                update(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + firmwarename);
        }

        private void update(string filename)
        {
            string board = "";
            MainV2.comPort.Close();
            System.Threading.Thread.Sleep(100);
            MainV2.givecomport = true;

            try
            {
                //if (softwares.Count == 0)
                //{
                //    MessageBox.Show("No valid options");
                //    return;
                //}

                lbl_status.Text = "Detecting PirateMega Version";

                this.Refresh();

                Comms port1 = new ArduinoSTKv2();

                //ArduinoSTKv2 port1 = new ArduinoSTKv2();
                port1.BaudRate = 115200;
                port1.DataBits = 8;
                port1.StopBits = StopBits.One;
                port1.Parity = Parity.None;
                port1.DtrEnable = false;

                try
                {
                    port1.PortName = MainV2.comportname;

                    port1.Open();

                    port1.DtrEnable = true;
                }
                catch
                {
                    if (port1.IsOpen)
                        port1.Close();
                    return;
                }

                if (port1.connectAP())
                {
                    board = "2560";
                    port1.Close();
                }
                else
                {
                    Console.WriteLine("Not a 2560");
                    port1.Close();
                }

                if (board != "2560")
                {
                    port1 = new ArduinoSTK();
                    port1.BaudRate = 57600;
                    port1.DataBits = 8;
                    port1.StopBits = StopBits.One;
                    port1.Parity = Parity.None;
                    port1.DtrEnable = true;

                    try
                    {
                        port1.PortName = MainV2.comportname;

                        port1.Open();
                    }
                    catch
                    {
                        if (port1.IsOpen)
                            port1.Close();
                        return;
                    }

                    if (port1.connectAP())
                    {
                        board = "1280";
                        port1.Close();
                    }
                    else
                    {
                        port1.Close();
                        Console.WriteLine("Not a 1280");
                        MessageBox.Show("Cant detect your MegaPirate version. Please check your cabling");
                        return;
                    }
                }

                Console.WriteLine("Detected a " + board);

                /*
                string baseurl = "";
                if (board == "2560")
                {
                    baseurl = temp.url2560.ToString();
                }
                else
                {
                    baseurl = temp.url.ToString();
                }

                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(baseurl);
                request.Timeout = 10000;
                // Set the Method property of the request to POST.
                request.Method = "GET";
                // Get the request stream.
                Stream dataStream; //= request.GetRequestStream();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                long bytes = response.ContentLength;
                long contlen = bytes;

                byte[] buf1 = new byte[1024];

                FileStream fs = new FileStream(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"firmware.hex", FileMode.Create);

                lbl_status.Text = "Downloading from Internet";

                this.Refresh();

                while (dataStream.CanRead && bytes > 0)
                {
                    try
                    {
                        progress.Value = (int)(((float)(response.ContentLength - bytes) / (float)response.ContentLength) * 100);
                        this.progress.Refresh();
                    }
                    catch { }
                    int len = dataStream.Read(buf1, 0, 1024);
                    bytes -= len;
                    fs.Write(buf1, 0, len);
                }

                fs.Close();
                dataStream.Close();
                response.Close();

                progress.Value = 100;
                this.Refresh();
                Console.WriteLine("Downloaded");
                */
            }
            catch (Exception ex) { lbl_status.Text = "Failed download"; MessageBox.Show("Failed to download new firmware : " + ex.Message); return; }

            byte[] FLASH = new byte[1];
            StreamReader sr = null;
            try
            {
                lbl_status.Text = "Reading Hex File";
                this.Refresh();
                sr = new StreamReader(_selected_Firmware);
                FLASH = readIntelHEXv2(sr);
                sr.Close();

            }
            catch (Exception ex) { if (sr != null) { sr.Dispose(); } lbl_status.Text = "Failed read HEX"; MessageBox.Show("Failed to read firmware.hex : " + ex.Message); return; }
            Comms port = new ArduinoSTK();

            if (board == "1280")
            {
                //port = new ArduinoSTK();
                port.BaudRate = 57600;
            }
            else if (board == "2560")
            {
                port = new ArduinoSTKv2();
                port.BaudRate = 115200;
            }
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DtrEnable = true;

            try
            {
                port.PortName = MainV2.comportname;

                port.Open();

                flashing = true;

                if (port.connectAP())
                {
                    Console.WriteLine("starting");
                    lbl_status.Text = "Uploading to MegaPirate";
                    progress.Value = 0;
                    this.Refresh();

                    // this is enough to make ap_var reset
                    //port.upload(new byte[256], 0, 2, 0);

                    port.Progress += new ProgressEventHandler(port_Progress);

                    if (!port.uploadflash(FLASH, 0, FLASH.Length, 0))
                    {
                        flashing = false;
                        if (port.IsOpen)
                            port.Close();
                        throw new Exception("Upload failed. Lost sync. Try Arduino!!");
                    }

                    port.Progress -= new ProgressEventHandler(port_Progress);

                    progress.Value = 100;

                    Console.WriteLine("Uploaded");

                    this.Refresh();

                    int start = 0;
                    short length = 0x100;

                    byte[] flashverify = new byte[FLASH.Length + 256];

                    lbl_status.Text = "Verify MegaPirate";
                    progress.Value = 0;
                    this.Refresh();

                    while (start < FLASH.Length)
                    {
                        progress.Value = (int)((start / (float)FLASH.Length) * 100);
                        this.Refresh();
                        Console.WriteLine("Downloading " + length + " at " + start);
                        port.setaddress(start);
                        port.downloadflash(length).CopyTo(flashverify, start);
                        start += length;
                    }

                    progress.Value = 100;

                    for (int s = 0; s < FLASH.Length; s++)
                    {
                        if (FLASH[s] != flashverify[s])
                            MessageBox.Show("Upload succeeded, but verify failed");
                    }

                    lbl_status.Text = "Done";
                }
                else
                {
                    lbl_status.Text = "Failed upload";
                    MessageBox.Show("Communication Error - no connection");
                }
                port.Close();

                flashing = false;

            }
            catch (Exception ex) { lbl_status.Text = "Failed upload"; MessageBox.Show("Check port settings or Port in use? " + ex.ToString()); port.Close(); }
            flashing = false;
            MainV2.givecomport = false;
        }

        private void update(software temp)
        {
            string board = "";
            MainV2.comPort.Close();
            System.Threading.Thread.Sleep(100);
            MainV2.givecomport = true;

            try
            {
                if (softwares.Count == 0)
                {
                    MessageBox.Show("No valid options");
                    return;
                }

                lbl_status.Text = "Detecting APM Version";

                this.Refresh();

                Comms port1 = new ArduinoSTKv2();

                //ArduinoSTKv2 port1 = new ArduinoSTKv2();
                port1.BaudRate = 115200;
                port1.DataBits = 8;
                port1.StopBits = StopBits.One;
                port1.Parity = Parity.None;
                port1.DtrEnable = false;

                try
                {
                    port1.PortName = MainV2.comportname;

                    port1.Open();

                    port1.DtrEnable = true;
                }
                catch
                {
                    if (port1.IsOpen)
                        port1.Close();
                    return;
                }

                if (port1.connectAP())
                {
                    board = "2560";
                    port1.Close();
                }
                else
                {
                    Console.WriteLine("Not a 2560");
                    port1.Close();
                }

                if (board != "2560")
                {
                    port1 = new ArduinoSTK();
                    port1.BaudRate = 57600;
                    port1.DataBits = 8;
                    port1.StopBits = StopBits.One;
                    port1.Parity = Parity.None;
                    port1.DtrEnable = true;

                    try
                    {
                        port1.PortName = MainV2.comportname;

                        port1.Open();
                    }
                    catch
                    {
                        if (port1.IsOpen)
                            port1.Close();
                        return;
                    }

                    if (port1.connectAP())
                    {
                        board = "1280";
                        port1.Close();
                    }
                    else
                    {
                        port1.Close();
                        Console.WriteLine("Not a 1280");
                        MessageBox.Show("Cant detect your APM version. Please check your cabling");
                        return;
                    }
                }

                Console.WriteLine("Detected a " + board);

                string baseurl = "";
                if (board == "2560")
                {
                    baseurl = temp.url2560.ToString();
                }
                else
                {
                    baseurl = temp.url.ToString();
                }

                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(baseurl);
                request.Timeout = 10000;
                // Set the Method property of the request to POST.
                request.Method = "GET";
                // Get the request stream.
                Stream dataStream; //= request.GetRequestStream();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                long bytes = response.ContentLength;
                long contlen = bytes;

                byte[] buf1 = new byte[1024];

                FileStream fs = new FileStream(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"firmware.hex", FileMode.Create);

                lbl_status.Text = "Downloading from Internet";

                this.Refresh();

                while (dataStream.CanRead && bytes > 0)
                {
                    try
                    {
                        progress.Value = (int)(((float)(response.ContentLength - bytes) / (float)response.ContentLength) * 100);
                        this.progress.Refresh();
                    }
                    catch { }
                    int len = dataStream.Read(buf1, 0, 1024);
                    bytes -= len;
                    fs.Write(buf1, 0, len);
                }

                fs.Close();
                dataStream.Close();
                response.Close();

                progress.Value = 100;
                this.Refresh();
                Console.WriteLine("Downloaded");
            }
            catch (Exception ex) { lbl_status.Text = "Failed download"; MessageBox.Show("Failed to download new firmware : " + ex.Message); return; }

            byte[] FLASH = new byte[1];
            StreamReader sr = null;
            try
            {
                lbl_status.Text = "Reading Hex File";
                this.Refresh();
                sr = new StreamReader(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"firmware.hex");
                FLASH = readIntelHEXv2(sr);
                sr.Close();

            }
            catch (Exception ex) { if (sr != null) { sr.Dispose(); } lbl_status.Text = "Failed read HEX"; MessageBox.Show("Failed to read firmware.hex : " + ex.Message); return; }
            Comms port = new ArduinoSTK();

            if (board == "1280")
            {
                //port = new ArduinoSTK();
                port.BaudRate = 57600;
            }
            else if (board == "2560")
            {
                port = new ArduinoSTKv2();
                port.BaudRate = 115200;
            }
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DtrEnable = true;

            try
            {
                port.PortName = MainV2.comportname;

                port.Open();

                flashing = true;

                if (port.connectAP())
                {
                    Console.WriteLine("starting");
                    lbl_status.Text = "Uploading to APM";
                    progress.Value = 0;
                    this.Refresh();

                    // this is enough to make ap_var reset
                    //port.upload(new byte[256], 0, 2, 0);

                    port.Progress += new ProgressEventHandler(port_Progress);

                    if (!port.uploadflash(FLASH, 0, FLASH.Length, 0))
                    {
                        flashing = false;
                        if (port.IsOpen)
                            port.Close();
                        throw new Exception("Upload failed. Lost sync. Try Arduino!!");
                    }

                    port.Progress -= new ProgressEventHandler(port_Progress);

                    progress.Value = 100;

                    Console.WriteLine("Uploaded");

                    this.Refresh();

                    int start = 0;
                    short length = 0x100;

                    byte[] flashverify = new byte[FLASH.Length + 256];

                    lbl_status.Text = "Verify APM";
                    progress.Value = 0;
                    this.Refresh();

                    while (start < FLASH.Length)
                    {
                        progress.Value = (int)((start / (float)FLASH.Length) * 100);
                        this.Refresh();
                        Console.WriteLine("Downloading " + length + " at " + start);
                        port.setaddress(start);
                        port.downloadflash(length).CopyTo(flashverify, start);
                        start += length;
                    }

                    progress.Value = 100;

                    for (int s = 0; s < FLASH.Length; s++)
                    {
                        if (FLASH[s] != flashverify[s])
                            MessageBox.Show("Upload succeeded, but verify failed");
                    }

                    lbl_status.Text = "Done";
                }
                else
                {
                    lbl_status.Text = "Failed upload";
                    MessageBox.Show("Communication Error - no connection");
                }
                port.Close();

                flashing = false;

            }
            catch (Exception ex) { lbl_status.Text = "Failed upload"; MessageBox.Show("Check port settings or Port in use? " + ex.ToString()); port.Close(); }
            flashing = false;
            MainV2.givecomport = false;
        }

        void port_Progress(int progress)
        {
            Console.WriteLine("Progress {0} ", progress);
            this.progress.Value = progress;
            this.progress.Refresh();
        }

        byte[] readIntelHEXv2(StreamReader sr)
        {
            byte[] FLASH = new byte[sr.BaseStream.Length / 2];

            int optionoffset = 0;
            int total = 0;

            while (!sr.EndOfStream)
            {
                progress.Value = (int)(((float)sr.BaseStream.Position / (float)sr.BaseStream.Length) * 100);
                progress.Refresh();

                string line = sr.ReadLine();

                if (line.StartsWith(":"))
                {
                    int length = Convert.ToInt32(line.Substring(1, 2), 16);
                    int address = Convert.ToInt32(line.Substring(3, 4), 16);
                    int option = Convert.ToInt32(line.Substring(7, 2), 16);
                    Console.WriteLine("len {0} add {1} opt {2}", length, address, option);

                    if (option == 0)
                    {
                        string data = line.Substring(9, length * 2);
                        for (int i = 0; i < length; i++)
                        {
                            byte byte1 = Convert.ToByte(data.Substring(i * 2, 2), 16);
                            FLASH[optionoffset + address] = byte1;
                            address++;
                            if ((optionoffset + address) > total)
                                total = optionoffset + address;
                        }
                    }
                    else if (option == 2)
                    {
                        optionoffset += (int)Convert.ToUInt16(line.Substring(9, 4), 16) << 4;
                    }
                    int checksum = Convert.ToInt32(line.Substring(line.Length - 2, 2), 16);
                }
                //Regex regex = new Regex(@"^:(..)(....)(..)(.*)(..)$"); // length - address - option - data - checksum
            }

            Array.Resize<byte>(ref FLASH, total);

            return FLASH;
        }

        private void FirmwareVisual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flashing == true)
            {
                e.Cancel = true;
                MessageBox.Show("Cant exit while updating");
            }
        }

        private void ACSetup_Click(object sender, EventArgs e)
        {
            MainV2.givecomport = true;

            MessageBox.Show("Please make sure you are in CLI/Setup mode");

            SerialPort comPortT = MainV2.comPort;

            if (comPortT.IsOpen)
                comPortT.Close();

            comPortT.DtrEnable = true;
            if (MainV2.comportname == null)
            {
                MessageBox.Show("Please select a valid comport! look in the Options menu");
                MainV2.givecomport = false;
                return;
            }
            try
            {
                comPortT.Open();

            }
            catch (Exception ex) { MainV2.givecomport = false; MessageBox.Show("Invalid Comport Settings : " + ex.Message); return; }

            lbl_status.Text = "Comport Opened";
            this.Refresh();

            comPortT.DtrEnable = false;
            System.Threading.Thread.Sleep(100);
            comPortT.DtrEnable = true;
            System.Threading.Thread.Sleep(3000);
            comPortT.DtrEnable = false;
            System.Threading.Thread.Sleep(100);
            comPortT.DtrEnable = true;

            string data = "";

            DateTime timeout = DateTime.Now;
            ///////////////////////// FIX ME////////////////////////////////////////////////////////
            int step = 0;
            //System.Threading.Thread.Sleep(2000);
            //comPortT.Write("IMU\r");

            comPortT.ReadTimeout = -1;

            while (comPortT.IsOpen)
            {
                string line = "";
                try
                {
                    if ( comPortT.BytesToRead > 0 )
                        line = comPortT.ReadLine();
                }
                catch
                {
                    try { line = comPortT.ReadExisting(); }
                    catch { MessageBox.Show("Can not read from serial port - existing"); return; }
                }

                this.Refresh();
                Application.DoEvents();

                Console.Write(line + "\n");
                switch (step)
                {
                    case 0:
                        if (line.Contains("interactive"))
                        {
                            lbl_status.Text = "Erasing EEPROM.. (20 seconds)";
                            this.Refresh();

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");
                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("erase\r");
                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("y\r");
                            step = 0;
                        }
                        if (line.Contains("done"))
                        {
                            lbl_status.Text = "Rebooting APM..";
                            this.Refresh();

                            comPortT.DtrEnable = false;
                            System.Threading.Thread.Sleep(100);
                            comPortT.DtrEnable = true;
                            System.Threading.Thread.Sleep(3000);
                            comPortT.DtrEnable = false;
                            Console.WriteLine(comPortT.ReadExisting());
                            System.Threading.Thread.Sleep(100);
                            comPortT.DtrEnable = true;
                            step = 1;
                        }
                        break;
                    case 1:
                        if (line.Contains("interactive")) // becuase we rebooted
                        {
                            lbl_status.Text = "Setup Radio..";
                            this.Refresh();

                            Console.WriteLine(comPortT.ReadExisting());

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");
                            System.Threading.Thread.Sleep(200);
                            MessageBox.Show("Ensure that your RC transmitter is on, and that you have your ArduCopter battery plugged in or are otherwise powering APM's RC pins (USB power does NOT power the RC receiver)", "Radio Setup");
                            comPortT.Write("radio\r");
                            MessageBox.Show("Move all your radio controls to each extreme. Hit OK when done.", "Radio Setup");
                            //comPortT.DiscardInBuffer();
                            comPortT.Write("\r\r");
                            step = 2;
                        }
                        break;
                    case 2:
                        if (data.Contains("----"))
                            data = "";
                        data += line;

                        if (line.Contains("CH7")) // 
                        {
                            MessageBox.Show("Here are the detected radio options\nNOTE Channels not connected are displayed as 1200\nNormal values are around 1100 | 1900\nChannel:Min | Max \n" + data, "Radio");

                            lbl_status.Text = "Setup Accel Offsets..";
                            this.Refresh();

                            MessageBox.Show("Ensure your quad is level, and click OK to continue", "Offset Setup");

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");
                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("level\r");
                            System.Threading.Thread.Sleep(1000);
                            step = 3;
                        }
                        break;
                    case 3:
                        if (line.Contains("IMU")) // 
                        {
                            lbl_status.Text = "Setup Options";
                            this.Refresh();

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");

                            DialogResult dr;
                            /*
                            dr = MessageBox.Show("Do you have a Current sensor attached?","Current Sensor",MessageBoxButtons.YesNo);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                comPortT.Write("current on\r");
                                System.Threading.Thread.Sleep(100);
                            }*/
                            dr = MessageBox.Show("Do you have a Sonar sensor attached?", "Sonar Sensor", MessageBoxButtons.YesNo);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                comPortT.Write("sonar on\r");
                                System.Threading.Thread.Sleep(100);
                            }
                            dr = MessageBox.Show("Do you have a Compass sensor attached?", "Compass Sensor", MessageBoxButtons.YesNo);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                comPortT.Write("compass on\r");
                                System.Threading.Thread.Sleep(100);

                                MessageBox.Show("Next a webpage will appear to get your magnetic declination,\nenter it in the box that appears next", "Mag Dec");

                                try
                                {
                                    System.Diagnostics.Process.Start("http://www.ngdc.noaa.gov/geomagmodels/Declination.jsp");
                                }
                                catch { MessageBox.Show("Webpage open failed... do you have a virus?"); }
                                //This can be taken from 

                                string declination = "0";
                                Common.InputBox("Declination", "Magnetic Declination (-15.0 to 15.0) eg 2° 3' W is -2.3", ref declination);
                                string mins = declination.Substring(declination.IndexOf('.', 0) + 1);
                                float dec = 0.0f;
                                float.TryParse(declination, out dec);
                                dec = (float)((int)dec);
                                if (dec > 0)
                                {
                                    dec += (int.Parse(mins) / 60.0f);
                                }
                                else
                                {
                                    dec -= (int.Parse(mins) / 60.0f);
                                }
                                comPortT.Write("exit\rsetup\rdeclination " + dec.ToString() + "\r");
                            }

                            string frame = "+";
                            Common.InputBox("Frame", "Enter Frame type (options +, x)", ref frame);
                            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                            byte[] data2 = encoding.GetBytes("exit\rsetup\rframe " + frame.ToLower() + "\r");
                            comPortT.Write(data2, 0, data2.Length);

                            MessageBox.Show("NOTE: this setup has defaulted all modes to stabilize.\n As you are new this is the safest mode.\n To change this option please use the modes option in the CLI");

                            comPortT.Close();
                            MainV2.givecomport = false;
                            return;
                            //step = 4;
                        }
                        break;
                    //
                }
            }

        }

        private void APMSetup_Click(object sender, EventArgs e)
        {
            MainV2.givecomport = true;

            MessageBox.Show("Please make sure you are in CLI/Setup mode");

            SerialPort comPortT = MainV2.comPort;

            if (comPortT.IsOpen)
                comPortT.Close();

            comPortT.DtrEnable = true;
            if (MainV2.comportname == null)
            {
                MessageBox.Show("Please select a valid comport! look in the Options menu");
                MainV2.givecomport = false;
                return;
            }
            try
            {
                comPortT.Open();

            }
            catch (Exception ex) { MainV2.givecomport = false; MessageBox.Show("Invalid Comport Settings : " + ex.Message); return; }

            lbl_status.Text = "Comport Opened";
            this.Refresh();

            comPortT.DtrEnable = false;
            System.Threading.Thread.Sleep(100);
            comPortT.DtrEnable = true;
            System.Threading.Thread.Sleep(3000);
            comPortT.DtrEnable = false;
            System.Threading.Thread.Sleep(100);
            comPortT.DtrEnable = true;

            string data = "";

            DateTime timeout = DateTime.Now;
            ///////////////////////// FIX ME////////////////////////////////////////////////////////
            int step = 0;
            //System.Threading.Thread.Sleep(2000);
            //comPortT.Write("CH7\r");

            comPortT.ReadTimeout = -1;

            while (comPortT.IsOpen)
            {
                string line;
                try
                {
                    line = comPortT.ReadLine();
                }
                catch
                {
                    try { line = comPortT.ReadExisting(); }
                    catch { MessageBox.Show("Can not read from serial port - existing"); return; }
                }
                this.Refresh();
                Console.Write(line + "\n");
                switch (step)
                {
                    case 0:
                        if (line.Contains("interactive"))
                        {
                            lbl_status.Text = "Erasing EEPROM.. (20 seconds)";
                            this.Refresh();

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");
                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("erase\r");
                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("y\r");
                            step = 0;
                        }
                        if (line.Contains("done"))
                        {
                            lbl_status.Text = "Rebooting APM..";
                            this.Refresh();

                            comPortT.DtrEnable = false;
                            System.Threading.Thread.Sleep(100);
                            comPortT.DtrEnable = true;
                            System.Threading.Thread.Sleep(3000);
                            comPortT.DtrEnable = false;
                            System.Threading.Thread.Sleep(100);
                            comPortT.DtrEnable = true;
                            step = 1;
                        }
                        break;
                    case 1:
                        if (line.Contains("interactive")) // becuase we rebooted
                        {
                            lbl_status.Text = "Setup Radio..";
                            this.Refresh();

                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine(comPortT.ReadExisting());

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("setup\r");
                            System.Threading.Thread.Sleep(200);
                            MessageBox.Show("Ensure that your RC transmitter is on, and that you have your ArduPilot battery plugged in or are otherwise powering APM's RC pins (USB power does NOT power the RC receiver)", "Radio Setup");
                            comPortT.Write("radio\r");
                            Console.WriteLine(comPortT.ReadExisting());
                            MessageBox.Show("Move all your radio controls to each extreme. Hit OK when done.", "Radio Setup");
                            comPortT.Write("\r\r");
                            step = 2;
                        }
                        break;
                    case 2:
                        if (data.Contains("----"))
                            data = "";
                        data += line;

                        if (line.Contains("CH7")) // 
                        {
                            MessageBox.Show("Here are the detected radio options\nNOTE Channels not connected are displayed as 1200\nNormal values are around 1100 | 1900\nChannel:Min | Max \n" + data, "Radio");

                            lbl_status.Text = "Clearing Log Dataflash (20 seconds)";
                            this.Refresh();

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("exit\rlogs\rerase\r");
                            System.Threading.Thread.Sleep(200);
                            Console.WriteLine(comPortT.ReadExisting());
                            step = 3;
                        }
                        break;
                    case 3:
                        if (line.Contains("Log erased"))
                        {
                            lbl_status.Text = "Setup Options";
                            this.Refresh();

                            Console.WriteLine(comPortT.ReadExisting());

                            System.Threading.Thread.Sleep(50);
                            comPortT.Write("exit\rsetup\r");

                            DialogResult dr;
                            dr = MessageBox.Show("Do you have a Compass sensor attached?", "Compass Sensor", MessageBoxButtons.YesNo);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                comPortT.Write("compass on\r");
                                System.Threading.Thread.Sleep(100);

                                MessageBox.Show("Next a webpage will appear to get your magnetic declination,\nenter it in the box that appears next", "Mag Dec");

                                try
                                {
                                    System.Diagnostics.Process.Start("http://www.ngdc.noaa.gov/geomagmodels/Declination.jsp");
                                }
                                catch { MessageBox.Show("Webpage open failed... do you have a virus?"); }
                                //This can be taken from 

                                string declination = "0";
                                Common.InputBox("Declination", "Magnetic Declination (-15.0 to 15.0) eg 2° 3' W is -2.3", ref declination);
                                string mins = declination.Substring(declination.IndexOf('.', 0) + 1);
                                float dec = 0.0f;
                                float.TryParse(declination, out dec);
                                dec = (float)((int)dec);
                                if (dec > 0)
                                {
                                    dec += (int.Parse(mins) / 60.0f);
                                }
                                else
                                {
                                    dec -= (int.Parse(mins) / 60.0f);
                                }
                                comPortT.Write("exit\rsetup\rdeclination " + dec.ToString() + "\r");
                            }

                            MessageBox.Show("NOTE: this setup has defaulted all modes to there default.\n As you are new these are the safest.\n To change this option please use the modes option in the CLI");

                            Console.WriteLine(comPortT.ReadExisting());

                            comPortT.Close();
                            MainV2.givecomport = false;
                            lbl_status.Text = "Setup Done";
                            this.Refresh();
                            return;
                            //step = 4;
                        }
                        break;
                    //
                }
            }
        }


        //private bool _manuel_firmware = false;
        private void pictureBoxHeli_Click(object sender, EventArgs e)
        {
            //findfirmware("AC2-Heli-");
        }

        private String _selected_Firmware = "";
        private void downaload_manuel()
        {
            openFileDialog.Filter = "(*.hex)|*.hex";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    try
                    {

                        _selected_Firmware = openFileDialog.FileName;
                        DialogResult dr = MessageBox.Show("Are you sure you want to upload " + "?", "Continue", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            update(_selected_Firmware);
                        }
                        return;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        private void btn_manuel_Click(object sender, EventArgs e)
        {
            downaload_manuel();
        }


    }
}