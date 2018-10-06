using System.Collections.Generic;


namespace SimpleBot
{
    public class ConditionConfigBuilder
    {
        private string conditionType;
        private List<ConditionConfig> childConfigs;
        private string targetField;
        private List<Pair> arguments;

        public ConditionConfigBuilder()
        {
            this.conditionType = null;
            this.targetField = null;
            this.childConfigs = new List<ConditionConfig>();
            this.arguments = new List<Pair>();
        }

        public ConditionConfigBuilder AddType(string conditionType) {
            this.conditionType = conditionType;
            return this;
        }

        public ConditionConfigBuilder AddTargetField(string targetField)
        {
            this.targetField = targetField;
            return this;
        }

        public ConditionConfigBuilder AddChild(ConditionConfig config)
        {
            this.childConfigs.Add(config);
            return this;
        }

        public ConditionConfigBuilder AddArgument(Pair argument)
        {
            this.arguments.Add(argument);
            return this;
        }

        public ConditionConfig Build() {
            if (targetField == null) {
                return new ConditionConfig(this.conditionType, this.childConfigs);
            } else {
                return new ConditionConfig(this.conditionType, this.targetField, this.arguments);
            }
        }
    }
}
