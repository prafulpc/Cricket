using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cricket.View
{
    public partial class BestBowlerReportForm : Form
    {
        public BestBowlerReportForm()
        {
            InitializeComponent();
        }

        private void BestBowlerReportForm_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public ReportViewer GetReportViewer()
        {
            return this.reportViewer1;
        }
    }
}
