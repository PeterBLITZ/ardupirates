namespace ArdupilotMega
{
    partial class LogBrowse
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogBrowse));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.Graphit = new ArdupilotMega.MyButton();
            this.BUT_cleargraph = new ArdupilotMega.MyButton();
            this.BUT_loadlog = new ArdupilotMega.MyButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 301);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 100;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(749, 439);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // zg1
            // 
            this.zg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zg1.Location = new System.Drawing.Point(13, 12);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            this.zg1.Size = new System.Drawing.Size(749, 252);
            this.zg1.TabIndex = 30;
            // 
            // Graphit
            // 
            this.Graphit.Location = new System.Drawing.Point(13, 270);
            this.Graphit.Name = "Graphit";
            this.Graphit.Size = new System.Drawing.Size(91, 23);
            this.Graphit.TabIndex = 31;
            this.Graphit.Text = "Graph this data";
            this.toolTip1.SetToolTip(this.Graphit, "Graphs the current highlighted cell");
            this.Graphit.UseVisualStyleBackColor = true;
            this.Graphit.Click += new System.EventHandler(this.Graphit_Click);
            // 
            // BUT_cleargraph
            // 
            this.BUT_cleargraph.Location = new System.Drawing.Point(110, 270);
            this.BUT_cleargraph.Name = "BUT_cleargraph";
            this.BUT_cleargraph.Size = new System.Drawing.Size(75, 23);
            this.BUT_cleargraph.TabIndex = 32;
            this.BUT_cleargraph.Text = "Clear Graph";
            this.toolTip1.SetToolTip(this.BUT_cleargraph, "Clear all graph data");
            this.BUT_cleargraph.UseVisualStyleBackColor = true;
            this.BUT_cleargraph.Click += new System.EventHandler(this.BUT_cleargraph_Click);
            // 
            // BUT_loadlog
            // 
            this.BUT_loadlog.Location = new System.Drawing.Point(192, 271);
            this.BUT_loadlog.Name = "BUT_loadlog";
            this.BUT_loadlog.Size = new System.Drawing.Size(75, 23);
            this.BUT_loadlog.TabIndex = 33;
            this.BUT_loadlog.Text = "Load A Log";
            this.toolTip1.SetToolTip(this.BUT_loadlog, "Load a diffrent log file");
            this.BUT_loadlog.UseVisualStyleBackColor = true;
            this.BUT_loadlog.Click += new System.EventHandler(this.BUT_loadlog_Click);
            // 
            // LogBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 752);
            this.Controls.Add(this.BUT_loadlog);
            this.Controls.Add(this.BUT_cleargraph);
            this.Controls.Add(this.Graphit);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogBrowse";
            this.Text = "Log Browse";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private ZedGraph.ZedGraphControl zg1;
        private MyButton Graphit;
        private MyButton BUT_cleargraph;
        private MyButton BUT_loadlog;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

