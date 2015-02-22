namespace blap.framework.facebook.events
{
  enum FacebookServiceEvent
  {
    /* Initialize events */
    INITIALIZE,
    INITIALIZE_UNITY_RESUME,
    INITIALIZE_UNITY_MINIMIZE,
    INITIALIZE_COMPLETE,

    /* Activate App events */
    ACTIVATE_APP,
    ACTIVATE_APP_COMPLETE,

    /* Login events */
    LOGIN,
    LOGIN_COMPLETE,

    /* Get friends */
    GET_FRIENDS,
    GET_FRIENDS_COMPLETE,

    GET_USER_DETAILS,
    GET_USER_DETAILS_COMPLETE
  }
}
