Imports Microsoft.VisualBasic
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
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			cbeExportMode.Properties.Items.AddRange(New ExportMode() { ExportMode.Default, ExportMode.DisplayText, ExportMode.Value })
			cbeExportMode.EditValue = ExportMode.Default
		End Sub

		Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
			Dim text As String = ""
			If TypeOf timeSpanEdit.EditValue Is TimeSpan Then
				text = "TimeSpan:  "
			End If
			If TypeOf timeSpanEdit.EditValue Is String Then
				text = "String:  "
			End If
			Me.Text = text & timeSpanEdit.EditValue.ToString()
		End Sub

		Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkEditAllowDayInput.CheckedChanged
			timeSpanEdit.Properties.AllowDayInput = checkEditAllowDayInput.Checked
		End Sub

		Private Sub cbeExportMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbeExportMode.SelectedIndexChanged
			timeSpanEdit.Properties.ExportMode = CType(cbeExportMode.EditValue, ExportMode)
		End Sub

	End Class
End Namespace