using blap.framework.facebook.events;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;

namespace blap.framework.facebook.commands
{
  class FacebookGetFriendsCommand : AbstractFacebookRequestCommand
  {
    public override void Execute()
    {
      SendApiRequest<FacebookFriendsResponse>(new FacebookFriendsRequest(evt.data != null ? (string)evt.data : ""), FacebookServiceEvent.GET_FRIENDS_COMPLETE);
    }
  }
}
