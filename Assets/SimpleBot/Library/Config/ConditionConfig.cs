using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionConfig {
    private string condtionType;
    private List<ConditionConfig> childConfigs;

    public ConditionConfig(string conditionType, List<ConditionConfig> childConfigs)
    {
        this.condtionType = conditionType;
        this.childConfigs = childConfigs;
    }
}
