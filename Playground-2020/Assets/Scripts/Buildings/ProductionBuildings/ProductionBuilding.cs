using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : Building
{
    [SerializeField] private ProductionBuildingInfo info;

    public ProductionBuildingInfo Info { get => info; }

    private void Awake()
    {
        info.name = name;
    }

    public override bool CanEnter()
    {
        return !info.isFull;
    }

    public override void AddAgent(AIController agent)
    {
        base.AddAgent(agent);
        info.agentsEmployed.Add(agent);
    }

    public override void RemoveAgent(AIController agent)
    {
        base.RemoveAgent(agent);
        info.agentsEmployed.Remove(agent);
    }

    public override AIController RemoveLastAgent()
    {
        AIController lastAgent = base.RemoveLastAgent();
        if (!lastAgent)
            return null;

        info.agentsEmployed.Remove(lastAgent);
        return lastAgent;
    }

    public ResourceType GetResource()
    {
        return info.resourceProduced;
    }

    public float GetCurrentProduction()
    {
        return info.currentProduction;
    }

    public override void ShowDetails()
    {
        Debug.Log(info.ToString());
    }
}
