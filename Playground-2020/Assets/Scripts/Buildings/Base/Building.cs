using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] protected BuildingEntrance entrance = null;

    protected List<AIController> agents = new List<AIController>();

    protected bool isOverlapping = false;

    private bool isPlaced = false;
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

    public virtual void Work() { }
    public virtual void ShowDetails() { }
    public virtual bool CanBePlaced() { return !isOverlapping; }
    public virtual bool CanEnter() { return false; }

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
