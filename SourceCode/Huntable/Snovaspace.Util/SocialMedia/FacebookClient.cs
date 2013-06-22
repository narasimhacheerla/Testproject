using ImpactWorks.FBGraph.Connector;
using ImpactWorks.FBGraph.Core;
using ImpactWorks.FBGraph.Interfaces;

namespace Snovaspace.Util.SocialMedia
{
    public class FacebookClient
    {
        public bool SendInvite(Facebook facebook,string invUrl, long id,string name)
        {
            try
            {
                IFeedPost post = new FeedPost();

                post.Action = new FBAction { Link = invUrl, Name = "Read" };
                var Message = "Hi [NAME], I am inviting you to join Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It’s FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";
                post.Message = Message.Replace("[NAME]", name);
                post.Caption = "Inviting you to join huntable";
                post.Description = "The fastest growing Professional Resourcing Network";
                post.Name = "Click here to find out more";
                post.Url = invUrl;               
                facebook.PostToWall(id, post);
                
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool SendInviteTest(string token, string invUrl, long id, string name)
        {
            try
            {
                var facebook = new Facebook();
                facebook.AppID = "307487099357078";
                facebook.Secret = "d24675d8248f10d3f8a52e4d8fe29062";
                facebook.Token = token;
                IFeedPost post = new FeedPost();

                post.Action = new FBAction { Link = invUrl, Name = "Read" };
                var Message = "Hi [NAME], I am inviting you to join Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It’s FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";
                post.Message = Message.Replace("[NAME]", name);
                post.Caption = "Inviting you to join huntable";
                post.Description = "The fastest growing Professional Resourcing Network";
                post.Name = "Click here to find out more";
                post.Url = invUrl;
                facebook.PostToWall(id, post);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
