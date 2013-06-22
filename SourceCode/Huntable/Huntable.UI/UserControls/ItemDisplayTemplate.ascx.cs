using System;

namespace test1
{
    public partial class ItemDisplayTemplate : System.Web.UI.UserControl
    {
        public dynamic Item
        {
            set
            {
                lblItemDescription.Text = value.Key.Description;
                hfId.Value = ((Int32)value.Key.Id).ToString();
                hfOriginalSelected.Value = ((bool)value.Value).ToString(); 
                chkItem.Checked = (bool)value.Value;
            }
        }

        public int Id
        {
            get { return Int32.Parse(hfId.Value); }
        }

        public bool IsNewlyChecked
        {
            get { return !bool.Parse(hfOriginalSelected.Value) && chkItem.Checked; }
        }

        public bool IsNewlyUnchecked
        {
            get { return bool.Parse(hfOriginalSelected.Value) && !chkItem.Checked; }
        }
    }
}