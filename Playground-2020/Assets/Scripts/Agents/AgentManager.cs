using Messaging;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private int startingAgentsCount = 3;
    [SerializeField] private AIController agentPrefab = null;
    [SerializeField] private Transform highlighter = null;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private int spawnCounter = 6;
    [SerializeField] private BuildingManager buildingManager = null;

    private List<AIController> agents = new List<AIController>();
    private AIController selectedAgent = null;
    private bool isEnabled = true;
    private int maxAgentsCapacity;
    private int currentSpawnCounter = 0;

    private void Awake()
    {
        Init();
        InvokeRepeating("UpdateAgents", 0f, 2f);
    }

    private void Update()
    {
        if (!isEnabled)
            return;

        AgentSelection();
        TargetSelection();
    }

    public void Enable(bool isEnabled)
    {
        this.isEnabled = isEnabled;

        if (!this.isEnabled)
            DeselectAgent();
    }

    public bool IsEnabled()
    {
        return isEnabled;
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
        if (agent == selectedAgent)
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

    private void UpdateAgents()
    {
        int newMaxCapacity = 0;
        buildingManager.Houses.ForEach(house => newMaxCapacity += house.GetCapacity());
        maxAgentsCapacity = newMaxCapacity;
        currentSpawnCounter += maxAgentsCapacity;

        if(currentSpawnCounter >= spawnCounter)
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
    }

    private void AgentSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;

            AIController agent = hit.collider.GetComponent<AIController>();

            if (!agent || agent.IsEmployed)
            {
                if (selectedAgent)
                {
                    DeselectAgent();
                    return;
                }
            }
            else
            {
                if (selectedAgent && agent != selectedAgent)
                {
                    DeselectAgent();
                    SelectAgent(agent);
                    return;
                }

                if (!selectedAgent)
                    SelectAgent(agent);
            }

        }
    }

    private void TargetSelection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!selectedAgent)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;

            Building building = hit.collider.GetComponent<Building>();

            if (building)
            {
                if(building.CanEnter())
                    selectedAgent.GoTo(building.GetEntrance());
            }
            else
            {
                selectedAgent.GoTo(hit.point);
            }
        }
    }

    private void SelectAgent(AIController agent)
    {
        selectedAgent = agent;
        highlighter.parent = selectedAgent.transform;
        highlighter.position = new Vector3(selectedAgent.transform.position.x, 0.005f, selectedAgent.transform.position.z);
    }

    public void DeselectAgent()
    {
        selectedAgent = null;
        highlighter.parent = transform;
        highlighter.position = new Vector3(0, -20, 0);
    }
}
