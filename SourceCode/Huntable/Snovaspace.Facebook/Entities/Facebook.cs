using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snovaspace.Facebook.Entities
{
    /// <summary>
    /// Gigs entity
    /// </summary>
    [Serializable]
    public class Facebook
    {
        public int Id { get; set; }
        public string FacebookAccessToken { get; set; }
        public string FacebookId { get; set; }
    }
}
