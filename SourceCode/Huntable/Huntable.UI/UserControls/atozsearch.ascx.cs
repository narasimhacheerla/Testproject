using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Huntable.Business.DataProviders;
using test1;

namespace Huntable.UI.UserControls
{
    public partial class Atozsearch : System.Web.UI.UserControl
    {
        protected string LetterFilter
        {
            get
            {
                return ViewState[this + "_letterFilter"] as string;
            }
            set { ViewState[this + "_letterFilter"] = value == "All" ? null : value; }
        }

        public IDataProvider DataProvider { get; set; }
        public string SearchTitle { get; set; }
        public int GroupItemCount { get; set; }        
        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SearchTitle)) lblSearchTitle.Text = SearchTitle;
            if (GroupItemCount > 0) lvItems.GroupItemCount = GroupItemCount;
            
            DisplayData();
        }

        private void DisplayData()
        {
            IEnumerable<dynamic> dt = DataProvider.GetItems(txtSearch.Text, LetterFilter, 1, 1);
            lvItems.DataSource = dt;
            lvItems.DataBind();

            LettersBind();
        }

        private void LettersBind()
        {

            // Declares a variable that will store a referance to the DataTable we are 
            //  going to bind the repeater control to.
            DataTable dt;

            //------------------------------------------------------------------------
            // Get the appropriate set of records to view/edit
            if (Session[ToString() + "_LettersData"] == null)
            {

                string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
                             "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
                             "U", "V", "W", "X", "Y", "Z", "All"};

                // Create a new data table
                dt = new DataTable();

                // Create the scheme of the table
                dt.Columns.Add(new DataColumn("Letter", typeof(string)));

                // Populate the data table with the letter data
                foreach (string t in letters)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = t;
                    dt.Rows.Add(dr);
                }

                // Store a referance to the newly create data tabel in the session for 
                //  use on post back.
                Session[ToString() + "_LettersData"] = dt;
            }
            else
                dt = (DataTable)Session[ToString() + "_LettersData"];

            //------------------------------------------------------------------------
            // Bind the data's default view to the grid
            lvLetters.DataSource = dt.DefaultView;
            lvLetters.DataBind();
        }

        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }
        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string)commandEventArgs.CommandArgument;
            }
        }

        protected void OnSearchContent(object sender, EventArgs e)
        {

        }

        protected void ItemsItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            var uc = (ItemDisplayTemplate)eventArgs.Item.Controls[3];

            uc.Item = eventArgs.Item.DataItem;

            var key = (HiddenField)eventArgs.Item.FindControl("hfKey");

            key.Value = (eventArgs.Item.DataItem as dynamic).Key.Id as string;
        }

        protected void OnUpdate(object sender, EventArgs e)
        {                       
            if (hdnsave.Value != "next")
            {
                Session["data"] = null;
                Session["nondata"] = null;
            }
            lblmessage.Visible = true;
            lblmessage.ForeColor = System.Drawing.Color.Red;
            var checkedlist = (List<int>)this.Session["data"];
            var uncheckedlist = (List<int>) this.Session["nondata"];
            if (checkedlist != null && uncheckedlist != null)
            {
                DataProvider.UpdateItems(checkedlist, uncheckedlist);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                                                        "overlay('Preferences saved succesfully')", true);

            }
            if (hdnsave.Value == "next")
            {
                hdnsave.Value = "";
            }

          if(checkedlist==null&&uncheckedlist==null)
            {
               Savedata();
            }

        }
        public void Savedata()
        {
            var newlyCheckedList = new List<int>();
            var newlyUncheckedList = new List<int>();
            foreach (var listViewDataItem in lvItems.Items)
            {
                var uc = (ItemDisplayTemplate)listViewDataItem.Controls[3];
                if (uc.IsNewlyChecked)
                {
                    newlyCheckedList.Add(uc.Id);
                }
                if (uc.IsNewlyUnchecked)
                {
                    newlyUncheckedList.Add(uc.Id);
                }
            }
            
            Session["data"] = newlyCheckedList;
            Session["nondata"] = newlyUncheckedList;
            var checkedlist = (List<int>)this.Session["data"];
            var uncheckedlist = (List<int>)this.Session["nondata"];
            DataProvider.UpdateItems(checkedlist, uncheckedlist);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                                                    "overlay('Preferences saved succesfully')", true);
        }
        protected void BtnNextClick(object sender, EventArgs e)
        {
            var temp = "test";
             var newlyCheckedList = new List<int>();
            var newlyUncheckedList = new List<int>();

            foreach (var listViewDataItem in lvItems.Items)
            {
                var uc = (ItemDisplayTemplate) listViewDataItem.Controls[3];
                if (uc.IsNewlyChecked)
                {
                    newlyCheckedList.Add(uc.Id);
                }
                if (uc.IsNewlyUnchecked)
                {
                    newlyUncheckedList.Add(uc.Id);
                }
                temp = "temp";
            }
            
            List<int> checkedList = newlyCheckedList;
            List<int> uncheckedList = newlyUncheckedList;
            Session["data"] = checkedList;
            Session["nondata"] = uncheckedList;
            if (checkedList.Count == 0 && uncheckedList.Count == 0)
            {
                temp = "test";
            }
            if (temp == "temp")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "rowAction2('test');", true);
                hdnsave.Value = "next";
            }
            else

                Nextop();

        }

        public void Nextop()
        {
            if (HiddenField1.Value != string.Empty)
            {
                Response.Redirect(HiddenField1.Value);
            }
            else
            {


                var page = Path.GetFileName(Request.Path);
                if (page == "CustomizeJobsIndustry.aspx")
                {
                    Response.Redirect("~/CustomizeJobsSkill.aspx");
                }
                else if (page == "CustomizeJobsSkill.aspx")
                {
                    Response.Redirect("~/CustomizeJobsJobType.aspx");
                }
                else if (page == "CustomizeJobsJobType.aspx")
                {
                    Response.Redirect("~/CustomizeJobsCountry.aspx");
                }
                else if (page == "CustomizeJobsCountry.aspx")
                {
                    Response.Redirect("CustomizeJobsSalary.aspx");
                }
                else if (page == "CustomizeFeedsIndustry.aspx")
                {
                    Response.Redirect("~/CustomizeFeedsSkill.aspx");
                }
                else if (page == "CustomizeFeedsSkill.aspx")
                {
                    Response.Redirect("~/CustomizeFeedsInterest.aspx");
                }
                else if (page == "CustomizeFeedsInterest.aspx")
                {
                    Response.Redirect("~/CustomizeFeedsCountry.aspx");
                }
                else if (page == "CustomizeFeedsCountry.aspx")
                {
                    Response.Redirect("~/Customize-User.aspx");
                }
            }
        }
    }
}