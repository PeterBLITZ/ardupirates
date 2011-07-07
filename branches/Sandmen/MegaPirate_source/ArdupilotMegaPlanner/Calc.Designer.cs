namespace ArdupilotMega
{
    partial class Calc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TXT_input = new System.Windows.Forms.TextBox();
            this.BUT_tometers = new ArdupilotMega.MyButton();
            this.BUT_tofeet = new ArdupilotMega.MyButton();
            this.TXT_output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TXT_input
            // 
            this.TXT_input.Location = new System.Drawing.Point(13, 13);
            this.TXT_input.Name = "TXT_input";
            this.TXT_input.Size = new System.Drawing.Size(122, 20);
            this.TXT_input.TabIndex = 0;
            // 
            // BUT_tometers
            // 
            this.BUT_tometers.Location = new System.Drawing.Point(13, 40);
            this.BUT_tometers.Name = "BUT_tometers";
            this.BUT_tometers.Size = new System.Drawing.Size(63, 23);
            this.BUT_tometers.TabIndex = 1;
            this.BUT_tometers.Text = "To Meters";
            this.BUT_tometers.UseVisualStyleBackColor = true;
            this.BUT_tometers.Click += new System.EventHandler(this.BUT_tometers_Click);
            // 
            // BUT_tofeet
            // 
            this.BUT_tofeet.Location = new System.Drawing.Point(82, 39);
            this.BUT_tofeet.Name = "BUT_tofeet";
            this.BUT_tofeet.Size = new System.Drawing.Size(53, 23);
            this.BUT_tofeet.TabIndex = 2;
            this.BUT_tofeet.Text = "To Feet";
            this.BUT_tofeet.UseVisualStyleBackColor = true;
            this.BUT_tofeet.Click += new System.EventHandler(this.BUT_tofeet_Click);
            // 
            // TXT_output
            // 
            this.TXT_output.Location = new System.Drawing.Point(13, 69);
            this.TXT_output.Name = "TXT_output";
            this.TXT_output.Size = new System.Drawing.Size(122, 20);
            this.TXT_output.TabIndex = 3;
            this.TXT_output.Text = "Answer";
            // 
            // Calc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(146, 99);
            this.Controls.Add(this.TXT_output);
            this.Controls.Add(this.BUT_tofeet);
            this.Controls.Add(this.BUT_tometers);
            this.Controls.Add(this.TXT_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Calc";
            this.Text = "Calc";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_input;
        private MyButton BUT_tometers;
        private MyButton BUT_tofeet;
        private System.Windows.Forms.TextBox TXT_output;
    }
}