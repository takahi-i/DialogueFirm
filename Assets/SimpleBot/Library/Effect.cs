using System;

namespace SimpleBot
{
    public class Effect
    {
        private Func<State, bool> apply;
        private string targetField;
        private object setValue;

        public Effect(EffectConfig config)
        {
            targetField = config.TargetFIeld;
            setValue = config.SetValue;
            apply = this.load(config);
        }

        public bool Apply(State state) {
            return this.apply(state);
        }

        private Func<State, bool> load(EffectConfig config)
        {
            if (config.EffectType == "incr")
            {
                return (State state) =>
                {
                    if (state.HasKey(targetField))
                    {
                        int result = state.GetInt(targetField);
                        state.SetInt(targetField, ++result);
                    }
                    return true;
                };
            }
            else if (config.EffectType == "decr")
            {
                return (State state) =>
                {
                    if (state.HasKey(targetField))
                    {
                        int result = state.GetInt(targetField);
                        state.SetInt(targetField, --result);
                    }
                    return true;
                };
            }
            else if (config.EffectType == "set")
            {
                return (State state) =>
                {
                    if (this.setValue is int)
                    {
                        state.SetInt(targetField, (int)this.setValue);
                    } else if (this.setValue is string) {
                        state.SetString(targetField, (string)this.setValue);
                    }
                    return true;
                };
            }
            else {
                throw new ArgumentException(config.EffectType + " is not supported");
            }
        }
    }
}

