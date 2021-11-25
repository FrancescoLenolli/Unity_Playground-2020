using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public Transform target = null;

    private List<AIController> agents = new List<AIController>();

    private void Start()
    {
        agents = FindObjectsOfType<AIController>().ToList();
    }

    private void Update()
    {
        for (int i = 0; i < agents.Count; ++i)
            agents[i].UpdatePosition(agents, target);
    }
}
