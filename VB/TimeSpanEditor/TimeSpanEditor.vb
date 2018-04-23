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

	Public Class CustomDateTimeMaskManager
		Inherits DateTimeMaskManager
		Public Sub New(ByVal mask As String, ByVal isOperatorMask As Boolean, ByVal culture As CultureInfo, ByVal allowNull As Boolean)
			MyBase.New(mask, isOperatorMask, culture, allowNull)
			fFormatInfo = New CustomDateTimeMaskFormatInfo(mask, Me.fInitialDateTimeFormatInfo)
		End Sub
		Public Overrides Sub SetInitialEditText(ByVal initialEditText As String)
			KillCurrentElementEditor()
			Dim initialEditValue As Nullable(Of DateTime) = DateTime.MinValue
			If (Not String.IsNullOrEmpty(initialEditText)) Then
				Try
					initialEditValue = New DateTime(TimeSpanHelper.Parse(initialEditText).Ticks)
				Catch
				End Try
			End If
			SetInitialEditValue(initialEditValue)
		End Sub
	End Class
	Public Class CustomDateTimeMaskFormatInfo
		Inherits DateTimeMaskFormatInfo
		Public Sub New(ByVal mask As String, ByVal dateTimeFormatInfo As DateTimeFormatInfo)
			MyBase.New(mask, dateTimeFormatInfo)
			For i As Integer = 0 To Count - 1
				If TypeOf innerList(i) Is DateTimeMaskFormatElement_d OrElse TypeOf innerList(i) Is DateTimeMaskFormatElement_d Then
					innerList(i) = New DateTimeMaskFormatElement_Dxxx("H", dateTimeFormatInfo)
					Return
				End If
				If TypeOf innerList(i) Is DateTimeMaskFormatElement_H24 OrElse TypeOf innerList(i) Is DateTimeMaskFormatElement_h12 Then
					innerList(i) = New DateTimeMaskFormatElement_Hxxx("H", dateTimeFormatInfo)
					Return
				End If
			Next i
		End Sub
	End Class

	Public Class DateTimeMaskFormatElement_Hxxx
		Inherits DateTimeNumericRangeFormatElementEditable
		Public Sub New(ByVal mask As String, ByVal dateTimeFormatInfo As DateTimeFormatInfo)
			MyBase.New(mask, dateTimeFormatInfo, DateTimePart.Time)
		End Sub
		Public Overrides Function CreateElementEditor(ByVal editedDateTime As DateTime) As DateTimeElementEditor
			Return New DateTimeNumericRangeElementEditor(GetHours(editedDateTime), 0, 24000000, 1, 9)
		End Function
		Public Overrides Function ApplyElement(ByVal result As Integer, ByVal editedDateTime As DateTime) As DateTime
			Dim value As New TimeSpan(result, editedDateTime.Minute,editedDateTime.Second)
			Return New DateTime(value.Ticks)
		End Function
		Public Overrides Function Format(ByVal formattedDateTime As DateTime) As String
			Return GetHours(formattedDateTime).ToString()
		End Function
		Protected Overridable Function GetHours(ByVal dt As DateTime) As Integer
			Dim internalValue As New TimeSpan(dt.Ticks)
			Return System.Convert.ToInt32(Math.Floor(internalValue.TotalHours))
		End Function
	End Class

	Public Class DateTimeMaskFormatElement_Dxxx
		Inherits DateTimeNumericRangeFormatElementEditable
		Public Sub New(ByVal mask As String, ByVal dateTimeFormatInfo As DateTimeFormatInfo)
			MyBase.New(mask, dateTimeFormatInfo, DateTimePart.Time)
		End Sub
		Public Overrides Function CreateElementEditor(ByVal editedDateTime As DateTime) As DateTimeElementEditor
			Dim internalValue As New TimeSpan(editedDateTime.Ticks)
			Return New DateTimeNumericRangeElementEditor(internalValue.Days, 0, 1000000, 1, 7)
		End Function
		Public Overrides Function ApplyElement(ByVal result As Integer, ByVal editedDateTime As DateTime) As DateTime
			Dim internalValue As New TimeSpan(result, editedDateTime.Hour, editedDateTime.Minute, editedDateTime.Second)
			Return New DateTime(internalValue.Ticks)
		End Function
		Public Overrides Function Format(ByVal formattedDateTime As DateTime) As String
			Dim internalValue As New TimeSpan(formattedDateTime.Ticks)
			Return internalValue.Days.ToString()
		End Function
	End Class
	Public Class TimeSpanHelper
		Private Const timeSeparator_Renamed As Char = ":"c
		Private Const daySeparator_Renamed As Char = "."c
		Public Sub New()
		End Sub
		Public Shared Function Parse(ByVal str As String) As TimeSpan
			Dim ts As TimeSpan
			Try
				ts = TimeSpan.Parse(str)
			Catch e1 As System.OverflowException
				Dim hours As Integer, index As Integer = str.IndexOf(TimeSeparator)
				Dim HoursStr As String = str.Substring(0, index)
				str = str.Remove(0, index)
				str = str.Insert(0, "00")
				Try
					hours = Integer.Parse(HoursStr)
				Catch
					Return New TimeSpan(0)
				End Try
				Try
					ts = TimeSpan.Parse(str)
				Catch
					Return New TimeSpan(0)
				End Try
				ts = New TimeSpan(hours, ts.Minutes, ts.Seconds)
			Catch
				ts = New TimeSpan(0, 0, 0)
			End Try
			Return ts
		End Function
		Public Shared Function TimeSpanToString(ByVal value As TimeSpan, ByVal alloDayInput As Boolean) As String
			If alloDayInput Then
				Return value.Days.ToString() & DaySeparator + value.Hours.ToString("00") + TimeSeparator + value.Minutes.ToString("00") + TimeSeparator + value.Seconds.ToString("00")
			End If
			Dim hoursStr As String
			hoursStr = Math.Floor(value.TotalHours).ToString("0")
			Return hoursStr & TimeSeparator + value.Minutes.ToString("00") + TimeSeparator + value.Seconds.ToString("00")

		End Function
		Public Shared ReadOnly Property TimeSeparator() As Char
			Get
				Return timeSeparator_Renamed
			End Get
		End Property
		Public Shared ReadOnly Property DaySeparator() As Char
			Get
				Return daySeparator_Renamed
			End Get
		End Property
	End Class
End Namespace
