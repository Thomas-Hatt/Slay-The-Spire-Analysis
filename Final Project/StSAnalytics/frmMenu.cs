using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoAnalytics;

namespace SpireAnalytics
{
    public partial class frmMenu : Form
    {

        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            // Create a new instance of frmImport
            frmImport ImportForm = new frmImport();

            // Display the form
            ImportForm.ShowDialog();
        }

        private void btnAvgRuns_Click(object sender, EventArgs e)
        {
            // Create a new instance of frmAvgRun
            frmAvgRun AvgRunForm = new frmAvgRun();

            // Display the form
            AvgRunForm.ShowDialog();
        }

        private void btnViewRunCorrelations_Click(object sender, EventArgs e)
        {
            // Create a new instance of frmCorrelations
            frmCorrelations CorrelationForm = new frmCorrelations();

            // Display the form
            CorrelationForm.ShowDialog();
        }

        private void btnViewWebReport_Click(object sender, EventArgs e)
        {
            // Create a new instance of frmWebReport
            frmWebReport WebReport = new frmWebReport();

            // Display the form
            WebReport.ShowDialog();
        }
    }
}
