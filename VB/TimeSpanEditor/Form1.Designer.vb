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
			Me.timeSpanEdit1 = New DevExpress.XtraEditors.TimeSpanEdit()
			CType(Me.timeSpanEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' timeSpanEdit1
			' 
			Me.timeSpanEdit1.EditValue = System.TimeSpan.Parse("00:00:00")
			Me.timeSpanEdit1.Location = New System.Drawing.Point(52, 39)
			Me.timeSpanEdit1.Name = "timeSpanEdit1"
			Me.timeSpanEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.timeSpanEdit1.Properties.Mask.EditMask = "hh:mm"
			Me.timeSpanEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.TimeSpan
			Me.timeSpanEdit1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.SpinButtons
			Me.timeSpanEdit1.Size = New System.Drawing.Size(191, 20)
			Me.timeSpanEdit1.TabIndex = 0
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(311, 309)
			Me.Controls.Add(Me.timeSpanEdit1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.timeSpanEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub







		#End Region

		Private timeSpanEdit1 As DevExpress.XtraEditors.TimeSpanEdit
	End Class
End Namespace

