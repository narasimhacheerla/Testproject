using System.Linq;
using System.Windows;
using Huntable.Data;

namespace HuntableRecruiter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int Loginid;
        private void Button1Click(object sender, RoutedEventArgs e)
        {
           
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            //else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //{
            //    errormessage.Text = "Enter a valid email.";
            //    textBoxEmail.Select(0, textBoxEmail.Text.Length);
            //    textBoxEmail.Focus();
            //}
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox.Password;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var login =
                        context.Logins.FirstOrDefault(
                            x => x.EmailAddress == email && x.Password == password&&x.IsRecuirter==true);
                    if (login != null)
                    {
                        if (login.CompanyId != null)
                        {
                            var fp = new FileUpload((int) login.CompanyId);
                            fp.Show();
                        }
                        Application.Current.Properties["UserId"] = login.Id;
                        if (login.UserId != null) Loginid = (int) login.UserId;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials");
                    }
                }
            }
        }

        private void ButtonRegisterClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
