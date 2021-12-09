using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

public class Building : MonoBehaviour
{
    [SerializeField] protected Cost cost = new Cost();
    [SerializeField] protected BuildingEntrance entrance = null;

    protected List<AIController> agents = new List<AIController>();
    protected bool isOverlapping = false;

    private bool isPlaced = false;
    private ResourceManager resourceManager = null;
    private NavMeshObstacle navMeshObstacle;

    public bool IsPlaced { get => isPlaced; }

    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced || other.CompareTag("Terrain"))
            return;

        isOverlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlaced || other.CompareTag("Terrain"))
            return;

        isOverlapping = false;
    }

    public static Building CreateBuilding(Building prefab, ResourceManager resourceManager, string name)
    {
        Building newBuilding = Instantiate(prefab);
        newBuilding.resourceManager = resourceManager;
        newBuilding.name = name;

        return newBuilding;
    }

    public virtual void ShowDetails() { }
    public virtual bool CanBePlaced() { return !isOverlapping && resourceManager && resourceManager.CanAffordItem(cost); }
    public virtual bool CanEnter() { return false; }
    public virtual Task GetTask(AIController agent) { return new TaskEnterBuilding(this, agent); }

    public virtual void AddAgent(AIController agent)
    {
        agents.Add(agent);
        agent.StartTask(GetTask(agent));
    }

    public virtual void RemoveAgent(AIController agent)
    {
        agents.Remove(agent);
        agent.EndTask();
    }

    public Cost GetCost()
    {
        return cost;
    }

    public Vector3 GetEntrance()
    {
        return entrance.transform.position;
    }

    public void Place()
    {
        isPlaced = true;
        entrance.gameObject.SetActive(true);
        navMeshObstacle.enabled = true;
    }

    protected virtual void Init()
    {
        navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        entrance.gameObject.SetActive(false);
        navMeshObstacle.enabled = false;
    }
}
