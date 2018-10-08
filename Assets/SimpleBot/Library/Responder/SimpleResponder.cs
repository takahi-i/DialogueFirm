using System;
using System.Collections.Generic;
using SimpleBot;


namespace SimpleBot
{
    namespace Responder
    {
        public class SimpleResponder : ReplyResponder
        {
            private List<string> responds;
            private Random cRandom;
            private string targetIntent;
            Func<State, bool> condition;

            public SimpleResponder(string targetIntent, List<string> responds, ConditionConfig conditionConfig)
            {
                this.targetIntent = targetIntent;
                this.responds = responds;
                this.cRandom = new System.Random();
                this.condition = Condition.Load(conditionConfig);
            }

            public override bool SatisfyState(State state) {
                return this.condition(state);
            }

            public override string Respond(Intent intent)
            {
                if (this.responds.Count == 0)
                {
                    throw new InvalidOperationException("No responds are deployed in the responder for intent " + targetIntent);
                }
                return this.responds[this.cRandom.Next(this.responds.Count - 1)];
            }
        }
    }
}