using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArdupilotMega;
using System.IO.Ports;

namespace ArdupilotMega.GCSViews
{
    public partial class Terminal : UserControl
    {
        SerialPort comPort = MainV2.comPort;
        Object thisLock = new Object();
        public static bool threadrun = false;
        bool inlogview = false;

        public Terminal()
        {
            while (threadrun == true)
                threadrun = false;

            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false; // so can update display from another thread
        }

        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!comPort.IsOpen)
                return;
            try
            {
                lock (thisLock)
                {
                    TXT_terminal.SelectionStart = TXT_terminal.Text.Length;
                    string data = comPort.ReadExisting();
                    data = data.TrimEnd('\r'); // else added \n all by itself
                    TXT_terminal.AppendText(data);
                    if (data.Contains("\b"))
                    {
                        TXT_terminal.Text = TXT_terminal.Text.Remove(TXT_terminal.Text.IndexOf('\b'));
                        TXT_terminal.SelectionStart = TXT_terminal.Text.Length;
                    }
                }
            }
            catch (Exception) { if (!threadrun) return; TXT_terminal.AppendText("Error reading com port\r\n"); }
        }

        private void TXT_terminal_Click(object sender, EventArgs e)
        {
            // auto scroll
            TXT_terminal.SelectionStart = TXT_terminal.Text.Length;

            TXT_terminal.ScrollToCaret();

            TXT_terminal.Refresh();
        }

        private void TXT_terminal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                e.Handled = true; // ignore it
            }
        }

        private void Terminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadrun = false;

            if (comPort.IsOpen)
            {
                comPort.Close();
            }
            System.Threading.Thread.Sleep(400);
        }

        private void TXT_terminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comPort.IsOpen)
            {
                comPort.Write(new byte[] { (byte)e.KeyChar }, 0, 1);
            }
            e.Handled = true;
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Set your APM into LOG/SETUP mode!! (switch towards the servo header)");

            try
            {
                MainV2.givecomport = true;

                if (comPort.IsOpen)
                    comPort.Close();

                comPort.DtrEnable = true;

                comPort.ReadBufferSize = 1024 * 1024;

                comPort.Open();

                System.Threading.Thread t11 = new System.Threading.Thread(delegate()
                {
                    threadrun = true;
                    while (threadrun)
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(10);
                            if (inlogview)
                                continue;
                            if (!comPort.IsOpen)
                                break;
                            if (comPort.BytesToRead > 0)
                            {
                                comPort_DataReceived((object)null, (SerialDataReceivedEventArgs)null);
                            }
                        }
                        catch { }
                    }
                    if (threadrun == false)
                    {
                        comPort.Close();
                    }
                    Console.WriteLine("Comport thread close");
                });
                t11.IsBackground = true;
                t11.Name = "Terminal serial thread";
                t11.Start();
                MainV2.threads.Add(t11);

                // doesnt seem to work on mac
                //comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

                TXT_terminal.AppendText("Opened com port\r\n");
            }
            catch (Exception) { TXT_terminal.AppendText("Cant open serial port\r\n"); return; }
        }

        private void BUTsetupshow_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] data = encoding.GetBytes("exit\rsetup\rshow\r");
                comPort.Write(data, 0, data.Length);
            }
            TXT_terminal.Focus();
        }

        private void BUTradiosetup_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] data = encoding.GetBytes("exit\rsetup\r\nradio\r");
                comPort.Write(data, 0, data.Length);
            }
            TXT_terminal.Focus();
        }

        private void BUTtests_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] data = encoding.GetBytes("exit\rtest\r?\r\n");
                comPort.Write(data, 0, data.Length);
            }
            TXT_terminal.Focus();
        }

        private void Logs_Click(object sender, EventArgs e)
        {
            Form Log = new Log();
            MainV2.fixtheme(Log);
            inlogview = true;
            Log.ShowDialog();
            inlogview = false;
        }

        private void BUT_logbrowse_Click(object sender, EventArgs e)
        {
            Form logbrowse = new LogBrowse();
            MainV2.fixtheme(logbrowse);
            logbrowse.ShowDialog();
        }
    }
}
