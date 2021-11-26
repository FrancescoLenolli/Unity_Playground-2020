using System;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Food, Wood, Stone, Gold }
public class Building : MonoBehaviour
{
    [SerializeField] protected BuildingInfo info;

    public virtual float Produce()
    {
        Debug.Log("Base method called.");
        return 0f;
    }

    public void AddAgent(AIController agent)
    {
        info.agentsEmployed.Add(agent);
        agent.transform.parent = transform;
        agent.gameObject.SetActive(false);
    }

    public void RemoveAgent(AIController agent)
    {
        info.agentsEmployed.Remove(agent);
        agent.gameObject.SetActive(true);
        agent.transform.position = GetEntrance() + transform.forward * 3;
        agent.transform.parent = null;
    }

    public ResourceType GetResource()
    {
        return info.resourceProduced;
    }

    public Vector3 GetEntrance()
    {
        return info.entrance.transform.position;
    }
}
