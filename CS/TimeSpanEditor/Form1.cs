using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors.Repository;

namespace TimeSpanEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbeExportMode.Properties.Items.AddRange(new ExportMode[] { ExportMode.Default, ExportMode.DisplayText, ExportMode.Value });
            cbeExportMode.EditValue = ExportMode.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string text = "";
            if (timeSpanEdit.EditValue is TimeSpan)
                text = "TimeSpan:  ";
            if (timeSpanEdit.EditValue is string)
                text = "String:  ";
            this.Text = text + timeSpanEdit.EditValue.ToString();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            timeSpanEdit.Properties.AllowDayInput = checkEditAllowDayInput.Checked; 
        }

        private void cbeExportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            timeSpanEdit.Properties.ExportMode = (ExportMode)cbeExportMode.EditValue;
        }

    }
}