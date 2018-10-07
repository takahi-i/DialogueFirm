using System;

namespace SimpleBot
{
    public class Effect
    {
        private Func<State, bool> apply;
        private string targetField;

        public Effect(EffectConfig config)
        {
            targetField = config.TargetFIeld;
            apply = this.load(config);
        }

        public bool Apply(State state) {
            return this.apply(state);
        }

        private Func<State, bool> load(EffectConfig config)
        {
            if (config.EffectType == "incr")
            {
                return (State state) => {
                    if (state.HasKey(targetField))
                    {
                        int result = state.GetInt(targetField);
                        state.SetInt(targetField, ++result); 
                    }
                    return true;
                };
            } else {
                throw new ArgumentException(config.EffectType + " is not supported");
            }
        }
    }
}

