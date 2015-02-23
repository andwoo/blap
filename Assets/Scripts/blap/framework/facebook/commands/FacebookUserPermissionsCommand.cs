using blap.framework.facebook.events;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;

namespace blap.framework.facebook.commands
{
  class FacebookUserPermissionsCommand : AbstractFacebookRequestCommand
  {
    public override void Execute()
    {
      base.SendApiRequest<FacebookGetUserPermissionsResponse>(new FacebookGetUserPermissionsRequest(), FacebookServiceEvent.GET_USER_PERMISSIONS_COMPLETE);
    }
  }
}
