Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors
Imports System.ComponentModel
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils
Imports DevExpress.Data.Mask
Imports System.Globalization
Imports DevExpress.XtraEditors.Mask

Namespace TimeSpanEditor
	<UserRepositoryItem("RegisterTimeSpanEdit")> _
	Public Class RepositoryItemTimeSpanEdit
		Inherits RepositoryItemTimeEdit
		Private allowDayInput_Renamed As Boolean
		Shared Sub New()
			RegisterTimeSpanEdit()
		End Sub
		Public Sub New()
			allowDayInput_Renamed = False
			UpdateFormats()
		End Sub
		Public Const TimeSpanEditName As String = "TimeSpanEdit"
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return TimeSpanEditName
			End Get
		End Property
		Public Shared Sub RegisterTimeSpanEdit()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(TimeSpanEditName, GetType(TimeSpanEdit), GetType(RepositoryItemTimeSpanEdit), GetType(BaseSpinEditViewInfo), New ButtonEditPainter(), True))
		End Sub
		<Browsable(False)> _
		Public Overrides ReadOnly Property EditFormat() As FormatInfo
			Get
				Return MyBase.EditFormat
			End Get
		End Property
		<Browsable(False)> _
		Public Overrides ReadOnly Property DisplayFormat() As FormatInfo
			Get
				Return MyBase.DisplayFormat
			End Get
		End Property
		<Browsable(False)> _
		Public Overrides ReadOnly Property Mask() As MaskProperties
			Get
				Return MyBase.Mask
			End Get
		End Property
		<Browsable(False)> _
		Public Shadows Overridable ReadOnly Property EditMask() As String
			Get
				Dim mask As String = "HH:mm:ss"
				If AllowDayInput Then
					mask = "d." & mask
				End If
				Return mask
			End Get
		End Property
		Protected Friend Overridable ReadOnly Property TimeSeparator() As Char
			Get
				Return TimeSpanHelper.TimeSeparator
			End Get
		End Property
		Protected Friend Overridable ReadOnly Property DaySeparator() As Char
			Get
				Return TimeSpanHelper.DaySeparator
			End Get
		End Property
		<Category(CategoryName.Behavior), DefaultValue(False)> _
		Public Overridable Property AllowDayInput() As Boolean
			Get
				Return allowDayInput_Renamed
			End Get
			Set(ByVal value As Boolean)
				If allowDayInput_Renamed = value Then
					Return
				End If
				allowDayInput_Renamed = value
				UpdateFormats()
			End Set
		End Property
		Protected Overridable Sub UpdateFormats()
			EditFormat.FormatString = EditMask
			DisplayFormat.FormatString = EditMask
			Mask.EditMask = EditMask
		End Sub
		Public Overrides Sub Assign(ByVal item As RepositoryItem)
			BeginUpdate()
			Try
				MyBase.Assign(item)
				Dim source As RepositoryItemTimeSpanEdit = TryCast(item, RepositoryItemTimeSpanEdit)
				If source Is Nothing Then
					Return
				End If
				Me.AllowDayInput = source.AllowDayInput
			Finally
				EndUpdate()
			End Try
		End Sub

		Public Overrides Function GetDisplayText(ByVal format As FormatInfo, ByVal editValue As Object) As String
			If TypeOf editValue Is TimeSpan Then
				Return TimeSpanHelper.TimeSpanToString((CType(editValue, TimeSpan)),AllowDayInput)
			End If
			If TypeOf editValue Is String Then
				Return editValue.ToString()
			End If
			Return GetDisplayText(Nothing, New TimeSpan(0))
		End Function

		Protected Friend Overridable Function GetFormatMaskAccessFunction(ByVal editMask As String, ByVal managerCultureInfo As CultureInfo) As String
			Return GetFormatMask(editMask, managerCultureInfo)
		End Function
	End Class

	Public Class TimeSpanEdit
		Inherits TimeEdit
		Shared Sub New()
			RepositoryItemTimeSpanEdit.RegisterTimeSpanEdit()
		End Sub
		Public Sub New()
			MyBase.New()
			Me.fEditValue = New TimeSpan(0)
			Me.fOldEditValue = Me.fEditValue
		End Sub
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemTimeSpanEdit.TimeSpanEditName
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemTimeSpanEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemTimeSpanEdit)
			End Get
		End Property
		Public Overrides Property EditValue() As Object
			Get
				If Properties.ExportMode = ExportMode.DisplayText Then
					Return Properties.GetDisplayText(Nothing, MyBase.EditValue)
				End If
				Return MyBase.EditValue
			End Get
			Set(ByVal value As Object)
				If TypeOf value Is DateTime Then
					Dim time As DateTime = (CDate(value))
					MyBase.EditValue = New TimeSpan(time.Ticks)

				ElseIf TypeOf value Is TimeSpan Then
					MyBase.EditValue = value
				ElseIf TypeOf value Is String Then
					MyBase.EditValue = TimeSpanHelper.Parse(CStr(value))
				Else
					MyBase.EditValue = New TimeSpan(0, 0, 0)
				End If
			End Set
		End Property


		Protected Overrides Function CreateMaskManager(ByVal mask As MaskProperties) As MaskManager
			Dim patchedMask As New CustomTimeEditMaskProperties()
			patchedMask.Assign(mask)
			patchedMask.EditMask = Properties.GetFormatMaskAccessFunction(mask.EditMask, mask.Culture)
			Return patchedMask.CreatePatchedMaskManager()
		End Function
	End Class

	Public Class CustomTimeEditMaskProperties
		Inherits TimeEditMaskProperties
		Public Sub New()
			MyBase.New()
		End Sub
		Public Overridable Function CreatePatchedMaskManager() As MaskManager
			Dim managerCultureInfo As CultureInfo = Me.Culture
			If managerCultureInfo Is Nothing Then
				managerCultureInfo = CultureInfo.CurrentCulture
			End If
			Dim editMask As String = Me.EditMask
			If editMask Is Nothing Then
				editMask = String.Empty
			End If
			Return New CustomDateTimeMaskManager(editMask, False, managerCultureInfo, True)
		End Function
	End Class



End Namespace
