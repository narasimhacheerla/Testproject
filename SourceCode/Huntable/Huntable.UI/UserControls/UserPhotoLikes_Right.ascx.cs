using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huntable.UI.UserControls
{
    public partial class UserPhotoLikes_Right : System.Web.UI.UserControl
    {
        public string profileUserId
        {
            get { return hdnprofileUserId.Value; }
            set { hdnprofileUserId.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hyptopLike.HRef = "https://huntable.co.uk/Likes.aspx?UserId=" + profileUserId;
            hypbottomLike.HRef = "https://huntable.co.uk/Likes.aspx?UserId=" + profileUserId;
        }
    }
}