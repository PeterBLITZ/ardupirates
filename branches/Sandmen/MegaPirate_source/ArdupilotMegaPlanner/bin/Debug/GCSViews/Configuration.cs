using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace ArdupilotMega.GCSViews
{
    public partial class Configuration : UserControl
    {
        Hashtable param = new Hashtable();
        Hashtable changes = new Hashtable();
        bool startup = true;
        List<CultureInfo> languages = new List<CultureInfo>();

        public Configuration()
        {
            InitializeComponent();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            param = MainV2.comPort.param;
            processToScreen();
            if (MainV2.APMFirmware == MainV2.Firmwares.ArduPilotMega)
            {
                ConfigTabs.SelectedIndex = 0;
                TabAPM2.Enabled = true;
                TabAC2.Enabled = false;
            }
            else
            {
                ConfigTabs.SelectedIndex = 1;
                TabAPM2.Enabled = false;
                TabAC2.Enabled = true;
            }

            if (MainV2.cam != null)
            {
                BUT_videostart.Enabled = false;
                CHK_hudshow.Checked = MainV2.cam.showhud;
            }
            else
            {
                BUT_videostart.Enabled = true;
            }

            if (MainV2.config["speechenable"] != null)
                CHK_enablespeech.Checked = bool.Parse(MainV2.config["speechenable"].ToString());
            if (MainV2.config["speechwaypointenabled"] != null)
                CHK_speechwaypoint.Checked = bool.Parse(MainV2.config["speechwaypointenabled"].ToString());
            if (MainV2.config["speechmodeenabled"] != null)
                CHK_speechmode.Checked = bool.Parse(MainV2.config["speechmodeenabled"].ToString());
            if (MainV2.config["speechcustomenabled"] != null)
                CHK_speechcustom.Checked = bool.Parse(MainV2.config["speechcustomenabled"].ToString());
            if (MainV2.config["speechbatteryenabled"] != null)
                CHK_speechbattery.Checked = bool.Parse(MainV2.config["speechbatteryenabled"].ToString());            

            string hudcolor = (string)MainV2.config["hudcolor"];

            CMB_osdcolor.DataSource = Enum.GetNames(typeof(KnownColor));
            if (hudcolor != null)
            {
                int index = CMB_osdcolor.Items.IndexOf(hudcolor);
                CMB_osdcolor.SelectedIndex = index;
            }
            else
            {
                int index = CMB_osdcolor.Items.IndexOf("White");
                CMB_osdcolor.SelectedIndex = index;
            }

            CultureInfo ci = null;
            foreach (string name in new string[] { "en-US", "zh-Hans" })
            {
                ci = MainV2.getcultureinfo(name);
                if (ci != null)
                    languages.Add(ci);
            }

            CMB_language.DisplayMember = "DisplayName";
            CMB_language.DataSource = languages;
            bool match = false;
            for (int i = 0; i < languages.Count && !match; i++)
            {
                ci = Thread.CurrentThread.CurrentUICulture;
                while (!ci.Equals(CultureInfo.InvariantCulture))
                {
                    if (ci.Equals(languages[i]))
                    {
                        CMB_language.SelectedIndex = i;
                        match = true;
                        break;
                    }
                    ci = ci.Parent;
                }
            }
            CMB_language.SelectedIndexChanged += CMB_language_SelectedIndexChanged;

            startup = false;
        }

        string[] genpids()
        {
            List<string> temp = new List<string>();
            // pids
            for (double a = 4.00; a >= -0.001; a -= 0.001)
            {
                temp.Add(a.ToString("0.0##"));
            }

            // Nav angles + throttle
            for (int a = 100; a >= -90; a -= 1)
            {
                temp.Add(a.ToString("0.0#"));
            }

            // imax
            for (int a = 8000; a >= -4500; a -= 100)
            {
                temp.Add(a.ToString("0.0#"));
            }

            // FS pulse
            for (int a = 1200; a >= 900; a -= 1)
            {
                temp.Add(a.ToString("0.0#"));
            }

            return temp.ToArray();
        }

        void processToScreen()
        {
            Params.Rows.Clear();

            // process hashdefines and update display
            foreach (string value in param.Keys)
            {
                if (value == null || value == "")
                    continue;

                //System.Diagnostics.Debug.WriteLine("Doing: " + value);

                Params.Rows.Add();
                Params.Rows[Params.RowCount - 1].Cells[0].Value = value;
                Params.Rows[Params.RowCount - 1].Cells[1].Value = ((float)param[value]).ToString("0.###");

                string name = value;
                Control[] text = this.Controls.Find(name, true);
                if (text.Length > 0)
                {
                    ((DomainUpDown)text[0]).Items.AddRange(genpids());
                    string option = ((float)param[value]).ToString("0.0##");
                    int index = ((DomainUpDown)text[0]).Items.IndexOf(option);
                    ((DomainUpDown)text[0]).BackColor = Color.FromArgb(0x43, 0x44, 0x45);
                    Console.WriteLine(name + " " + option + " " + index + " " + ((float)param[value]));
                    if (index == -1)
                    {
                        ((DomainUpDown)text[0]).Text = ((float)param[value]).ToString("0.0##");
                    }
                    else
                    {
                        ((DomainUpDown)text[0]).SelectedIndex = index;
                    }
                    ((DomainUpDown)text[0]).Validated += new EventHandler(EEPROM_View_float_TextChanged);
                }
                else
                {
                    Console.WriteLine(name + " not found");
                }

            }

            Params.Sort(Params.Columns[0], ListSortDirection.Ascending);
        }

        void EEPROM_View_float_TextChanged(object sender, EventArgs e)
        {
            try
            {
                changes[((Control)sender).Name] = float.Parse(((Control)sender).Text);
                ((Control)sender).BackColor = Color.Green;
            }
            catch (Exception)
            {
                ((Control)sender).BackColor = Color.Red;
            }

            try
            {
                // set param table as well
                foreach (DataGridViewRow row in Params.Rows)
                {
                    if (row.Cells[0].Value.ToString() == ((Control)sender).Name)
                    {
                        row.Cells[1].Value = float.Parse(((Control)sender).Text);
                        break;
                    }
                }
            }
            catch { }
            //((Control)sender).Focus();
        }

        private void Params_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1 || startup == true || e.ColumnIndex != 1)
                return;
            try
            {
                Params[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Green;
                changes[Params[0, e.RowIndex].Value] = float.Parse(Params[e.ColumnIndex, e.RowIndex].Value.ToString());
            }
            catch (Exception)
            {
                Params[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
            }

            try
            {
                // set control as well
                Control[] text = this.Controls.Find(Params[0, e.RowIndex].Value.ToString(), true);
                if (text.Length > 0)
                {
                    string option = (float.Parse(Params[e.ColumnIndex, e.RowIndex].Value.ToString())).ToString("0.0##", System.Globalization.CultureInfo.CurrentCulture);
                    int index = ((DomainUpDown)text[0]).Items.IndexOf(option);
                    if (index != -1)
                    {
                        ((DomainUpDown)text[0]).SelectedIndex = index;
                        ((DomainUpDown)text[0]).BackColor = Color.Green;
                    }
                }
            }
            catch { }

            Params.Focus();
        }

        private void BUT_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = ".param";
            ofd.RestoreDirectory = true;
            ofd.Filter = "Param List|*.param";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.OpenFile());
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    int index = line.IndexOf(',');

                    if (index == -1)
                        continue;

                    string name = line.Substring(0, index);
                    float value = float.Parse(line.Substring(index + 1), new System.Globalization.CultureInfo("en-US"));

                    // set param table as well
                    foreach (DataGridViewRow row in Params.Rows)
                    {
                        if (name == "WP_TOTAL")
                            continue;
                        if (row.Cells[0].Value.ToString() == name)
                        {
                            if (row.Cells[1].Value.ToString() != value.ToString())
                                row.Cells[1].Value = value;
                            break;
                        }
                    }
                }
                sr.Close();
            }
        }

        private void BUT_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".param";
            sfd.RestoreDirectory = true;
            sfd.Filter = "Param List|*.param";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.OpenFile());
                foreach (DataGridViewRow row in Params.Rows)
                {
                    sw.WriteLine(row.Cells[0].Value.ToString() + "," + float.Parse(row.Cells[1].Value.ToString()).ToString(new System.Globalization.CultureInfo("en-US")));
                }
                sw.Close();
            }
        }

        private void BUT_writePIDS_Click(object sender, EventArgs e)
        {
            foreach (string value in changes.Keys)
            {
                try
                {
                    MainV2.comPort.setParam(value, (float)changes[value]);

                    try
                    {
                        // set control as well
                        Control[] text = this.Controls.Find(value, true);
                        if (text.Length > 0)
                        {
                            ((DomainUpDown)text[0]).BackColor = Color.FromArgb(0x43, 0x44, 0x45);
                        }
                    }
                    catch { }

                    try
                    {
                        // set param table as well
                        foreach (DataGridViewRow row in Params.Rows)
                        {
                            if (row.Cells[0].Value.ToString() == value)
                            {
                                row.Cells[1].Style.BackColor = Color.FromArgb(0x43, 0x44, 0x45);
                                break;
                            }
                        }
                    }
                    catch { }

                }
                catch { MessageBox.Show("Set " + value + " Failed"); }
            }           

            changes.Clear();
        }

        private void Planner_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void CMB_videosources_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MainV2.webCamCapture1.CaptureHeight = 240;
            //MainV2.webCamCapture1.CaptureWidth = 320;

            //MainV2.videoDevice = WebCam_Capture.WebCamCapture.GetDevice(CMB_videosources.SelectedIndex);
        }

        private void BUT_videostart_Click(object sender, EventArgs e)
        {
            // stop first
            BUT_videostop_Click(sender, e);

            try
            {
                MainV2.cam = new WebCamService.Capture(CMB_videosources.SelectedIndex, 15, 320, 240);

                MainV2.cam.showhud = CHK_hudshow.Checked;

                MainV2.cam.Start();

                BUT_videostart.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("Camera Fail: " + ex.Message); }

        }

        private void BUT_videostop_Click(object sender, EventArgs e)
        {
            BUT_videostart.Enabled = true;
            if (MainV2.cam != null)
            {
                MainV2.cam.Dispose();
                MainV2.cam = null;
            }
        }

        private void CMB_videosources_MouseClick(object sender, MouseEventArgs e)
        {
            // the reason why i dont populate this list is because on linux/mac this call will fail.
            WebCamService.Capture capt = new WebCamService.Capture();

            List<string> devices = WebCamService.Capture.getDevices();

            CMB_videosources.DataSource = devices;

            capt.Dispose();
        }

        private void CHK_hudshow_CheckedChanged(object sender, EventArgs e)
        {
            GCSViews.FlightData.myhud.hudon = CHK_hudshow.Checked;
        }

        private void CHK_enablespeech_CheckedChanged(object sender, EventArgs e)
        {
            MainV2.speechenable = CHK_enablespeech.Checked;
            MainV2.config["speechenable"] = CHK_enablespeech.Checked;
            if (MainV2.talk != null)
                MainV2.talk.SpeakAsyncCancelAll();
        }
        private void CMB_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainV2.instance.changelanguage((CultureInfo)CMB_language.SelectedItem);
        }

        private void CMB_osdcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startup)
                return;
            if (CMB_osdcolor.Text != "")
            {
                MainV2.config["hudcolor"] = CMB_osdcolor.Text;
                GCSViews.FlightData.myhud.hudcolor = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), CMB_osdcolor.Text));
            }
        }

        private void CHK_speechwaypoint_CheckedChanged(object sender, EventArgs e)
        {
            if (startup)
                return;
            MainV2.config["speechwaypointenabled"] = ((CheckBox)sender).Checked.ToString();

            if (((CheckBox)sender).Checked)
            {
                string speechstring = "Heading to Waypoint {wpn}";
                if (MainV2.config["speechwaypoint"] != null)
                    speechstring = MainV2.config["speechwaypoint"].ToString();
                Common.InputBox("Notification", "What do you want it to say?", ref speechstring);
                MainV2.config["speechwaypoint"] = speechstring;
            }
        }

        private void CHK_speechmode_CheckedChanged(object sender, EventArgs e)
        {
            if (startup)
                return;
            MainV2.config["speechmodeenabled"] = ((CheckBox)sender).Checked.ToString();

            if (((CheckBox)sender).Checked)
            {
                string speechstring = "Mode changed to {mode}";
                if (MainV2.config["speechmode"] != null)
                    speechstring = MainV2.config["speechmode"].ToString();
                Common.InputBox("Notification", "What do you want it to say?", ref speechstring);
                MainV2.config["speechmode"] = speechstring;
            }
        }

        private void CHK_speechcustom_CheckedChanged(object sender, EventArgs e)
        {
            if (startup)
                return;
            MainV2.config["speechcustomenabled"] = ((CheckBox)sender).Checked.ToString();

            if (((CheckBox)sender).Checked)
            {
                string speechstring = "Heading to Waypoint {wpn}, altitude is {alt}, Ground speed is {gsp} ";
                if (MainV2.config["speechcustom"] != null)
                    speechstring = MainV2.config["speechcustom"].ToString();
                Common.InputBox("Notification", "What do you want it to say?", ref speechstring);
                MainV2.config["speechcustom"] = speechstring;
            }
        }

        private void BUT_rerequestparams_Click(object sender, EventArgs e)
        {
            if (!MainV2.comPort.IsOpen)
                return;
            try
            {
                MainV2.comPort.getParamList();
            }
            catch { MessageBox.Show("Error: getting param list");  }

            startup = true;
            Configuration_Load(null,null);
        }

        private void CHK_speechbattery_CheckedChanged(object sender, EventArgs e)
        {
            if (startup)
                return;
            MainV2.config["speechbatteryenabled"] = ((CheckBox)sender).Checked.ToString();

            if (((CheckBox)sender).Checked)
            {
                string speechstring = "WARNING, Battery at {batv} Volt";
                if (MainV2.config["speechbattery"] != null)
                    speechstring = MainV2.config["speechbattery"].ToString();
                Common.InputBox("Notification", "What do you want it to say?", ref speechstring);
                MainV2.config["speechbattery"] = speechstring;

                speechstring = "9.6";
                if (MainV2.config["speechbatteryvolt"] != null)
                    speechstring = MainV2.config["speechbatteryvolt"].ToString();
                Common.InputBox("Battery Level", "What Voltage do you want to warn at?", ref speechstring);
                MainV2.config["speechbatteryvolt"] = speechstring;

            }
        }

        private void BUT_Joystick_Click(object sender, EventArgs e)
        {
            Form joy = new JoystickSetup();
            MainV2.fixtheme(joy);
            joy.Show();
        }
    }
}