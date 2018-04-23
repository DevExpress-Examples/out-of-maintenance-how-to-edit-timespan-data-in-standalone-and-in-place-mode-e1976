Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Data.Mask
Imports System.Globalization

Namespace TimeSpanEditor
	Public Class CustomDateTimeMaskManager
		Inherits MaskManagerSelectAllEnhancer(Of DateTimeMaskManagerCore)
		Public Sub New(ByVal mask As String, ByVal isOperatorMask As Boolean, ByVal culture As CultureInfo, ByVal allowNull As Boolean)
			MyBase.New(New CustomDateTimeMaskManagerCore(mask, isOperatorMask, culture, allowNull))
		End Sub
		Protected Overrides ReadOnly Property IsNestedCanSelectAll() As Boolean
			Get
				Return False
			End Get
		End Property
		Public Shared DoNotClearValueOnInsertAfterSelectAll As Boolean = False
		Protected Overrides Function MakeChange(ByVal changeWithTrueWhenSuccessfull As Func(Of Boolean)) As Boolean
			If IsSelectAllEnforced Then
				ClearSelectAllFlag()
				If (Not DoNotClearValueOnInsertAfterSelectAll) Then
					Nested.ClearFromSelectAll()
				End If
				MyBase.MakeChange(changeWithTrueWhenSuccessfull)
				Return True
			Else
				Return MyBase.MakeChange(changeWithTrueWhenSuccessfull)
			End If
		End Function
		Protected Overrides Function MakeCursorOp(ByVal cursorOpWithTrueWhenSuccessfull As Func(Of Boolean)) As Boolean
			If IsSelectAllEnforced Then
				ClearSelectAllFlag()
				MyBase.MakeCursorOp(cursorOpWithTrueWhenSuccessfull)
				Return True
			Else
				Return MyBase.MakeCursorOp(cursorOpWithTrueWhenSuccessfull)
			End If
		End Function
		Private Shared Function IsGoodCalendar(ByVal calendar As System.Globalization.Calendar) As Boolean
			If TypeOf calendar Is GregorianCalendar Then
				Return True
			End If
			If TypeOf calendar Is KoreanCalendar Then
				Return True
			End If
			If TypeOf calendar Is TaiwanCalendar Then
				Return True
			End If
			If TypeOf calendar Is ThaiBuddhistCalendar Then
				Return True
			End If
			Return False
		End Function
		Public Shared Function GetGoodCalendarDateTimeFormatInfo(ByVal inputCulture As CultureInfo) As DateTimeFormatInfo
			If IsGoodCalendar(inputCulture.DateTimeFormat.Calendar) Then
				Return inputCulture.DateTimeFormat
			End If
			Dim result As DateTimeFormatInfo = CType(inputCulture.DateTimeFormat.Clone(), DateTimeFormatInfo)
			For Each candidateCalendar As System.Globalization.Calendar In inputCulture.OptionalCalendars
				If IsGoodCalendar(candidateCalendar) Then
					result.Calendar = candidateCalendar
					Return result
				End If
			Next candidateCalendar
			Return DateTimeFormatInfo.InvariantInfo
		End Function
		Public Overrides Function Backspace() As Boolean
			If IsSelectAllEnforced Then
				ClearSelectAllFlag()
				Nested.ClearFromSelectAll()
				Return True
			Else
				Return MyBase.Backspace()
			End If
		End Function
		Public Overrides Function Delete() As Boolean
			If IsSelectAllEnforced Then
				ClearSelectAllFlag()
				Nested.ClearFromSelectAll()
				Return True
			Else
				Return MyBase.Delete()
			End If
		End Function
	End Class


	Public Class CustomDateTimeMaskManagerCore
		Inherits DateTimeMaskManagerCore
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
			Dim value As New TimeSpan(result, editedDateTime.Minute, editedDateTime.Second)
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
