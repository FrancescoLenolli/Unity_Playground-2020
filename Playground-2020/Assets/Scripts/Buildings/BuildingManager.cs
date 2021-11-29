using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildingPrefabs = null;

    private Building currentBuilding = null;
    private AgentManager agentManager = null;
    private List<Building> totalbuildings = new List<Building>();
    private List<ProductionBuilding> productionBuildings = new List<ProductionBuilding>();

    public int buildingsCount { get => buildingPrefabs.Count; }
    public List<ProductionBuilding> ProductionBuildings { get => productionBuildings; }

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
                    AddBuilding(currentBuilding);
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

    private void AddBuilding(Building building)
    {

        totalbuildings.Add(building);

        ProductionBuilding productionBuilding = (ProductionBuilding)building;
        if (productionBuilding)
        {
            productionBuildings.Add(productionBuilding);
            return;
        }
    }
}
