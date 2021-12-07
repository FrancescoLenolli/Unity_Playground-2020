using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    [SerializeField] private ResourceType type = ResourceType.Food;
    [SerializeField] private float value = 100f;

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
