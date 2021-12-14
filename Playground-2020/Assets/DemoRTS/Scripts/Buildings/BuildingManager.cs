using DemoRTS.Agents;
using DemoRTS.Resources;
using Messaging;
using System.Collections.Generic;
using UnityEngine;

namespace DemoRTS.Buildings
{
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
            HandleMouseActions();

            if (currentBuilding)
                MoveBuilding();
        }

        public void InstantiateBuilding(int index)
        {
            currentBuilding = Instantiate(buildingPrefabs[index]);
            currentBuilding.name = buildingPrefabs[index].name;
            agentManager.Enable(false);
        }

        private void HandleMouseActions()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (agentManager.IsEnabled)
                    HandleBuildingHighlight();
                else if (currentBuilding && currentBuilding.CanBePlaced())
                    PlaceBuilding();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (agentManager.IsEnabled)
                {
                    if (!agentManager.SelectedAgent)
                        RemoveBuildingEmployee();
                }
                else if (currentBuilding)
                    RemoveBuilding();
            }
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

            if (ShowProductionInfo(hit.collider))
                return;

            if (ShowHouseInfo(hit.collider))
                return;
        }

        private bool ShowProductionInfo(Collider buildingCollider)
        {
            ProductionBuilding productionBuilding = buildingCollider.GetComponent<ProductionBuilding>();
            if (productionBuilding && productionBuildings.Contains(productionBuilding) && !isBuildingHighlighted)
            {
                MessagingSystem.TriggerEvent("ShowBuildingInfo", productionBuilding.Info);
                isBuildingHighlighted = true;
                return true;
            }
            else return false;
        }

        private bool ShowHouseInfo(Collider houseCollider)
        {
            House house = houseCollider.GetComponent<House>();
            if (house && houses.Contains(house) && !isBuildingHighlighted)
            {
                MessagingSystem.TriggerEvent("ShowBuildingInfo", house);
                isBuildingHighlighted = true;
                return true;
            }
            else return false;
        }

        private void PlaceBuilding()
        {
            resourceManager.BuyItem(currentBuilding.Cost);
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
}