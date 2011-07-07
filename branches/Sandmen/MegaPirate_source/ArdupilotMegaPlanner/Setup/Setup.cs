using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArdupilotMega.Setup
{
    public partial class Setup : Form
    {
        bool run = false;

        float[] rcmin = new float[8];
        float[] rcmax = new float[8];
        float[] rctrim = new float[8];

        public Setup()
        {
            InitializeComponent();

            for (int a = 0; a < rcmin.Length;a++)
            {
                rcmin[a] = 2200;
            }

                MainV2.cs.UpdateCurrentSettings(currentStateBindingSource, true);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MainV2.cs.UpdateCurrentSettings(currentStateBindingSource, true);
            base.OnPaint(e);
        }

        private void BUT_Calibrateradio_Click(object sender, EventArgs e)
        {
            if (run)
            {
                BUT_Calibrateradio.Text = "Please goto next tab";
                run = false;
                return;
            }

            MessageBox.Show("Ensure your transmitter is on and receiver is powered and connected");

            MainV2.cs.raterc = 10;
            MainV2.cs.rateattitude = 0;
            MainV2.cs.rateposition = 0;
            MainV2.cs.ratestatus = 0;

            MainV2.comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_RC_CHANNELS, 10);

            BUT_Calibrateradio.Text = "Click when Done";

            run = true;


            while (run)
            {
                Application.DoEvents();

                System.Threading.Thread.Sleep(5);

                MainV2.cs.UpdateCurrentSettings(currentStateBindingSource, true);

                if (MainV2.cs.ch1in > 800 && MainV2.cs.ch1in < 2200)
                {
                    rcmin[0] = Math.Min(rcmin[0], MainV2.cs.ch1in);
                    rcmax[0] = Math.Max(rcmax[0], MainV2.cs.ch1in);

                    rcmin[1] = Math.Min(rcmin[1], MainV2.cs.ch2in);
                    rcmax[1] = Math.Max(rcmax[1], MainV2.cs.ch2in);

                    rcmin[2] = Math.Min(rcmin[2], MainV2.cs.ch3in);
                    rcmax[2] = Math.Max(rcmax[2], MainV2.cs.ch3in);

                    rcmin[3] = Math.Min(rcmin[3], MainV2.cs.ch4in);
                    rcmax[3] = Math.Max(rcmax[3], MainV2.cs.ch4in);

                    rcmin[4] = Math.Min(rcmin[4], MainV2.cs.ch5in);
                    rcmax[4] = Math.Max(rcmax[4], MainV2.cs.ch5in);

                    rcmin[5] = Math.Min(rcmin[5], MainV2.cs.ch6in);
                    rcmax[5] = Math.Max(rcmax[5], MainV2.cs.ch6in);

                    rcmin[6] = Math.Min(rcmin[6], MainV2.cs.ch7in);
                    rcmax[6] = Math.Max(rcmax[6], MainV2.cs.ch7in);

                    rcmin[7] = Math.Min(rcmin[7], MainV2.cs.ch8in);
                    rcmax[7] = Math.Max(rcmax[7], MainV2.cs.ch8in);

                    BARroll.minline = (int)rcmin[0];
                    BARroll.maxline = (int)rcmax[0];

                    BARpitch.minline = (int)rcmin[1];
                    BARpitch.maxline = (int)rcmax[1];
                }
            }

            MainV2.cs.UpdateCurrentSettings(currentStateBindingSource, true);

            rctrim[0] = MainV2.cs.ch1in;
            rctrim[1] = MainV2.cs.ch2in;
            rctrim[2] = MainV2.cs.ch3in;
            rctrim[3] = MainV2.cs.ch4in;
            rctrim[4] = MainV2.cs.ch5in;
            rctrim[5] = MainV2.cs.ch6in;
            rctrim[6] = MainV2.cs.ch7in;
            rctrim[7] = MainV2.cs.ch8in;

            string data = "---------------\n";

            for (int a = 0; a < 8; a++)
            {
                // we want these to save no matter what
                BUT_Calibrateradio.Text = "Saving";
                MainV2.comPort.setParam("RC" + (a + 1).ToString("0") + "_MIN", rcmin[a]);
                MainV2.comPort.setParam("RC" + (a + 1).ToString("0") + "_MAX", rcmax[a]);
                MainV2.comPort.setParam("RC" + (a + 1).ToString("0") + "_TRIM", rctrim[a]);

                data = data + " " + rcmin[a] + " | " + rcmax[a] + "\n";
            }

            MessageBox.Show("Here are the detected radio options\nNOTE Channels not connected are displayed as 1500 +-2\nNormal values are around 1100 | 1900\nChannel:Min | Max \n" + data, "Radio");

            BUT_Calibrateradio.Text = "Please goto next tab";
        }
    }
}
