using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;

namespace Snovaspace.Util.Controls
{
    public partial class DataPager : System.Web.UI.UserControl
    {
        private Delegate _delUpdatePageIndex;
        public Delegate UpdatePageIndex
        {
            set { _delUpdatePageIndex = value; }
        }

        #region "Properties"
        [Category("Behavior")]
        [Description("Total number of records")]
        [DefaultValue(0)]
        public int TotalRecords
        {
            get
            {
                object o = ViewState["TotalRecords"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set
            {
                UpdatePaging(PageIndex, RecordsPerPage, value, false);
                ViewState["TotalRecords"] = value;
            }
        }
        [Category("Behavior")]
        [Description("Current page index")]
        [DefaultValue(1)]
        public int PageIndex
        {
            get
            {
                object o = ViewState["PageIndex"];
                if (o == null)
                    return 1;
                return (int)o;
            }
            set { ViewState["PageIndex"] = value; }
        }
        [Category("Behavior")]
        [Description("Total number of records to each page")]
        [DefaultValue(25)]
        public int RecordsPerPage
        {
            get
            {
                object o = ViewState["RecordsPerPage"];
                if (o == null)
                    return 25;
                return (int)o;
            }
            set { ViewState["RecordsPerPage"] = value; }
        }
        private decimal TotalPages
        {
            get
            {
                object o = ViewState["TotalPages"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["TotalPages"] = value; }
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int[] paging = { 3, 5, 10, 20, 30, 50, 100, 200, 300, 500, 1000 };
                for (int p = 0; p < paging.Length; p++)
                {
                    ddlRecords.Items.Add(paging[p].ToString());
                    ddlRecords.SelectedIndex = 0;
                }
                if (RecordsPerPage <= 0) RecordsPerPage = Convert.ToInt32(ddlRecords.Text);
                UpdatePaging(PageIndex, RecordsPerPage, TotalRecords, true);
            }
        }
        #endregion

        #region "Control Events"
        protected void BtnMoveClick(object sender, CommandEventArgs e)
        {
            switch (Convert.ToString(e.CommandArgument))
            {
                case "First":
                    PageIndex = 1;
                    break;
                case "Previous":
                    PageIndex--;
                    break;
                case "Next":
                    PageIndex++;
                    break;
                case "Last":
                    PageIndex = (int)TotalPages;
                    break;
            }
            UpdatePaging(PageIndex, RecordsPerPage, TotalRecords, true);
        }
        protected void TxtPageTextChanged(object sender, EventArgs e)
        {
            int newPage = Convert.ToInt32(txtPage.Text);
            if (newPage > TotalPages)
                PageIndex = (int)TotalPages;
            else
                PageIndex = newPage;
            UpdatePaging(PageIndex, RecordsPerPage, TotalRecords, true);
        }
        protected void DDLRecordsSelectedIndexChanged(object sender, EventArgs e)
        {
            RecordsPerPage = Convert.ToInt32(ddlRecords.Text);
            PageIndex = 1;
            UpdatePaging(PageIndex, RecordsPerPage, TotalRecords, true);
        }
        #endregion

        #region "Web Methods"
        public void UpdatePaging(int pageIndex, int pageSize, int recordCount, bool isNotify)
        {
            if (recordCount > 0)
            {
                lblTotalRecord.ForeColor = Color.Black;
                ddlRecords.Enabled = true;
                int currentEndRow = (pageIndex * pageSize);
                if (currentEndRow > recordCount) currentEndRow = recordCount;

                if (currentEndRow < pageSize) pageSize = currentEndRow;
                int currentStartRow = (currentEndRow - pageSize) + 1;

                TotalPages = Math.Ceiling((decimal)recordCount / pageSize);
                txtPage.Text = string.Format("{0:00}", PageIndex);
                lblTotalRecord.Text = string.Format("{0:00}-{1:00} of {2:00}", currentStartRow, currentEndRow, recordCount);
                lblTotalPage.Text = string.Format(" of {0:00} page(s)", TotalPages);

                btnMoveFirst.Enabled = pageIndex != 1;
                btnMovePrevious.Enabled = (pageIndex > 1);
                btnMoveNext.Enabled = (pageIndex * pageSize < recordCount);
                btnMoveLast.Enabled = !(pageIndex * pageSize >= recordCount);

                //call method to re-populate parent page data, 
                //given current index:
                var aObj = new object[1];
                aObj[0] = pageIndex;
                if (isNotify && _delUpdatePageIndex != null)
                    _delUpdatePageIndex.DynamicInvoke(aObj);
            }
            else
            {
                lblTotalPage.Text = "";
                lblTotalRecord.Text = "No Record Found!";
                lblTotalRecord.ForeColor = Color.Red;
                btnMoveFirst.Enabled = false;
                btnMovePrevious.Enabled = false;
                btnMoveNext.Enabled = false;
                btnMoveLast.Enabled = false;
                txtPage.Enabled = false;
                ddlRecords.Enabled = false;
            }
        }
        #endregion
    }
}