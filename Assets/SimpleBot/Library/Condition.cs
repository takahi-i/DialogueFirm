using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot
{
    public static class Condition
    {
        public static Func<State, bool> Load(ConditionConfig config)
        {
            return term(config.CondtionType, config);
        }

        private static Func<State, bool> term(string condtion_type, ConditionConfig factor_config)
        {
            if (condtion_type == "must")
            {
                return must_method(factor_config);
            }
            else if (condtion_type == "should")
            {
                return should_method(factor_config);
            }
            else
            {
                throw new ArgumentException("Condtion type, " + condtion_type + " is not supported in the top level");
            }
        }

        private static Func<State, bool> must_method(ConditionConfig factor_config)
        {
            var factor_methods = generate_factor_methods(factor_config);
            return (State state) => {
                foreach (var factor in factor_methods)
                {
                    if (!factor(state))
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        private static Func<State, bool> should_method(ConditionConfig factor_config)
        {
            var factor_methods = generate_factor_methods(factor_config);
            return (State state) => {
                foreach (var factor in factor_methods)
                {
                    if (factor(state))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        private static List<Func<State, bool>> generate_factor_methods(ConditionConfig factor_config)
        {
            var factor_methods = new List<Func<State, bool>>();
            foreach (var config in factor_config.ChildConfigs)
            {
                Func<State, bool> factor_method = generate_factor_method(config);
                factor_methods.Add(factor_method);
            }
            return factor_methods;
        }

        private static Func<State, bool> generate_factor_method(ConditionConfig config)
        {
            var factor_name = config.CondtionType;
            if (factor_name == "term")
            {
                return term_method(config);
            }
            else if (factor_name == "range")
            {
                return ranges_method(config);
            } else {
                throw new ArgumentException(factor_name + " is not supported in factor method.");
            }
        }

        private static Func<State, bool> term_method(ConditionConfig config)
        {
            var targetField = config.TargetField;
            var targetValue = (string)config.Arguments[0].First;
            return (State state) => {
                if (state.GetString(targetField) == targetValue)
                {
                    return true;
                }
                return false;
            };
        }

        private static Func<State, bool> ranges_method(ConditionConfig config)
        {
            var targetField = config.TargetField;
            var ranges = config.Arguments;
            List<Func<State, bool>> methods = new List<Func<State, bool>>();
            foreach (var range in ranges) {
                methods.Add(range_method(targetField, range));
            }
            return (State state) => {
                foreach (var method in methods) {
                    if (!method(state)) {
                        return false;
                    }
                }
                return true;
            };
        }

        private static Func<State, bool> range_method(string targetField, Pair range)
        {
            string identifier = range.First;
            int target_value = (int) range.Second;
            if (identifier == "eq")
            {
                return (State state) =>
                {
                    if (state.HasKey(targetField) && state.GetInt(targetField) != target_value)
                    {
                        return false;
                    }
                    return true;
                };
            } else if (identifier == "lte") {
                return (State state) =>
                {
                    if (state.HasKey(targetField) && state.GetInt(targetField) <= target_value)
                    {
                        return true;
                    }
                    return false;
                };
            } else if (identifier == "gte") {
                return (State state) =>
                {
                    if (state.HasKey(targetField) && state.GetInt(targetField) >= target_value)
                    {
                        return true;
                    }
                    return false;
                };
            }
            throw new ArgumentException(identifier + " is not supported in factor method.");
        }
    }
}
