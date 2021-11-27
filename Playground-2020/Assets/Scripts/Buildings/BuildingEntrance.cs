using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEntrance : MonoBehaviour
{
    [SerializeField] private Building building = null;

    private AgentManager agentManager;

    private void Awake()
    {
        agentManager = FindObjectOfType<AgentManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AIController agent = other.GetComponent<AIController>();
        if (agent)
        {
            if(building.isFull())
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
