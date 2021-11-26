using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private Transform highlighter = null;
    private List<AIController> agents = new List<AIController>();
    private AIController selectedAgent = null;
    private bool isEnabled = true;

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

    public void TryDeselectAgent(AIController agent)
    {
        if (agent == selectedAgent)
            DeselectAgent();
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

            if (!agent)
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
                selectedAgent.GoTo(building ? building.GetEntrance() : hit.point);
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
