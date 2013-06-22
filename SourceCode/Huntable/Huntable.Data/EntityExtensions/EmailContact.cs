using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huntable.Data
{
    public class EmailContact
    {
        private string _email = string.Empty;
        private string _name = string.Empty;
        private string _photo = string.Empty;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }
    }
}
