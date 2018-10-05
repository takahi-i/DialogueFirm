using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot
{
    public class ConditionConfig
    {
        private readonly string condtionType;

        public string CondtionType
        {
            get
            {
                return condtionType;
            }
        }

        private readonly List<ConditionConfig> childConfigs;

        public List<ConditionConfig> ChildConfigs
        {
            get
            {
                return childConfigs;
            }
        }

        private readonly string targetField;

        public string TargetField
        {
            get
            {
                return targetField;
            }
        }

        private readonly List<SimpleBot.Pair> arguments;

        public List<SimpleBot.Pair> Arguments
        {
            get
            {
                return arguments;
            }
        }

        public ConditionConfig(string conditionType, List<ConditionConfig> childConfigs)
        {
            this.condtionType = conditionType;
            this.childConfigs = childConfigs;
            this.targetField = null;
            this.arguments = new List<SimpleBot.Pair>();
        }

        public ConditionConfig(string conditionType, string targetField, List<SimpleBot.Pair> arguments)
        {
            this.condtionType = conditionType;
            this.childConfigs = new List<ConditionConfig>();
            this.targetField = targetField;
            this.arguments = arguments;
        }
    }
}
