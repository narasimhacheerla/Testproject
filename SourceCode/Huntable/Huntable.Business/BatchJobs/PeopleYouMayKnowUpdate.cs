using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class PeopleYouMayKnowUpdate : BatchJobBase
    {
        public PeopleYouMayKnowUpdate()
            : base("PeopleYouMayKnowUpdate")
        {

        }

        public override void Run(huntableEntities huntableEntities)
        {
            LoggingManager.Debug("Entering into PeopleYouMay");
            List<User> allUsers = huntableEntities.Users.ToList();
            List<FriendsToInvite> allFriendsInvitationList = huntableEntities.FriendsToInvites.ToList();

            // For each user, 
            foreach (User user in allUsers)
            {
                try
                {

           
                User user2 = user;
                List<User> allConnections = huntableEntities.Users.Where(u => u.ReferralId == user2.Id).ToList();

                // 1. find all his connections
                foreach (User connectedUser in allConnections)
                {
                    User connectedUser1 = connectedUser;
                    var allInvitationsList = huntableEntities.Invitations.Where(u => u.UserId == connectedUser1.Id).ToList();

                    // 2. For each of his connection, find all his invitations - add them to his list
                    foreach (Invitation invitation in allInvitationsList.Where(x => !x.IsJoined))
                    {
                        User user1 = user;
                        Invitation invitation1 = invitation;

                        if(allFriendsInvitationList.Any(x => x.UserId == user1.Id && x.FriendInvitationId == invitation1.Id))
                        {
                            continue;
                        }

                        if (invitation.InvitationType == InvitationType.Email)
                        {
                            var friendToInvite = new FriendsToInvite
                                                     {
                                                         UserId = user.Id,
                                                         FriendInvitationId = invitation.Id,
                                                         CreatedDateTime = DateTime.Now,
                                                         Invited = false,
                                                         Cancelled = false
                                                     };

                            huntableEntities.AddToFriendsToInvites(friendToInvite);
                        }
                    }
                }

                huntableEntities.SaveChanges();
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("exiting from solution with exception");
                    throw;
                }
            }
        }
    }
}