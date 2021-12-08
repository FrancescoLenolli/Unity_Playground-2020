using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    protected Building building;
    protected AIController agent;

    public Task(Building building, AIController agent)
    {
        this.building = building;
        this.agent = agent;
    }

    public virtual void Start()
    {
        Debug.Log("Start Task");
        EnterBuilding();
    }

    public virtual void End()
    {
        Debug.Log("End Task");
        ExitBuilding();
    }

    private void EnterBuilding()
    {
        agent.transform.parent = building.transform;
        agent.gameObject.SetActive(false);
    }

    private void ExitBuilding()
    {
        agent.gameObject.SetActive(true);
        agent.transform.position = building.GetEntrance() + building.transform.forward * 3;
        agent.transform.parent = null;
    }
}
