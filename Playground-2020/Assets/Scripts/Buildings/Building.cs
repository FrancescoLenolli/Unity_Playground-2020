using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] protected BuildingEntrance entrance = null;

    protected List<AIController> agents = new List<AIController>();

    private bool isPlaced = false;
    private bool canBePlaced = true;
    private NavMeshObstacle navMeshObstacle;

    private void Awake()
    {
        navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        entrance.gameObject.SetActive(false);
        navMeshObstacle.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced)
            return;

        canBePlaced = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlaced)
            return;

        canBePlaced = true;
    }

    public virtual void Work() { }

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

    public bool CanBePlaced()
    {
        return canBePlaced;
    }

    public void Place()
    {
        isPlaced = true;
        entrance.gameObject.SetActive(true);
        navMeshObstacle.enabled = true;
    }
}
