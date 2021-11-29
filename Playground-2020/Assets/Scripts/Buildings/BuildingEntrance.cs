using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEntrance : MonoBehaviour
{
    private Building building;
    private AgentManager agentManager;

    private void Awake()
    {
        building = GetComponentInParent<Building>();
        agentManager = FindObjectOfType<AgentManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AIController agent = other.GetComponent<AIController>();
        if (agent)
        {
            if(!building.CanEnter())
            {
                Debug.Log($"{building.name} is full!");
                return;
            }

            agent.Stop();
            agentManager.TryDeselectAgent(agent);
            building.AddAgent(agent);
        }
    }
}
