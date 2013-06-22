
using System.Collections.Generic;

namespace OAuthUtility
{
    public class Contact
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string Email { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsInvited { get; set; }
        public int TokenId { get; set; }
        

        public static IList<Contact> SampleContacts;

        static Contact()
        {
            SampleContacts = new List<Contact>();
            for (var i = 1; i < 101; i++)
                SampleContacts.Add(new Contact { Id = i, Provider = "test", Name = "Contact-" + i, Email = "a" + i + "@a.com" });

        }
    }
}
