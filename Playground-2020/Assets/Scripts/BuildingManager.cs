using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform buildingPrefab = null;

    private Transform currentBuilding = null;
    private AgentManager agentManager = null;

    private void Awake()
    {
        agentManager = FindObjectOfType<AgentManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBuilding = Instantiate(buildingPrefab);
            agentManager.Enable(false);
        }

        if (!agentManager.IsEnabled())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBuilding)
                {
                    currentBuilding = null;
                    agentManager.Enable(true);
                }
            }
        }

        if(currentBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 targetPosition = hit.point;
            targetPosition = new Vector3(targetPosition.x, .5f, targetPosition.z);

            currentBuilding.position = targetPosition;
        }
    }
}
