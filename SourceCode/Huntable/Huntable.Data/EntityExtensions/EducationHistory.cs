using System;

namespace Huntable.Data
{
    public partial class EducationHistory
    {
        public bool IsNew
        {
            get
            {
                return ID == 0;
            }
        }

        private string _tempId;

        public string TempId
        {
            get
            {
                if (string.IsNullOrEmpty(_tempId))
                {
                    _tempId = Guid.NewGuid().ToString();
                }
                return _tempId;
            }
        }
    }
}
