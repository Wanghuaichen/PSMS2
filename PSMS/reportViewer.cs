﻿using MetroFramework.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PSMS
{
    public partial class reportViewer : MetroForm
    {
        public reportViewer()
        {
            InitializeComponent();
        }
        
        public reportViewer(List<Class.reportInvoice> reportInvoice,float discount)
        {
            InitializeComponent();
            Class.FullMode.Fullscreen(this);

            invoiceReportDoc.SetDataSource(reportInvoice);
            invoiceReportDoc.SetParameterValue(0, discount);

            crystalReportViewer1.ReportSource = invoiceReportDoc;
        }

        public reportViewer(List<Class.reportEmployee> reportEmployee)
        {
            InitializeComponent();
            Class.FullMode.Fullscreen(this);

            employeeReportDoc.SetDataSource(reportEmployee);
            crystalReportViewer1.ReportSource = employeeReportDoc;
        }

        public reportViewer(List<Class.reportCustomer> reportCustomer)
        {
            InitializeComponent();
            Class.FullMode.Fullscreen(this);

            CustomerReportDoc.SetDataSource(reportCustomer);
            crystalReportViewer1.ReportSource = CustomerReportDoc;
        }

        private void loadReport()
        {
            //invoiceReportDoc.SetDataSource(reportCustomer);
        }

        private void reportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
