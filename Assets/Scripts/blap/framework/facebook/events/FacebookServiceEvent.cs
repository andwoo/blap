namespace blap.framework.facebook.events
{
  enum FacebookServiceEvent
  {
    /* Initialize events */
    INITIALIZE,
    INITIALIZE_UNITY_RESUME,
    INITIALIZE_UNITY_MINIMIZE,
    INITIALIZE_COMPLETE,

    /* Login events */
    LOGIN,
    LOGIN_COMPLETE_SUCCESS,
    LOGIN_COMPLETE_FAILED
  }
}
