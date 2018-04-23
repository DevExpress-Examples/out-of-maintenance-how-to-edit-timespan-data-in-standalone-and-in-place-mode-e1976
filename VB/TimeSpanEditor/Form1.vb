Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections

Namespace TimeSpanEditor
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private timeSpanEdit As New TimeSpanEdit()
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Controls.Add(timeSpanEdit)
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			timeSpanEdit.Properties.AllowDayInput = Not timeSpanEdit.Properties.AllowDayInput
		End Sub

		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			If timeSpanEdit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText Then
				timeSpanEdit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default
			Else
				timeSpanEdit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText
			End If
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

	End Class
End Namespace