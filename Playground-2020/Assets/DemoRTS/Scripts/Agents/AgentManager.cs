using DemoRTS.Buildings;
using DemoRTS.UI;
using Messaging;
using System.Collections.Generic;
using UnityEngine;

namespace DemoRTS.Agents
{
    public class AgentManager : MonoBehaviour
    {
        [SerializeField] private int startingAgentsCount = 3;
        [SerializeField] private AIController agentPrefab = null;
        [SerializeField] private Transform highlighter = null;
        [SerializeField] private Transform spawnPoint = null;
        [SerializeField] private int spawnCounter = 6;
        [SerializeField] private BuildingManager buildingManager = null;

        private List<AIController> agents = new List<AIController>();
        private bool isEnabled = true;
        private int maxAgentsCapacity;
        private int currentSpawnCounter = 0;

        public AIController SelectedAgent { get; private set; }
        public bool IsEnabled { get => isEnabled; }

        private void Awake()
        {
            Init();
            InvokeRepeating("UpdateAgents", 0f, 2f);
        }

        private void Update()
        {
            if (!isEnabled)
                return;

            if (Input.GetMouseButtonDown(0))
                AgentSelection();
            if (Input.GetMouseButtonDown(1))
                TargetSelection();
        }

        public void Enable(bool isEnabled)
        {
            this.isEnabled = isEnabled;

            if (!this.isEnabled)
                DeselectAgent();
        }

        public void AddAgent(AIController agent)
        {
            agents.Add(agent);
        }

        public void RemoveAgent(AIController agent)
        {
            agents.Remove(agent);
        }

        public void TryDeselectAgent(AIController agent)
        {
            if (agent == SelectedAgent)
                DeselectAgent();
        }

        private void Init()
        {
            maxAgentsCapacity = startingAgentsCount;
            for (int i = 0; i < startingAgentsCount; ++i)
            {
                InstantiateAgent();
            }
        }

        // Spawn new Agent if conditions are met.
        private void UpdateAgents()
        {
            int newMaxCapacity = 0;
            buildingManager.Houses.ForEach(house => newMaxCapacity += house.GetCapacity());
            maxAgentsCapacity = newMaxCapacity;
            currentSpawnCounter += maxAgentsCapacity;

            if (currentSpawnCounter >= spawnCounter)
            {
                currentSpawnCounter = 0;
                InstantiateAgent();
            }

            MessagingSystem.TriggerEvent("UpdatedAgentsCounter", new Vector2(agents.Count, maxAgentsCapacity));
        }

        private void InstantiateAgent()
        {
            if (!(agents.Count + 1 <= maxAgentsCapacity))
                return;

            float randomX = Random.Range(-3f, 3f);
            float randomZ = Random.Range(-3f, 3f);
            Vector3 randomPoint = new Vector3(randomX, 0, randomZ);
            AIController agent = Instantiate(agentPrefab, spawnPoint.position + randomPoint, Quaternion.identity);
            AddAgent(agent);

            UINotificationInfo newNotification = new UINotificationInfo("New Agent Spawned", true);
            MessagingSystem.TriggerEvent("NotificationSent", newNotification);
        }

        private void AgentSelection()
        {
            RaycastHit hit = Utils.GetMouseWorldPoint();
            if (!hit.collider)
                return;

            AIController agent = hit.collider.GetComponent<AIController>();
            HandleAgentSelection(agent);
        }

        private void HandleAgentSelection(AIController agent)
        {
            if (!agent || agent.IsEmployed)
            {
                if (SelectedAgent)
                {
                    DeselectAgent();
                    return;
                }
            }
            else
            {
                /*if there's already a selected agent,
                 * deselect the last one and select the new one.*/
                if (SelectedAgent && agent != SelectedAgent)
                {
                    DeselectAgent();
                    SelectAgent(agent);
                    return;
                }

                if (!SelectedAgent)
                    SelectAgent(agent);
            }
        }

        private void TargetSelection()
        {
            if (!SelectedAgent)
                return;

            RaycastHit hit = Utils.GetMouseWorldPoint();
            if (!hit.collider)
                return;

            Building building = hit.collider.GetComponent<Building>();
            if (building && building.CanEnter())
                SelectedAgent.GoTo(building);
            else
                SelectedAgent.GoTo(hit.point);
        }

        private void SelectAgent(AIController agent)
        {
            //Use a small object to highlight the selected agent.

            SelectedAgent = agent;
            highlighter.parent = SelectedAgent.transform;
            highlighter.position = new Vector3(SelectedAgent.transform.position.x, 0.005f, SelectedAgent.transform.position.z);
        }

        public void DeselectAgent()
        {
            SelectedAgent = null;
            highlighter.parent = transform;
            highlighter.position = new Vector3(0, -20, 0);
        }
    }
}