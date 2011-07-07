using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArdupilotMega
{
    public partial class Calc : Form
    {
        public Calc()
        {
            InitializeComponent();
            TXT_input.Text = (328.191663931736).ToString();
        }

        private void BUT_tometers_Click(object sender, EventArgs e)
        {
            try
            {
                TXT_output.Text = (double.Parse(TXT_input.Text) * 0.3047).ToString();
            }
            catch { TXT_output.Text = "Invalid Input"; }
        }

        private void BUT_tofeet_Click(object sender, EventArgs e)
        {
            try
            {
                TXT_output.Text = (double.Parse(TXT_input.Text) / 0.3047).ToString();
            }
            catch { TXT_output.Text = "Invalid Input"; }
        }
    }
}
