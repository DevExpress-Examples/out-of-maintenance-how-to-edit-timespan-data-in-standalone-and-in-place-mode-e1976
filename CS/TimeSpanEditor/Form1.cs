using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TimeSpanEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TimeSpanEdit timeSpanEdit = new TimeSpanEdit();
        private void Form1_Load(object sender, EventArgs e)
        {
            Controls.Add(timeSpanEdit);            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            timeSpanEdit.Properties.AllowDayInput = !timeSpanEdit.Properties.AllowDayInput; 
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (timeSpanEdit.Properties.ExportMode == DevExpress.XtraEditors.Repository.ExportMode.DisplayText)
                timeSpanEdit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            else
                timeSpanEdit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText; 
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

    }
}