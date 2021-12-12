using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cost
{
    public List<Resource> requiredResources = new List<Resource>();

    public override string ToString()
    {
        string cost = "";
        requiredResources.ForEach(resource => cost += "\n" + resource.ToString());

        return cost;
    }
}
