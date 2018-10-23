using System;
using UnityEngine;

namespace SimpleBot
{
    public class Effect
    {
        private Func<State, bool> apply;
        private string targetField;
        private object setValue;
        private object defaultValue;
        private string referField;
        private State state;

        public Effect(EffectConfig config)
        {
            targetField = config.TargetFIeld;
            setValue = config.SetValue;
            referField = config.ReferField;
            apply = this.load(config);
            state = new State();

            // Set default value...
            if (config.DefaultValue is Int64 || config.DefaultValue is int)
            {
                var targetValue = Int32.Parse(config.DefaultValue.ToString());
                state.SetInt(targetField, targetValue);
            }
            else if (config.DefaultValue is string)
            {
                state.SetString(targetField, (string)config.DefaultValue);
            }
            else if (config.DefaultValue == null)
            { 
                // dummy
            }
            else
            {
                Debug.Log("failed to add the default value for " + targetField + "....");
                Debug.Log("type of the targett value is " + config.DefaultValue.GetType().FullName);
            }
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
            else if (config.EffectType == "copy-sfield")
            {
                return (State state) =>
                {
                    if (state.HasKey(referField))
                    {
                        string value = state.GetString(referField);
                        state.SetString(targetField, value);
                    }
                    else
                    {
                        Debug.Log("no such key as " + referField);
                    }
                    return true;
                };
            }
            else if (config.EffectType == "copy-ifield")
            {
                return (State state) =>
                {
                    if (state.HasKey(referField))
                    {
                        int value = state.GetInt(referField);
                        state.SetInt(targetField, value);
                    }
                    else
                    {
                        Debug.Log("no such key as " + referField);
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

