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
    public List<string> Responds
    {
        get
        {
            return responds;
        }
    }

    private string type;
    public string Type
    {
        get
        {
            return type;
        }
    }

    public ResponderConfig(string target, List<string> responds, string type = "simple")
    {
        this.target = target;
        this.responds = responds;
        this.type = type;
    }
}
