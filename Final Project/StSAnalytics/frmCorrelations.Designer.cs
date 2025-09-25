namespace SpireAnalytics
{
    partial class frmCorrelations
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.lblRunsAnalyzed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrSystemRuns = new System.Windows.Forms.Timer(this.components);
            this.chartCharacterFloorCorrelations = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.chartAnalyzeGoldFloorCorrelations = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnViewNextCharts = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chartAscensionCorrelation = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRelicsCorrelation = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblDisplayingChart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartCharacterFloorCorrelations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyzeGoldFloorCorrelations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAscensionCorrelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRelicsCorrelation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRunsAnalyzed
            // 
            this.lblRunsAnalyzed.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunsAnalyzed.Location = new System.Drawing.Point(562, 175);
            this.lblRunsAnalyzed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRunsAnalyzed.Name = "lblRunsAnalyzed";
            this.lblRunsAnalyzed.Size = new System.Drawing.Size(137, 25);
            this.lblRunsAnalyzed.TabIndex = 4;
            this.lblRunsAnalyzed.Text = "0";
            this.lblRunsAnalyzed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(596, 200);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Runs Analyzed";
            // 
            // tmrSystemRuns
            // 
            this.tmrSystemRuns.Tick += new System.EventHandler(this.tmrSystemRuns_Tick);
            // 
            // chartCharacterFloorCorrelations
            // 
            this.chartCharacterFloorCorrelations.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chartCharacterFloorCorrelations.BorderlineColor = System.Drawing.Color.Black;
            this.chartCharacterFloorCorrelations.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartCharacterFloorCorrelations.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chartCharacterFloorCorrelations.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Font = new System.Drawing.Font("Arial", 8F);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.Title = "Character";
            legend1.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DoubleLine;
            this.chartCharacterFloorCorrelations.Legends.Add(legend1);
            this.chartCharacterFloorCorrelations.Location = new System.Drawing.Point(11, 245);
            this.chartCharacterFloorCorrelations.Name = "chartCharacterFloorCorrelations";
            series1.BackSecondaryColor = System.Drawing.Color.White;
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series1.Legend = "Legend1";
            series1.Name = "Data";
            series1.ShadowColor = System.Drawing.Color.Silver;
            series1.ShadowOffset = 1;
            this.chartCharacterFloorCorrelations.Series.Add(series1);
            this.chartCharacterFloorCorrelations.Size = new System.Drawing.Size(649, 388);
            this.chartCharacterFloorCorrelations.TabIndex = 6;
            this.chartCharacterFloorCorrelations.Text = "Character Choice vs Floor Reached Correlations";
            title1.BackColor = System.Drawing.Color.White;
            title1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Character Choice vs Floor Reached Correlations";
            this.chartCharacterFloorCorrelations.Titles.Add(title1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(442, 26);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(6);
            this.label3.Size = new System.Drawing.Size(454, 51);
            this.label3.TabIndex = 7;
            this.label3.Text = "Slay the Spire: Correlations";
            // 
            // chartAnalyzeGoldFloorCorrelations
            // 
            this.chartAnalyzeGoldFloorCorrelations.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chartAnalyzeGoldFloorCorrelations.BorderlineColor = System.Drawing.Color.Black;
            this.chartAnalyzeGoldFloorCorrelations.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartAnalyzeGoldFloorCorrelations.BorderlineWidth = 2;
            chartArea2.Name = "ChartArea1";
            this.chartAnalyzeGoldFloorCorrelations.ChartAreas.Add(chartArea2);
            this.chartAnalyzeGoldFloorCorrelations.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            legend2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.BottomRight;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend2.Font = new System.Drawing.Font("Arial", 8F);
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            legend2.Title = "Floor Reached";
            legend2.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DoubleLine;
            this.chartAnalyzeGoldFloorCorrelations.Legends.Add(legend2);
            this.chartAnalyzeGoldFloorCorrelations.Location = new System.Drawing.Point(679, 245);
            this.chartAnalyzeGoldFloorCorrelations.Name = "chartAnalyzeGoldFloorCorrelations";
            this.chartAnalyzeGoldFloorCorrelations.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.LabelBackColor = System.Drawing.Color.White;
            series2.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series2.LabelBorderWidth = 5;
            series2.Legend = "Legend1";
            series2.LegendText = "0";
            series2.Name = "Data";
            series2.YValuesPerPoint = 2;
            this.chartAnalyzeGoldFloorCorrelations.Series.Add(series2);
            this.chartAnalyzeGoldFloorCorrelations.Size = new System.Drawing.Size(649, 388);
            this.chartAnalyzeGoldFloorCorrelations.TabIndex = 8;
            this.chartAnalyzeGoldFloorCorrelations.Text = "Average Floor Reached";
            title2.BackColor = System.Drawing.Color.White;
            title2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Gold Earned vs Floor Reached Correlations";
            this.chartAnalyzeGoldFloorCorrelations.Titles.Add(title2);
            // 
            // btnViewNextCharts
            // 
            this.btnViewNextCharts.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnViewNextCharts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewNextCharts.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewNextCharts.Location = new System.Drawing.Point(12, 668);
            this.btnViewNextCharts.Name = "btnViewNextCharts";
            this.btnViewNextCharts.Size = new System.Drawing.Size(649, 74);
            this.btnViewNextCharts.TabIndex = 0;
            this.btnViewNextCharts.Text = "View Next Series of Charts";
            this.btnViewNextCharts.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(680, 668);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(649, 74);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close Form";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chartAscensionCorrelation
            // 
            this.chartAscensionCorrelation.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chartAscensionCorrelation.BorderlineColor = System.Drawing.Color.Black;
            this.chartAscensionCorrelation.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartAscensionCorrelation.BorderlineWidth = 2;
            chartArea3.Name = "ChartArea1";
            this.chartAscensionCorrelation.ChartAreas.Add(chartArea3);
            legend3.DockedToChartArea = "ChartArea1";
            legend3.Font = new System.Drawing.Font("Arial", 8F);
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            legend3.Title = "Ascension Level";
            legend3.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DoubleLine;
            this.chartAscensionCorrelation.Legends.Add(legend3);
            this.chartAscensionCorrelation.Location = new System.Drawing.Point(12, 245);
            this.chartAscensionCorrelation.Name = "chartAscensionCorrelation";
            series3.ChartArea = "ChartArea1";
            series3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.LabelBackColor = System.Drawing.Color.White;
            series3.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series3.Legend = "Legend1";
            series3.Name = "Data";
            this.chartAscensionCorrelation.Series.Add(series3);
            this.chartAscensionCorrelation.Size = new System.Drawing.Size(648, 388);
            this.chartAscensionCorrelation.TabIndex = 13;
            this.chartAscensionCorrelation.Text = "Average Ascension";
            title3.BackColor = System.Drawing.Color.White;
            title3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "Ascension Level Distribution";
            this.chartAscensionCorrelation.Titles.Add(title3);
            // 
            // chartRelicsCorrelation
            // 
            this.chartRelicsCorrelation.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chartRelicsCorrelation.BorderlineColor = System.Drawing.Color.Black;
            this.chartRelicsCorrelation.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartRelicsCorrelation.BorderlineWidth = 2;
            chartArea4.Name = "ChartArea1";
            this.chartRelicsCorrelation.ChartAreas.Add(chartArea4);
            legend4.DockedToChartArea = "ChartArea1";
            legend4.Font = new System.Drawing.Font("Arial", 8F);
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend1";
            legend4.Title = "Relic Count";
            legend4.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DoubleLine;
            this.chartRelicsCorrelation.Legends.Add(legend4);
            this.chartRelicsCorrelation.Location = new System.Drawing.Point(12, 245);
            this.chartRelicsCorrelation.Name = "chartRelicsCorrelation";
            this.chartRelicsCorrelation.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series4.ChartArea = "ChartArea1";
            series4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.LabelBackColor = System.Drawing.Color.White;
            series4.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series4.Legend = "Legend1";
            series4.Name = "Data";
            this.chartRelicsCorrelation.Series.Add(series4);
            this.chartRelicsCorrelation.Size = new System.Drawing.Size(648, 388);
            this.chartRelicsCorrelation.TabIndex = 14;
            this.chartRelicsCorrelation.Text = "Average Relic Amount";
            title4.BackColor = System.Drawing.Color.White;
            title4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "Title1";
            title4.Text = "Relic Amount Distribution";
            this.chartRelicsCorrelation.Titles.Add(title4);
            // 
            // lblDisplayingChart
            // 
            this.lblDisplayingChart.AutoSize = true;
            this.lblDisplayingChart.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDisplayingChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayingChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDisplayingChart.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.lblDisplayingChart.Location = new System.Drawing.Point(505, 98);
            this.lblDisplayingChart.Name = "lblDisplayingChart";
            this.lblDisplayingChart.Padding = new System.Windows.Forms.Padding(6);
            this.lblDisplayingChart.Size = new System.Drawing.Size(328, 46);
            this.lblDisplayingChart.TabIndex = 15;
            this.lblDisplayingChart.Text = "Displaying Chart 1 of 3";
            this.lblDisplayingChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCorrelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1338, 771);
            this.Controls.Add(this.lblDisplayingChart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnViewNextCharts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chartCharacterFloorCorrelations);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRunsAnalyzed);
            this.Controls.Add(this.chartAnalyzeGoldFloorCorrelations);
            this.Controls.Add(this.chartAscensionCorrelation);
            this.Controls.Add(this.chartRelicsCorrelation);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1354, 810);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1354, 810);
            this.Name = "frmCorrelations";
            this.Text = "Slay the Spire Analysis | Correlations";
            this.Load += new System.EventHandler(this.frmCorrelations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartCharacterFloorCorrelations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyzeGoldFloorCorrelations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAscensionCorrelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRelicsCorrelation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRunsAnalyzed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrSystemRuns;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCharacterFloorCorrelations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAnalyzeGoldFloorCorrelations;
        private System.Windows.Forms.Button btnViewNextCharts;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAscensionCorrelation;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRelicsCorrelation;
        private System.Windows.Forms.Label lblDisplayingChart;
    }
}