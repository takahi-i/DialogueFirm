using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponderConfig
{
    private string target;
    private List<ConditionConfig> conditions;
    private List<string> responds;

    public string Target
    {
        get
        {
            return target;
        }
    }

    public List<string> Responds
    {
        get
        {
            return responds;
        }
    }

    public ResponderConfig(string target, List<string> responds)
    {
        this.target = target;
        this.responds = responds;
        this.conditions = new List<ConditionConfig>();
    }

    public ResponderConfig(string target, List<string> responds, List<ConditionConfig> conditions) : this(target, responds)
    {
        this.conditions = conditions;
    }

    public void AddCondtion(ConditionConfig condition)
    {
        this.conditions.Add(condition);
    }
}
