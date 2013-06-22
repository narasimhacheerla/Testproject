namespace Huntable.UI.UserControls
{
    public partial class ShowProfileImage : System.Web.UI.UserControl
    {
        private string _smallImageSource;
        public string SmallImageSource
        {
            get { return _smallImageSource; }
            set
            {
                _smallImageSource = value;
                smallImage.Src = value;
            }
        }

        private string _bigImageSource;
        public string BigImageSource
        {
            get { return _bigImageSource; }
            set
            {
                _bigImageSource = value;
                bigImage.HRef = value;
            }
        }
    }
}