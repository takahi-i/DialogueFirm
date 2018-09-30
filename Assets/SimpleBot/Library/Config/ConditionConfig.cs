using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionConfig {
    private List<ConditionConfig> childConfigs;

    public ConditionConfig(List<ConditionConfig> childConfigs)
    {
        this.childConfigs = childConfigs;
    }
}
