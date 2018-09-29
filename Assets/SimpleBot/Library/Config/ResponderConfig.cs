using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponderConfig
{
    private string target;
    public string Target
    {
        get
        {
            return target;
        }
    }

    private List<string> responds;
    private JsonNode condition;

    public List<string> Responds
    {
        get
        {
            return responds;
        }
    }

    public ResponderConfig(string target, List<string> responds, JsonNode condition)
    {
        this.target = target;
        this.responds = responds;
        this.condition = condition;
    }
}
