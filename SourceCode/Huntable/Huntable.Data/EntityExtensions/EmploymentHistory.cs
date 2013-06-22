using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huntable.Data
{
    public partial class EmploymentHistory
    {
        public bool IsNew
        {
            get
            {
                return this.Id == 0;
            }
        }

        private string _tempId;
        public string TempId
        {
            get
            {
                if(string.IsNullOrEmpty(_tempId))
                {
                    _tempId = Guid.NewGuid().ToString();
                }
                return _tempId;
            }
        }

       
    }
}
