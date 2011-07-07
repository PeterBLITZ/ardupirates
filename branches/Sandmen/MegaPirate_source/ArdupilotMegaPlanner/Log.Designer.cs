namespace ArdupilotMega
{
    partial class Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log));
            this.TXT_seriallog = new System.Windows.Forms.TextBox();
            this.BUT_DLall = new ArdupilotMega.MyButton();
            this.BUT_DLthese = new ArdupilotMega.MyButton();
            this.BUT_clearlogs = new ArdupilotMega.MyButton();
            this.CHK_logs = new System.Windows.Forms.CheckedListBox();
            this.TXT_status = new System.Windows.Forms.TextBox();
            this.BUT_redokml = new ArdupilotMega.MyButton();
            this.BUT_firstperson = new ArdupilotMega.MyButton();
            this.SuspendLayout();
            // 
            // TXT_seriallog
            // 
            this.TXT_seriallog.Location = new System.Drawing.Point(135, 13);
            this.TXT_seriallog.Multiline = true;
            this.TXT_seriallog.Name = "TXT_seriallog";
            this.TXT_seriallog.Size = new System.Drawing.Size(470, 321);
            this.TXT_seriallog.TabIndex = 1;
            // 
            // BUT_DLall
            // 
            this.BUT_DLall.Location = new System.Drawing.Point(13, 13);
            this.BUT_DLall.Name = "BUT_DLall";
            this.BUT_DLall.Size = new System.Drawing.Size(116, 23);
            this.BUT_DLall.TabIndex = 3;
            this.BUT_DLall.Text = "Download All Logs";
            this.BUT_DLall.UseVisualStyleBackColor = true;
            this.BUT_DLall.Click += new System.EventHandler(this.BUT_DLall_Click);
            // 
            // BUT_DLthese
            // 
            this.BUT_DLthese.Location = new System.Drawing.Point(13, 43);
            this.BUT_DLthese.Name = "BUT_DLthese";
            this.BUT_DLthese.Size = new System.Drawing.Size(116, 23);
            this.BUT_DLthese.TabIndex = 4;
            this.BUT_DLthese.Text = "Download These Log";
            this.BUT_DLthese.UseVisualStyleBackColor = true;
            this.BUT_DLthese.Click += new System.EventHandler(this.BUT_DLthese_Click);
            // 
            // BUT_clearlogs
            // 
            this.BUT_clearlogs.Location = new System.Drawing.Point(13, 187);
            this.BUT_clearlogs.Name = "BUT_clearlogs";
            this.BUT_clearlogs.Size = new System.Drawing.Size(116, 23);
            this.BUT_clearlogs.TabIndex = 5;
            this.BUT_clearlogs.Text = "Clear Logs";
            this.BUT_clearlogs.UseVisualStyleBackColor = true;
            this.BUT_clearlogs.Click += new System.EventHandler(this.BUT_clearlogs_Click);
            // 
            // CHK_logs
            // 
            this.CHK_logs.CheckOnClick = true;
            this.CHK_logs.FormattingEnabled = true;
            this.CHK_logs.Location = new System.Drawing.Point(13, 72);
            this.CHK_logs.Name = "CHK_logs";
            this.CHK_logs.Size = new System.Drawing.Size(116, 109);
            this.CHK_logs.TabIndex = 6;
            this.CHK_logs.Click += new System.EventHandler(this.CHK_logs_Click);
            // 
            // TXT_status
            // 
            this.TXT_status.Enabled = false;
            this.TXT_status.Location = new System.Drawing.Point(13, 217);
            this.TXT_status.Name = "TXT_status";
            this.TXT_status.Size = new System.Drawing.Size(116, 20);
            this.TXT_status.TabIndex = 7;
            // 
            // BUT_redokml
            // 
            this.BUT_redokml.Location = new System.Drawing.Point(13, 311);
            this.BUT_redokml.Name = "BUT_redokml";
            this.BUT_redokml.Size = new System.Drawing.Size(116, 23);
            this.BUT_redokml.TabIndex = 8;
            this.BUT_redokml.Text = "Recreate KML";
            this.BUT_redokml.UseVisualStyleBackColor = true;
            this.BUT_redokml.Click += new System.EventHandler(this.BUT_redokml_Click);
            // 
            // BUT_firstperson
            // 
            this.BUT_firstperson.Location = new System.Drawing.Point(13, 282);
            this.BUT_firstperson.Name = "BUT_firstperson";
            this.BUT_firstperson.Size = new System.Drawing.Size(116, 23);
            this.BUT_firstperson.TabIndex = 9;
            this.BUT_firstperson.Text = "First Person KML";
            this.BUT_firstperson.UseVisualStyleBackColor = true;
            this.BUT_firstperson.Click += new System.EventHandler(this.BUT_firstperson_Click);
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 349);
            this.Controls.Add(this.BUT_firstperson);
            this.Controls.Add(this.BUT_redokml);
            this.Controls.Add(this.TXT_status);
            this.Controls.Add(this.CHK_logs);
            this.Controls.Add(this.BUT_clearlogs);
            this.Controls.Add(this.BUT_DLthese);
            this.Controls.Add(this.BUT_DLall);
            this.Controls.Add(this.TXT_seriallog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Log";
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.Load += new System.EventHandler(this.Log_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyButton BUT_DLall;
        private MyButton BUT_DLthese;
        private MyButton BUT_clearlogs;
        private System.Windows.Forms.CheckedListBox CHK_logs;
        private System.Windows.Forms.TextBox TXT_status;
        private MyButton BUT_redokml;
        private System.Windows.Forms.TextBox TXT_seriallog;
        private MyButton BUT_firstperson;
    }
}