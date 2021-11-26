using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingInfo
{
    public int jobsCounter = 1;
    public ResourceType resourceProduced = ResourceType.Food;
    public float maxProduction = 100f;
    public BuildingEntrance entrance = null;

    [HideInInspector] public List<AIController> agentsEmployed = new List<AIController>();
    [HideInInspector] public int jobsOccupied { get => agentsEmployed.Count; }
    [HideInInspector] public int jobsFree { get => jobsCounter - jobsOccupied; }
    [HideInInspector] public float efficiency { get => GetEfficiency(jobsOccupied, jobsCounter); }
    [HideInInspector] public float currentProduction { get => PercentOf(efficiency, maxProduction); }

    private float GetEfficiency(float value, float maxValue)
    {
        return Mathf.Round(value * 100 / maxValue);
    }

    private float PercentOf(float percentage, float value)
    {
        return Mathf.Round(percentage * value / 100);
    }
}
