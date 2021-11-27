using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    private ResourceType type;
    private float value;

    public Resource(ResourceType type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public new ResourceType GetType()
    {
        return type;
    }
    public float GetValue()
    {
        return value;
    }

    public void AddValue(float value)
    {
        this.value += value;
    }
}
