namespace SpireAnalytics
{
    partial class frmMenu
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
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.btnViewRunCorrelations = new System.Windows.Forms.Button();
            this.btnViewRunAverages = new System.Windows.Forms.Button();
            this.btnImportData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnViewWebReport = new System.Windows.Forms.Button();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.btnViewWebReport);
            this.pnlOptions.Controls.Add(this.btnViewRunCorrelations);
            this.pnlOptions.Controls.Add(this.btnViewRunAverages);
            this.pnlOptions.Controls.Add(this.btnImportData);
            this.pnlOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlOptions.Location = new System.Drawing.Point(55, 141);
            this.pnlOptions.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(529, 477);
            this.pnlOptions.TabIndex = 0;
            // 
            // btnViewRunCorrelations
            // 
            this.btnViewRunCorrelations.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnViewRunCorrelations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRunCorrelations.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.btnViewRunCorrelations, "Click to view the run correlations form.");
            this.btnViewRunCorrelations.Location = new System.Drawing.Point(106, 270);
            this.btnViewRunCorrelations.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnViewRunCorrelations.Name = "btnViewRunCorrelations";
            this.helpProvider1.SetShowHelp(this.btnViewRunCorrelations, true);
            this.btnViewRunCorrelations.Size = new System.Drawing.Size(316, 61);
            this.btnViewRunCorrelations.TabIndex = 2;
            this.btnViewRunCorrelations.Text = "View Run Correlations";
            this.btnViewRunCorrelations.UseVisualStyleBackColor = false;
            this.btnViewRunCorrelations.Click += new System.EventHandler(this.btnViewRunCorrelations_Click);
            // 
            // btnViewRunAverages
            // 
            this.btnViewRunAverages.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnViewRunAverages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRunAverages.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.btnViewRunAverages, "Click to view the run averages form.");
            this.btnViewRunAverages.Location = new System.Drawing.Point(106, 165);
            this.btnViewRunAverages.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnViewRunAverages.Name = "btnViewRunAverages";
            this.helpProvider1.SetShowHelp(this.btnViewRunAverages, true);
            this.btnViewRunAverages.Size = new System.Drawing.Size(316, 64);
            this.btnViewRunAverages.TabIndex = 1;
            this.btnViewRunAverages.Text = "View Run Averages";
            this.btnViewRunAverages.UseVisualStyleBackColor = false;
            this.btnViewRunAverages.Click += new System.EventHandler(this.btnAvgRuns_Click);
            // 
            // btnImportData
            // 
            this.btnImportData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnImportData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportData.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.btnImportData, "Click to import data from the CSV file.");
            this.btnImportData.Location = new System.Drawing.Point(106, 56);
            this.btnImportData.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnImportData.Name = "btnImportData";
            this.helpProvider1.SetShowHelp(this.btnImportData, true);
            this.btnImportData.Size = new System.Drawing.Size(316, 68);
            this.btnImportData.TabIndex = 0;
            this.btnImportData.Text = "Import Data";
            this.btnImportData.UseVisualStyleBackColor = false;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 22);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(6);
            this.label1.Size = new System.Drawing.Size(385, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "Slay the Spire Analysis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 86);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(150, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "Thomas Hatt";
            // 
            // btnViewWebReport
            // 
            this.btnViewWebReport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnViewWebReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewWebReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.btnViewWebReport, "Click to view the run correlations form.");
            this.btnViewWebReport.Location = new System.Drawing.Point(106, 375);
            this.btnViewWebReport.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnViewWebReport.Name = "btnViewWebReport";
            this.helpProvider1.SetShowHelp(this.btnViewWebReport, true);
            this.btnViewWebReport.Size = new System.Drawing.Size(316, 61);
            this.btnViewWebReport.TabIndex = 3;
            this.btnViewWebReport.Text = "View Web Report";
            this.btnViewWebReport.UseVisualStyleBackColor = false;
            this.btnViewWebReport.Click += new System.EventHandler(this.btnViewWebReport_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(640, 648);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlOptions);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMenu";
            this.Text = "Slay the Spire Analysis | Menu";
            this.pnlOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.Button btnViewRunAverages;
        private System.Windows.Forms.Button btnViewRunCorrelations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnViewWebReport;
    }
}

