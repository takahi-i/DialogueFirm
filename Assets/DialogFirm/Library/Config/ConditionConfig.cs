using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogFirm
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

        private readonly List<DialogFirm.Pair> arguments;

        public List<DialogFirm.Pair> Arguments
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
            this.arguments = new List<DialogFirm.Pair>();
        }

        public ConditionConfig(string conditionType, string targetField, List<DialogFirm.Pair> arguments)
        {
            this.condtionType = conditionType;
            this.childConfigs = new List<ConditionConfig>();
            this.targetField = targetField;
            this.arguments = arguments;
        }
    }
}
