using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public virtual AIController RemoveLastAgent()
    {
        if (agents.Count <= 0)
            return null;

        AIController lastAgent = agents[agents.Count - 1];
        agents.Remove(lastAgent);
        lastAgent.EndTask();

        return lastAgent;
    }

    protected virtual void Init()
    {
        navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        entrance.gameObject.SetActive(false);
        navMeshObstacle.enabled = false;
        StartCoroutine(CheckOverlapRoutine());
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

    private IEnumerator CheckOverlapRoutine()
    {
        // OverlapBox is weird, it doesn't reproduce the exact scale of the object.
        float scaleMultiplier = 2.3f;

        while (!isPlaced)
        {
            List<Collider> colliders = Physics.OverlapBox(transform.position, transform.localScale * scaleMultiplier, Quaternion.identity).ToList();
            colliders.RemoveAll(collider => collider.gameObject.CompareTag("Terrain") || collider.gameObject == gameObject);

            int overlapCount = colliders.Count;
            isOverlapping = overlapCount > 0;

            yield return null;
        }

        yield return null;
    }
}
