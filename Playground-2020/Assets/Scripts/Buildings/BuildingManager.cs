using Messaging;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildingPrefabs = null;

    private Building currentBuilding;
    private ResourceManager resourceManager;
    private AgentManager agentManager;
    private List<Building> totalbuildings = new List<Building>();
    private List<ProductionBuilding> productionBuildings = new List<ProductionBuilding>();
    private List<House> houses = new List<House>();
    private bool isBuildingHighlighted = false;

    public List<Building> Buildings { get => buildingPrefabs; }
    public List<ProductionBuilding> ProductionBuildings { get => productionBuildings; }
    public List<House> Houses { get => houses; }

    private void Awake()
    {
        agentManager = FindObjectOfType<AgentManager>();
        resourceManager = FindObjectOfType<ResourceManager>();
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
            if (Input.GetMouseButtonDown(0))
            {
                HandleBuildingHighlight();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!agentManager.SelectedAgent)
                    RemoveBuildingEmployee();
            }
        }

        if (currentBuilding)
            MoveBuilding();
    }

    public void InstantiateBuilding(int index)
    {
        currentBuilding = Instantiate(buildingPrefabs[index]);
        currentBuilding.name = buildingPrefabs[index].name;
        agentManager.Enable(false);
    }

    private void InstantiateBuilding(object index)
    {
        int buildingIndex = (int)index;

        currentBuilding = Building.CreateBuilding(buildingPrefabs[buildingIndex], resourceManager, buildingPrefabs[buildingIndex].name);
        agentManager.Enable(false);
    }

    private void HandleBuildingHighlight()
    {
        if (isBuildingHighlighted)
        {
            MessagingSystem.TriggerEvent("HideBuildingInfo");
            isBuildingHighlighted = false;
            return;
        }

        RaycastHit hit = Utils.GetMouseWorldPoint();
        if (!hit.collider)
            return;

        ProductionBuilding productionBuilding = hit.collider.GetComponent<ProductionBuilding>();
        if (productionBuilding && productionBuildings.Contains(productionBuilding) && !isBuildingHighlighted)
        {
            MessagingSystem.TriggerEvent("ShowBuildingInfo", productionBuilding.Info);
            isBuildingHighlighted = true;
            return;
        }

        House house = hit.collider.GetComponent<House>();
        if (house && houses.Contains(house) && !isBuildingHighlighted)
        {
            MessagingSystem.TriggerEvent("ShowBuildingInfo", house);
            isBuildingHighlighted = true;
            return;
        }
    }

    private void PlaceBuilding()
    {
        resourceManager.BuyItem(currentBuilding.GetCost());
        AddBuilding(currentBuilding);
        currentBuilding.Place();
        currentBuilding = null;
        agentManager.Enable(true);
    }

    private void RemoveBuilding()
    {
        Destroy(currentBuilding.gameObject);
        agentManager.Enable(true);
        currentBuilding = null;
    }

    private void RemoveBuildingEmployee()
    {
        RaycastHit hit = Utils.GetMouseWorldPoint();
        if (!hit.collider)
            return;

        Building building = hit.collider.GetComponent<Building>();
        if (building)
            building.RemoveLastAgent();
    }

    private void MoveBuilding()
    {
        Vector3 targetPosition = Utils.GetMouseWorldPoint().point;
        targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);

        currentBuilding.transform.position = targetPosition;
    }

    private void AddBuilding(Building building)
    {
        totalbuildings.Add(building);

        ProductionBuilding productionBuilding = building.GetComponent<ProductionBuilding>();
        if (productionBuilding)
        {
            productionBuildings.Add(productionBuilding);
            return;
        }
        House house = building.GetComponent<House>();
        if (house)
        {
            houses.Add(house);
            return;
        }
    }
}
