using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionConfig {
    private string condtionType;
    private List<ConditionConfig> childConfigs;
    private string targetField;
    private List<SimpleBot.Pair> arguments;

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
