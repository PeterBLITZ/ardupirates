using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;



namespace ArdupilotMega
{
    public partial class JoystickSetup : Form
    {
        public JoystickSetup()
        {
            InitializeComponent();
        }

        private void Joystick_Load(object sender, EventArgs e)
        {
            DeviceList joysticklist = Joystick.getDevices();

            foreach (DeviceInstance device in joysticklist)
            {
                CMB_joysticks.Items.Add(device.ProductName);
            }

            if (CMB_joysticks.Items.Count > 0)
                CMB_joysticks.SelectedIndex = 0;

            CMB_CH1.DataSource = (Enum.GetValues(typeof(Joystick.joystickaxis)));
            CMB_CH2.DataSource = (Enum.GetValues(typeof(Joystick.joystickaxis)));
            CMB_CH3.DataSource = (Enum.GetValues(typeof(Joystick.joystickaxis)));
            CMB_CH4.DataSource = (Enum.GetValues(typeof(Joystick.joystickaxis)));

            try
            {
                //CMB_CH1
                CMB_CH1.Text = MainV2.config["CMB_CH1"].ToString();
                CMB_CH2.Text = MainV2.config["CMB_CH2"].ToString();
                CMB_CH3.Text = MainV2.config["CMB_CH3"].ToString();
                CMB_CH4.Text = MainV2.config["CMB_CH4"].ToString();

                //revCH1
                revCH1.Checked = bool.Parse(MainV2.config["revCH1"].ToString());
                revCH2.Checked = bool.Parse(MainV2.config["revCH2"].ToString());
                revCH3.Checked = bool.Parse(MainV2.config["revCH3"].ToString());
                revCH4.Checked = bool.Parse(MainV2.config["revCH4"].ToString());

                //expo_ch1
                expo_ch1.Text = MainV2.config["expo_ch1"].ToString();
                expo_ch2.Text = MainV2.config["expo_ch2"].ToString();
                expo_ch3.Text = MainV2.config["expo_ch3"].ToString();
                expo_ch4.Text = MainV2.config["expo_ch4"].ToString();
            }
            catch { } // IF 1 DOESNT EXIST NONE WILL

            if (MainV2.joystickenabled)
            {
                timer1.Start();
            }
        }

        private void BUT_enable_Click(object sender, EventArgs e)
        {
            if (MainV2.joystick == null)
            {
                Joystick joy = new Joystick();
                joy.setChannel(1, (Joystick.joystickaxis)Enum.Parse(typeof(Joystick.joystickaxis), CMB_CH1.Text), revCH1.Checked, int.Parse(expo_ch1.Text));
                joy.setChannel(2, (Joystick.joystickaxis)Enum.Parse(typeof(Joystick.joystickaxis), CMB_CH2.Text), revCH2.Checked, int.Parse(expo_ch2.Text));
                joy.setChannel(3, (Joystick.joystickaxis)Enum.Parse(typeof(Joystick.joystickaxis), CMB_CH3.Text), revCH3.Checked, int.Parse(expo_ch3.Text));
                joy.setChannel(4, (Joystick.joystickaxis)Enum.Parse(typeof(Joystick.joystickaxis), CMB_CH4.Text), revCH4.Checked, int.Parse(expo_ch4.Text));

                joy.start(CMB_joysticks.Text);

                MainV2.joystick = joy;
                MainV2.joystickenabled = true;

                BUT_enable.Text = "Disable";

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                MainV2.joystickenabled = false;
                MainV2.joystick = null;
                BUT_enable.Text = "Enable";
            }

            /*
            MainV2.cs.rcoverridech1 = pickchannel(1, CMB_CH1.Text, revCH1.Checked, int.Parse(expo_ch1.Text));//(ushort)(((int)state.Rz / 65.535) + 1000);
            MainV2.cs.rcoverridech2 = pickchannel(2, CMB_CH2.Text, revCH2.Checked, int.Parse(expo_ch2.Text));//(ushort)(((int)state.Y / 65.535) + 1000);
            MainV2.cs.rcoverridech3 = pickchannel(3, CMB_CH3.Text, revCH3.Checked, int.Parse(expo_ch3.Text));//(ushort)(1000 - ((int)slider[0] / 65.535) + 1000);
            MainV2.cs.rcoverridech4 = pickchannel(4, CMB_CH4.Text, revCH4.Checked, int.Parse(expo_ch4.Text));//(ushort)(((int)state.X / 65.535) + 1000);
            */
        }

        private void CMB_CH1_Click(object sender, EventArgs e)
        {
            ((ComboBox)(sender)).Text = Joystick.getMovingAxis(CMB_joysticks.Text,16000).ToString();
        }

        private void CMB_CH2_Click(object sender, EventArgs e)
        {
            ((ComboBox)(sender)).Text = Joystick.getMovingAxis(CMB_joysticks.Text, 16000).ToString();
        }

        private void CMB_CH3_Click(object sender, EventArgs e)
        {
            ((ComboBox)(sender)).Text = Joystick.getMovingAxis(CMB_joysticks.Text, 16000).ToString();
        }

        private void CMB_CH4_Click(object sender, EventArgs e)
        {
            ((ComboBox)(sender)).Text = Joystick.getMovingAxis(CMB_joysticks.Text, 16000).ToString();
        }

        private void BUT_save_Click(object sender, EventArgs e)
        {
            //CMB_CH1
            MainV2.config["CMB_CH1"] = CMB_CH1.Text;
            MainV2.config["CMB_CH2"] = CMB_CH2.Text;
            MainV2.config["CMB_CH3"] = CMB_CH3.Text;
            MainV2.config["CMB_CH4"] = CMB_CH4.Text;

            //revCH1
            MainV2.config["revCH1"] = revCH1.Checked;
            MainV2.config["revCH2"] = revCH2.Checked;
            MainV2.config["revCH3"] = revCH3.Checked;
            MainV2.config["revCH4"] = revCH4.Checked;
            
            //expo_ch1
            MainV2.config["expo_ch1"] = expo_ch1.Text;
            MainV2.config["expo_ch2"] = expo_ch2.Text;
            MainV2.config["expo_ch3"] = expo_ch3.Text;
            MainV2.config["expo_ch4"] = expo_ch4.Text;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = MainV2.cs.rcoverridech1;
            progressBar2.Value = MainV2.cs.rcoverridech2;
            progressBar3.Value = MainV2.cs.rcoverridech3;
            progressBar4.Value = MainV2.cs.rcoverridech4;
        }

        private void CMB_joysticks_Click(object sender, EventArgs e)
        {
            CMB_joysticks.Items.Clear();

            DeviceList joysticklist = Joystick.getDevices();

            foreach (DeviceInstance device in joysticklist)
            {
                CMB_joysticks.Items.Add(device.ProductName);
            }

            if (CMB_joysticks.Items.Count > 0)
                CMB_joysticks.SelectedIndex = 0;
        }

        private void revCH1_CheckedChanged(object sender, EventArgs e)
        {
            if (MainV2.joystick != null)
            MainV2.joystick.setReverse(1,((CheckBox)sender).Checked);
        }

        private void revCH2_CheckedChanged(object sender, EventArgs e)
        {
            if (MainV2.joystick != null)
            MainV2.joystick.setReverse(2, ((CheckBox)sender).Checked);
        }

        private void revCH3_CheckedChanged(object sender, EventArgs e)
        {
            if (MainV2.joystick != null)
            MainV2.joystick.setReverse(3, ((CheckBox)sender).Checked);
        }

        private void revCH4_CheckedChanged(object sender, EventArgs e)
        {
            if (MainV2.joystick != null)
            MainV2.joystick.setReverse(4, ((CheckBox)sender).Checked);
        }
    }
}
