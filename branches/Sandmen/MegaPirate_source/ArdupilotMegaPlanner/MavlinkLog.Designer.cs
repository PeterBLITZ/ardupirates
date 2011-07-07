namespace ArdupilotMega
{
    partial class MavlinkLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MavlinkLog));
            this.BUT_redokml = new ArdupilotMega.MyButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // BUT_redokml
            // 
            this.BUT_redokml.Location = new System.Drawing.Point(25, 12);
            this.BUT_redokml.Name = "BUT_redokml";
            this.BUT_redokml.Size = new System.Drawing.Size(116, 23);
            this.BUT_redokml.TabIndex = 8;
            this.BUT_redokml.Text = "create KML";
            this.BUT_redokml.UseVisualStyleBackColor = true;
            this.BUT_redokml.Click += new System.EventHandler(this.BUT_redokml_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(147, 26);
            this.progressBar1.TabIndex = 9;
            // 
            // MavlinkLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 84);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BUT_redokml);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MavlinkLog";
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MyButton BUT_redokml;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}