using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponderConfigs {
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

    public ResponderConfigs(string target, List<string> responds)
    {
        this.target = target;
        this.responds = responds;
    }
}
