using SpireAnalytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoAnalytics
{
    public partial class frmWebReport : Form
    {
        public frmWebReport()
        {
            InitializeComponent();
            SpireListReport();
        }


        /// <summary>
        /// Upon loading the form, the HTML is created for web browser. The SQL is then queried to get all of the runs from the database. 
        /// All of the runs are then displayed into the table.
        /// </summary>
        public void SpireListReport()
        {
            // Header
            string strReportHeader;
            strReportHeader = "<HTML><HEAD><TITLE>Slay the Spire Analysis | Web Report</TITLE></HEAD>";

            // Body
            string strReportBody = "<BODY>";
            strReportBody += "<H1>Spire Report</H1>";
            strReportBody += "<H1>Sorted by Gold obtained</H1>";
            strReportBody += "<hr/>";


            // Create a table to display content
            strReportBody += "<table>";
            strReportBody += "<tr>";

            strReportBody += "<td><strong>Gold</strong></td>";
            strReportBody += "<td><strong>Relics</strong></td>";
            strReportBody += "<td><strong>Score</strong></td>";
            strReportBody += "<td><strong>Floor Reached</strong></td>";
            strReportBody += "<td><strong>Ascension Level</strong></td>";

            strReportBody += "</tr>";

            // Create instance of the class
            clsData myData = new clsData();

            // Send an SQL command to the class
            myData.SQL = "SELECT Gold, Relics, Score, FloorReached, AscensionLevel FROM tblRuns ORDER BY Gold DESC";

            // Loop through the datatable to get values
            for (int i = 0; i < myData.Dt.Rows.Count; i++)
            {
                // Create a table row
                strReportBody += "<tr>";

                // Display Gold Obtained
                strReportBody += "<td>" + myData.Dt.Rows[i]["Gold"].ToString() + "</td>";

                // Display Relics Obtained
                strReportBody += "<td>" + myData.Dt.Rows[i]["Relics"].ToString() + "</td>";

                // Display Score Obtained
                strReportBody += "<td>" + myData.Dt.Rows[i]["Score"].ToString() + "</td>";

                // Display Floor Reached
                strReportBody += "<td>" + myData.Dt.Rows[i]["FloorReached"].ToString() + "</td>";

                // Display Ascension Level
                strReportBody += "<td>" + myData.Dt.Rows[i]["AscensionLevel"].ToString() + "</td>";

                // Close the table row
                strReportBody += "</tr>";
            }


            // Close the table
            strReportBody += "</table>";


            // Close the report
            strReportBody += "</body></html>";


            // Display report in browser control
            wbReport.DocumentText = strReportHeader + strReportBody;
        }
    }
}
