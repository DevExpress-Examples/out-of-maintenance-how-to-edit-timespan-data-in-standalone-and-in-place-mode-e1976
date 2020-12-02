Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports DevExpress.XtraEditors.Repository

Namespace TimeSpanEditor
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()

			timeSpanEdit1.Properties.Mask.EditMask = "hh:mm"
			timeSpanEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.TimeSpan
		End Sub


	End Class
End Namespace