﻿namespace Facebook
{
    public class OGActionType
    {
        public static readonly OGActionType Send = new OGActionType { actionTypeValue = "send" };
        public static readonly OGActionType AskFor = new OGActionType { actionTypeValue = "askfor" };
        public static readonly OGActionType Turn = new OGActionType { actionTypeValue = "turn" };

        private string actionTypeValue;

        public override string ToString()
        {
            return actionTypeValue;
        }
    }
}
