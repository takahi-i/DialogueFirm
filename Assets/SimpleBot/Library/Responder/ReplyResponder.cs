﻿using SimpleBot;


namespace SimpleBot
{
    namespace Responder
    {
        public abstract class ReplyResponder
        {
            public abstract string Respond(Intent intent);
        }
    }
}