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
    }

    public virtual void End()
    {
        Debug.Log("End Task");
    }
}
