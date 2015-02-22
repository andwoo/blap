using blap.framework.facebook.events;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;

namespace blap.framework.facebook.commands
{
  class FacebookGetUserDetailsCommand : AbstractFacebookRequestCommand
  {
    public override void Execute()
    {
      base.SendApiRequest<FacebookGetUserDetailsResponse>(new FacebookGetUserDetailsRequest(evt.data != null ? (string)evt.data : ""), FacebookServiceEvent.GET_USER_DETAILS_COMPLETE);
    }
  }
}
