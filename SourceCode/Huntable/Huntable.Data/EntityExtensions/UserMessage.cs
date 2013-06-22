using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huntable.Data
{
    public partial class UserMessage
    {
        public string ReadUnReadCssClass
        {
            get
            {
                if (this.IsRead)
                {
                    return "readMessageStyle";
                }
                else
                {
                    return "unReadMessageStyle";
                }
            }
        }

        public string ShortSubject
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Subject) )
                {
                    return (this.Subject.Trim().Length > 20) ? this.Subject.Trim().Substring(0, 20) + "..." : this.Subject.Trim();
                }
                else
                {
                    return null;
                }
            }
        }

        public string ShortBody
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Body))
                {
                    return (this.Body.Trim().Length > 30) ? this.Body.Trim().Substring(0, 30) + "..." : this.Body.Trim();
                }
                else
                {
                    return null;
                }
            }
        }
        public string SubjectAndBody
        {
            get
            {                
                    return this.Subject.Trim() + "#splitHere#" + this.Body.Trim();               
            }
        }
        public bool IsUserBlockMsg { get; set; }        
    }
}
