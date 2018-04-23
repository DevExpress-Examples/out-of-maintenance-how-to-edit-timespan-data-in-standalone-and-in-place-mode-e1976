using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Mask;
using System.Globalization;

namespace TimeSpanEditor
{
    public class CustomDateTimeMaskManager : MaskManagerSelectAllEnhancer<DateTimeMaskManagerCore>
    {
        public CustomDateTimeMaskManager(string mask, bool isOperatorMask, CultureInfo culture, bool allowNull)
            : base(new CustomDateTimeMaskManagerCore(mask, isOperatorMask, culture, allowNull))
        {
        }
        protected override bool IsNestedCanSelectAll { get { return false; } }
        public static bool DoNotClearValueOnInsertAfterSelectAll = false;
        protected override bool MakeChange(Func<bool> changeWithTrueWhenSuccessfull)
        {
            if (IsSelectAllEnforced)
            {
                ClearSelectAllFlag();
                if (!DoNotClearValueOnInsertAfterSelectAll)
                {
                    Nested.ClearFromSelectAll();
                }
                base.MakeChange(changeWithTrueWhenSuccessfull);
                return true;
            }
            else
            {
                return base.MakeChange(changeWithTrueWhenSuccessfull);
            }
        }
        protected override bool MakeCursorOp(Func<bool> cursorOpWithTrueWhenSuccessfull)
        {
            if (IsSelectAllEnforced)
            {
                ClearSelectAllFlag();
                base.MakeCursorOp(cursorOpWithTrueWhenSuccessfull);
                return true;
            }
            else
            {
                return base.MakeCursorOp(cursorOpWithTrueWhenSuccessfull);
            }
        }
        static bool IsGoodCalendar(System.Globalization.Calendar calendar)
        {
            if (calendar is GregorianCalendar)
                return true;
            if (calendar is KoreanCalendar)
                return true;
            if (calendar is TaiwanCalendar)
                return true;
            if (calendar is ThaiBuddhistCalendar)
                return true;
            return false;
        }
        public static DateTimeFormatInfo GetGoodCalendarDateTimeFormatInfo(CultureInfo inputCulture)
        {
            if (IsGoodCalendar(inputCulture.DateTimeFormat.Calendar))
                return inputCulture.DateTimeFormat;
            DateTimeFormatInfo result = (DateTimeFormatInfo)inputCulture.DateTimeFormat.Clone();
            foreach (System.Globalization.Calendar candidateCalendar in inputCulture.OptionalCalendars)
            {
                if (IsGoodCalendar(candidateCalendar))
                {
                    result.Calendar = candidateCalendar;
                    return result;
                }
            }
            return DateTimeFormatInfo.InvariantInfo;
        }
        public override bool Backspace()
        {
            if (IsSelectAllEnforced)
            {
                ClearSelectAllFlag();
                Nested.ClearFromSelectAll();
                return true;
            }
            else
            {
                return base.Backspace();
            }
        }
        public override bool Delete()
        {
            if (IsSelectAllEnforced)
            {
                ClearSelectAllFlag();
                Nested.ClearFromSelectAll();
                return true;
            }
            else
            {
                return base.Delete();
            }
        }
    }


    public class CustomDateTimeMaskManagerCore : DateTimeMaskManagerCore
    {
        public CustomDateTimeMaskManagerCore(string mask, bool isOperatorMask, CultureInfo culture, bool allowNull)
            : base(mask, isOperatorMask, culture, allowNull)
        {
            fFormatInfo = new CustomDateTimeMaskFormatInfo(mask, this.fInitialDateTimeFormatInfo);
        }
        public override void SetInitialEditText(string initialEditText)
        {
            KillCurrentElementEditor();
            DateTime? initialEditValue = DateTime.MinValue;
            if (!string.IsNullOrEmpty(initialEditText))
            {
                try
                {
                    initialEditValue = new DateTime(TimeSpanHelper.Parse(initialEditText).Ticks);
                }
                catch { }
            }
            SetInitialEditValue(initialEditValue);
        }
    }

    public class CustomDateTimeMaskFormatInfo : DateTimeMaskFormatInfo
    {
        public CustomDateTimeMaskFormatInfo(string mask, DateTimeFormatInfo dateTimeFormatInfo)
            : base(mask, dateTimeFormatInfo)
        {
            for (int i = 0; i < Count; i++)
            {
                if (innerList[i] is DateTimeMaskFormatElement_d || innerList[i] is DateTimeMaskFormatElement_d)
                {
                    innerList[i] = new DateTimeMaskFormatElement_Dxxx("H", dateTimeFormatInfo);
                    return;
                }
                if (innerList[i] is DateTimeMaskFormatElement_H24 || innerList[i] is DateTimeMaskFormatElement_h12)
                {
                    innerList[i] = new DateTimeMaskFormatElement_Hxxx("H", dateTimeFormatInfo);
                    return;
                }
            }
        }
    }
    public class DateTimeMaskFormatElement_Hxxx : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_Hxxx(string mask, DateTimeFormatInfo dateTimeFormatInfo) : base(mask, dateTimeFormatInfo, DateTimePart.Time) { }
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime)
        {
            return new DateTimeNumericRangeElementEditor(GetHours(editedDateTime), 0, 24000000, 1, 9);
        }
        public override DateTime ApplyElement(int result, DateTime editedDateTime)
        {
            TimeSpan value = new TimeSpan(result, editedDateTime.Minute, editedDateTime.Second);
            return new DateTime(value.Ticks);
        }
        public override string Format(DateTime formattedDateTime)
        {
            return GetHours(formattedDateTime).ToString();
        }
        protected virtual int GetHours(DateTime dt)
        {
            TimeSpan internalValue = new TimeSpan(dt.Ticks);
            return System.Convert.ToInt32(Math.Floor(internalValue.TotalHours));
        }
    }

    public class DateTimeMaskFormatElement_Dxxx : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_Dxxx(string mask, DateTimeFormatInfo dateTimeFormatInfo) : base(mask, dateTimeFormatInfo, DateTimePart.Time) { }
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime)
        {
            TimeSpan internalValue = new TimeSpan(editedDateTime.Ticks);
            return new DateTimeNumericRangeElementEditor(internalValue.Days, 0, 1000000, 1, 7);
        }
        public override DateTime ApplyElement(int result, DateTime editedDateTime)
        {
            TimeSpan internalValue = new TimeSpan(result, editedDateTime.Hour, editedDateTime.Minute, editedDateTime.Second);
            return new DateTime(internalValue.Ticks);
        }
        public override string Format(DateTime formattedDateTime)
        {
            TimeSpan internalValue = new TimeSpan(formattedDateTime.Ticks);
            return internalValue.Days.ToString();
        }
    }
    public class TimeSpanHelper
    {
        const char timeSeparator = ':';
        const char daySeparator = '.';
        public TimeSpanHelper() { }
        public static TimeSpan Parse(string str)
        {
            TimeSpan ts;
            try { ts = TimeSpan.Parse(str); }
            catch (System.OverflowException)
            {
                int hours, index = str.IndexOf(TimeSeparator);
                string HoursStr = str.Substring(0, index);
                str = str.Remove(0, index);
                str = str.Insert(0, "00");
                try { hours = int.Parse(HoursStr); }
                catch { return new TimeSpan(0); }
                try { ts = TimeSpan.Parse(str); }
                catch { return new TimeSpan(0); }
                ts = new TimeSpan(hours, ts.Minutes, ts.Seconds);
            }
            catch { ts = new TimeSpan(0, 0, 0); }
            return ts;
        }
        public static string TimeSpanToString(TimeSpan value, bool alloDayInput)
        {
            if (alloDayInput)
                return value.Days.ToString() + DaySeparator + value.Hours.ToString("00") + TimeSeparator + value.Minutes.ToString("00") + TimeSeparator + value.Seconds.ToString("00");
            string hoursStr;
            hoursStr = Math.Floor(value.TotalHours).ToString("0");
            return hoursStr + TimeSeparator + value.Minutes.ToString("00") + TimeSeparator + value.Seconds.ToString("00");

        }
        public static char TimeSeparator { get { return timeSeparator; } }
        public static char DaySeparator { get { return daySeparator; } }
    }
}
