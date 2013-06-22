using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huntable.UI.UserControls
{
    public partial class UserFeedComments : System.Web.UI.UserControl
    {
        public string UserImage
        {
            get { return hdnUserImage.Value; }
            set { hdnUserImage.Value = value; }
        }
        public string FeedUserId
        {
            get { return hdnFeedUserId.Value; }
            set { hdnFeedUserId.Value = value; }
        }
        public string FeedId
        {
            get { return hdnFeedId.Value; }
            set { hdnFeedId.Value = value; }
        }
        public string FeedType
        {
            get { return hdnType.Value; }
            set { hdnType.Value = value; }
        }
        public string RefRecordId
        {
            get { return hdnRefRecId.Value; }
            set { hdnRefRecId.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}