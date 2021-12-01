using Messaging;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildingPrefabs = null;

    private Building currentBuilding = null;
    private AgentManager agentManager = null;
    private List<Building> totalbuildings = new List<Building>();
    private List<ProductionBuilding> productionBuildings = new List<ProductionBuilding>();

    public List<Building> Buildings { get => buildingPrefabs; }
    public List<ProductionBuilding> ProductionBuildings { get => productionBuildings; }

    private void Awake()
    {
        agentManager = FindObjectOfType<AgentManager>();
        MessagingSystem.StartListening("SelectBuilding", InstantiateBuilding);
    }

    private void Update()
    {
        if (!agentManager.IsEnabled())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBuilding && currentBuilding.CanBePlaced())
                    PlaceBuilding();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (currentBuilding)
                    RemoveBuilding();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider collider = GetMouseWorldPoint().collider;
                if (!collider)
                    return;

                Building building = collider.GetComponent<Building>();
                if (building)
                    building.ShowDetails();
            }
        }

        if (currentBuilding)
            MoveBuilding();
    }

    public void InstantiateBuilding(int index)
    {
        currentBuilding = Instantiate(buildingPrefabs[index]);
        agentManager.Enable(false);
    }

    private void InstantiateBuilding(object index)
    {
        int buildingIndex = (int)index;
        currentBuilding = Instantiate(buildingPrefabs[buildingIndex]);
        agentManager.Enable(false);
    }

    private void PlaceBuilding()
    {
        AddBuilding(currentBuilding);
        currentBuilding.Place();
        currentBuilding = null;
        agentManager.Enable(true);
    }

    private void RemoveBuilding()
    {
        Destroy(currentBuilding.gameObject);
        currentBuilding = null;
    }

    private void MoveBuilding()
    {
        Vector3 targetPosition = GetMouseWorldPoint().point;
        targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);

        currentBuilding.transform.position = targetPosition;
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

    private RaycastHit GetMouseWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }
}
