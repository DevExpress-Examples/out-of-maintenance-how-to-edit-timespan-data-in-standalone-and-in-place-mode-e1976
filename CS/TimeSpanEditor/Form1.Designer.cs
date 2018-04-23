namespace TimeSpanEditor
{
    partial class Form1
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkEditAllowDayInput = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbeExportMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.timeSpanEdit = new TimeSpanEditor.TimeSpanEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAllowDayInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeExportMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkEditAllowDayInput
            // 
            this.checkEditAllowDayInput.Location = new System.Drawing.Point(12, 69);
            this.checkEditAllowDayInput.Name = "checkEditAllowDayInput";
            this.checkEditAllowDayInput.Properties.Caption = "Allow Day Input";
            this.checkEditAllowDayInput.Size = new System.Drawing.Size(113, 19);
            this.checkEditAllowDayInput.TabIndex = 3;
            this.checkEditAllowDayInput.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(167, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Export Mode:";
            // 
            // cbeExportMode
            // 
            this.cbeExportMode.Location = new System.Drawing.Point(167, 91);
            this.cbeExportMode.Name = "cbeExportMode";
            this.cbeExportMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeExportMode.Size = new System.Drawing.Size(132, 20);
            this.cbeExportMode.TabIndex = 5;
            this.cbeExportMode.SelectedIndexChanged += new System.EventHandler(this.cbeExportMode_SelectedIndexChanged);
            // 
            // timeSpanEdit
            // 
            this.timeSpanEdit.EditValue = System.TimeSpan.Parse("00:00:00");
            this.timeSpanEdit.Location = new System.Drawing.Point(89, 12);
            this.timeSpanEdit.Name = "timeSpanEdit";
            this.timeSpanEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeSpanEdit.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.timeSpanEdit.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.timeSpanEdit.Properties.EditFormat.FormatString = "HH:mm:ss";
            this.timeSpanEdit.Properties.Mask.EditMask = "HH:mm:ss";
            this.timeSpanEdit.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.timeSpanEdit.Size = new System.Drawing.Size(126, 20);
            this.timeSpanEdit.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 309);
            this.Controls.Add(this.cbeExportMode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.checkEditAllowDayInput);
            this.Controls.Add(this.timeSpanEdit);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAllowDayInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeExportMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private TimeSpanEdit timeSpanEdit;
        private DevExpress.XtraEditors.CheckEdit checkEditAllowDayInput;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbeExportMode;





    }
}

