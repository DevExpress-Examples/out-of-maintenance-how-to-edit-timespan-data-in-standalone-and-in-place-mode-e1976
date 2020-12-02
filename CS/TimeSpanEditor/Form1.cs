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

            timeSpanEdit1.Properties.Mask.EditMask = "hh:mm";
            timeSpanEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.TimeSpan;
        }

       
    }
}