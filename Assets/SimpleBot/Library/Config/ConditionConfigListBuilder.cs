using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleBot
{
    public class ConditionConfigListBuilder
    {
        private List<ConditionConfig> configs;

        public ConditionConfigListBuilder AddCondition(ConditionConfig condition)
        {
            this.configs.Add(condition);
            return this;
        }

        public List<ConditionConfig> build() {
            return configs;
        }
    }
}