using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snovaspace.Util.Mail.Batchjob;
using Snovaspace.Util.Mail;

namespace Snovaspace.Batchjob.Email
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //EmailManager em = new EmailManager("Claims");
                //em.SendEmail("Test", "Test", new string[] { "hareenmallipeddi@gmail.com" });
                SendPendingEmailManager.SendEmails();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
