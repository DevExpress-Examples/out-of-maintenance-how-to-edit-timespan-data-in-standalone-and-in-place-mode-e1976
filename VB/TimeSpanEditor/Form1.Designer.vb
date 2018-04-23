Imports Microsoft.VisualBasic
Imports System
Namespace TimeSpanEditor
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.timer1 = New System.Windows.Forms.Timer(Me.components)
			Me.checkEditAllowDayInput = New DevExpress.XtraEditors.CheckEdit()
			Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
			Me.cbeExportMode = New DevExpress.XtraEditors.ComboBoxEdit()
			Me.timeSpanEdit = New TimeSpanEditor.TimeSpanEdit()
			CType(Me.checkEditAllowDayInput.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.cbeExportMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.timeSpanEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' timer1
			' 
			Me.timer1.Enabled = True
'			Me.timer1.Tick += New System.EventHandler(Me.timer1_Tick);
			' 
			' checkEditAllowDayInput
			' 
			Me.checkEditAllowDayInput.Location = New System.Drawing.Point(12, 69)
			Me.checkEditAllowDayInput.Name = "checkEditAllowDayInput"
			Me.checkEditAllowDayInput.Properties.Caption = "Allow Day Input"
			Me.checkEditAllowDayInput.Size = New System.Drawing.Size(113, 19)
			Me.checkEditAllowDayInput.TabIndex = 3
'			Me.checkEditAllowDayInput.CheckedChanged += New System.EventHandler(Me.checkEdit1_CheckedChanged);
			' 
			' labelControl1
			' 
			Me.labelControl1.Location = New System.Drawing.Point(167, 71)
			Me.labelControl1.Name = "labelControl1"
			Me.labelControl1.Size = New System.Drawing.Size(65, 13)
			Me.labelControl1.TabIndex = 4
			Me.labelControl1.Text = "Export Mode:"
			' 
			' cbeExportMode
			' 
			Me.cbeExportMode.Location = New System.Drawing.Point(167, 91)
			Me.cbeExportMode.Name = "cbeExportMode"
			Me.cbeExportMode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.cbeExportMode.Size = New System.Drawing.Size(132, 20)
			Me.cbeExportMode.TabIndex = 5
'			Me.cbeExportMode.SelectedIndexChanged += New System.EventHandler(Me.cbeExportMode_SelectedIndexChanged);
			' 
			' timeSpanEdit
			' 
			Me.timeSpanEdit.EditValue = System.TimeSpan.Parse("00:00:00")
			Me.timeSpanEdit.Location = New System.Drawing.Point(89, 12)
			Me.timeSpanEdit.Name = "timeSpanEdit"
			Me.timeSpanEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.timeSpanEdit.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
			Me.timeSpanEdit.Properties.DisplayFormat.FormatString = "HH:mm:ss"
			Me.timeSpanEdit.Properties.EditFormat.FormatString = "HH:mm:ss"
			Me.timeSpanEdit.Properties.Mask.EditMask = "HH:mm:ss"
			Me.timeSpanEdit.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default
			Me.timeSpanEdit.Size = New System.Drawing.Size(126, 20)
			Me.timeSpanEdit.TabIndex = 2
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(311, 309)
			Me.Controls.Add(Me.cbeExportMode)
			Me.Controls.Add(Me.labelControl1)
			Me.Controls.Add(Me.checkEditAllowDayInput)
			Me.Controls.Add(Me.timeSpanEdit)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.checkEditAllowDayInput.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.cbeExportMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.timeSpanEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents timer1 As System.Windows.Forms.Timer
		Private timeSpanEdit As TimeSpanEdit
		Private WithEvents checkEditAllowDayInput As DevExpress.XtraEditors.CheckEdit
		Private labelControl1 As DevExpress.XtraEditors.LabelControl
		Private WithEvents cbeExportMode As DevExpress.XtraEditors.ComboBoxEdit





	End Class
End Namespace

