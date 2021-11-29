using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected BuildingEntrance entrance = null;
    protected List<AIController> agents = new List<AIController>();

    public virtual void Work()
    {
        Debug.Log("Base Work() function called");
    }

    public virtual bool CanEnter()
    {
        Debug.Log("Base CanEnter() function called, return value set to FALSE");
        return false;
    }

    public virtual void AddAgent(AIController agent)
    {
        agents.Add(agent);
        agent.transform.parent = transform;
        agent.gameObject.SetActive(false);
    }

    public virtual void RemoveAgent(AIController agent)
    {
        agents.Remove(agent);
        agent.gameObject.SetActive(true);
        agent.transform.position = GetEntrance() + transform.forward * 3;
        agent.transform.parent = null;
    }

    public Vector3 GetEntrance()
    {
        return entrance.transform.position;
    }
}
