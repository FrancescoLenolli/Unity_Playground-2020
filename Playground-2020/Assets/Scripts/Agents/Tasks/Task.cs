using UnityEngine;

/// <summary>
/// Handles what an employed agent does.
/// </summary>
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
