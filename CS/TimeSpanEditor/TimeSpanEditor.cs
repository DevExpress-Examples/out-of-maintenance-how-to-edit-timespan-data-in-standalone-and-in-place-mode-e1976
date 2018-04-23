using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors;
using System.ComponentModel;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils;
using DevExpress.Data.Mask;
using System.Globalization;
using DevExpress.XtraEditors.Mask;

namespace TimeSpanEditor
{
    [UserRepositoryItem("RegisterTimeSpanEdit")]
    public class RepositoryItemTimeSpanEdit : RepositoryItemTimeEdit
    {
        bool allowDayInput;
        static RepositoryItemTimeSpanEdit() { RegisterTimeSpanEdit(); }
        public RepositoryItemTimeSpanEdit() {
            allowDayInput = false;
            UpdateFormats();
        }
        public const string TimeSpanEditName = "TimeSpanEdit";
        public override string EditorTypeName { get { return TimeSpanEditName; } }
        public static void RegisterTimeSpanEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(TimeSpanEditName,
              typeof(TimeSpanEdit), typeof(RepositoryItemTimeSpanEdit),
              typeof(BaseSpinEditViewInfo), new ButtonEditPainter(), true));
        }
        [Browsable(false)]
        public override FormatInfo EditFormat { get { return base.EditFormat; } }
        [Browsable(false)]
        public override FormatInfo DisplayFormat { get { return base.DisplayFormat; } }
        [Browsable(false)]
        public override MaskProperties Mask { get { return base.Mask; } }
        [Browsable(false)]
        public new virtual string EditMask { 
            get {
                string mask = "HH:mm:ss";
                if (AllowDayInput) mask = "d." + mask;
                return mask; 
            } 
        }
        protected internal virtual char TimeSeparator { get { return TimeSpanHelper.TimeSeparator; } }
        protected internal virtual char DaySeparator { get { return TimeSpanHelper.DaySeparator; } }
        [Category(CategoryName.Behavior), DefaultValue(false)]
        public virtual bool AllowDayInput 
        {
            get { return allowDayInput; }
            set { 
                if (allowDayInput == value) return;
                allowDayInput = value;
                UpdateFormats();
            }
        }
        protected virtual void UpdateFormats() {
            EditFormat.FormatString = EditMask;
            DisplayFormat.FormatString = EditMask;
            Mask.EditMask = EditMask;
        }
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemTimeSpanEdit source = item as RepositoryItemTimeSpanEdit;
                if (source == null) return;
                this.AllowDayInput = source.AllowDayInput;
            }
            finally
            {
                EndUpdate();
            }
        }

        public override string GetDisplayText(FormatInfo format, object editValue)
        {
            if (editValue is TimeSpan)
                return TimeSpanHelper.TimeSpanToString(((TimeSpan)editValue),AllowDayInput );
            if (editValue is string)
                return editValue.ToString();
            return GetDisplayText(null, new TimeSpan(0));
        }

        protected internal virtual string GetFormatMaskAccessFunction(string editMask, CultureInfo managerCultureInfo)
        {
            return GetFormatMask(editMask, managerCultureInfo);
        }
    }

    public class TimeSpanEdit : TimeEdit
    {
        static TimeSpanEdit() { RepositoryItemTimeSpanEdit.RegisterTimeSpanEdit(); }
        public TimeSpanEdit()
            : base()
        {
            this.fOldEditValue = this.fEditValue = new TimeSpan(0);
        }
        public override string EditorTypeName { get { return RepositoryItemTimeSpanEdit.TimeSpanEditName; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemTimeSpanEdit Properties { get { return base.Properties as RepositoryItemTimeSpanEdit; } }
        public override object EditValue
        {
            get
            {
                if (Properties.ExportMode == ExportMode.DisplayText)
                    return Properties.GetDisplayText(null, base.EditValue); 
                return base.EditValue;
            }
            set
            {
                if (value is DateTime)
                {
                    DateTime time = ((DateTime)value);
                    base.EditValue = new TimeSpan(time.Ticks);

                }
                else if (value is TimeSpan)
                    base.EditValue = value;
                else if (value is string)
                    base.EditValue = TimeSpanHelper.Parse((string)value);
                else
                    base.EditValue = new TimeSpan(0, 0, 0);
            }
        }


        protected override MaskManager CreateMaskManager(MaskProperties mask)
        {
            CustomTimeEditMaskProperties patchedMask = new CustomTimeEditMaskProperties();
            patchedMask.Assign(mask);
            patchedMask.EditMask = Properties.GetFormatMaskAccessFunction(mask.EditMask, mask.Culture);
            return patchedMask.CreatePatchedMaskManager();
        }
    }
    
    public class CustomTimeEditMaskProperties : TimeEditMaskProperties
    {
        public CustomTimeEditMaskProperties() : base() { }
        public virtual MaskManager CreatePatchedMaskManager()
        {
            CultureInfo managerCultureInfo = this.Culture;
            if (managerCultureInfo == null)
                managerCultureInfo = CultureInfo.CurrentCulture;
            string editMask = this.EditMask;
            if (editMask == null)
                editMask = string.Empty;
            return new CustomDateTimeMaskManager(editMask, false, managerCultureInfo, true);
        }
    }



}
