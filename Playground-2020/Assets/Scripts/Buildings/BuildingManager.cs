using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildingPrefabs = null;

    private Building currentBuilding = null;
    private AgentManager agentManager = null;
    private List<Building> buildings = new List<Building>();

    public int buildingsCount { get => buildingPrefabs.Count; }
    public List<Building> Buildings { get => buildings; }

    private void Awake()
    {
        agentManager = FindObjectOfType<AgentManager>();
    }

    private void Update()
    {
        if (!agentManager.IsEnabled())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBuilding)
                {
                    buildings.Add(currentBuilding);
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

            currentBuilding.transform.position = targetPosition;
        }
    }

    public void InstantiateBuilding(int index)
    {
        currentBuilding = Instantiate(buildingPrefabs[index]);
        agentManager.Enable(false);
    }
}
