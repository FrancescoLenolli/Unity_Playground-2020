using DemoRTS.Agents;
using UnityEngine;

namespace DemoRTS.Buildings
{
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
                if (!building.CanEnter() || agent.TargetBuilding != building)
                    return;

                agentManager.TryDeselectAgent(agent);
                building.AddAgent(agent);
            }
        }
    }
}