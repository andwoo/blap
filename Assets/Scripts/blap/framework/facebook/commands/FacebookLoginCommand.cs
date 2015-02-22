using blap.framework.facebook.events;
using blap.framework.facebook.responses;

namespace blap.framework.facebook.commands
{
  class FacebookLoginCommand : AbstractFacebookRequestCommand
  {
    public override void Execute()
    {
      base.SendLoginRequest<FacebookLoginResponse>((string)evt.data, FacebookServiceEvent.LOGIN_COMPLETE);
    }
  }
}
