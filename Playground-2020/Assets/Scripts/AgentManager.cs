using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
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
    }

    public bool IsEnabled()
    {
        return isEnabled;
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

            if (Physics.Raycast(ray, out hit))
                selectedAgent.GoTo(hit.point);
        }
    }

    private void SelectAgent(AIController agent)
    {
        selectedAgent = agent;
        Debug.Log("Agent selected");
    }

    private void DeselectAgent()
    {
        selectedAgent = null;
        Debug.Log("Agent deselected");
    }
}
