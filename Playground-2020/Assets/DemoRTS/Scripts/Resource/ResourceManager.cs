using DemoRTS.Buildings;
using DemoRTS.UI;
using Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DemoRTS.Resources
{
    public enum ResourceType { Food = 0, Wood = 1, Stone = 2, Gold = 3 }

    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private BuildingManager buildingManager = null;
        [SerializeField] private List<Resource> startingResources = new List<Resource>();

        private List<Resource> resources = new List<Resource>();

        private void Awake()
        {
            resources = startingResources;
            InvokeRepeating("GetTotalProduction", 0f, 2f);
        }

        public bool CanAffordItem(Cost itemCost)
        {
            foreach (Resource requiredResource in itemCost.requiredResources)
            {
                Resource resource = resources.Find(x => x.GetType() == requiredResource.GetType());
                if (resource.GetValue() < requiredResource.GetValue())
                    return false;
            }

            return true;
        }

        public void BuyItem(Cost itemCost)
        {
            foreach (Resource requiredResource in itemCost.requiredResources)
            {
                Resource resource = resources.Find(x => x.GetType() == requiredResource.GetType());
                resource.AddValue(-requiredResource.GetValue());
            }

            MessagingSystem.TriggerEvent("ResourcesUpdated", resources);
        }

        private void GetResources()
        {
            int resourcesCount = Enum.GetNames(typeof(ResourceType)).Length;

            for (int i = 0; i < resourcesCount; ++i)
            {
                ResourceType type = (ResourceType)(Enum.GetValues(typeof(ResourceType)).GetValue(i));
                resources.Add(new Resource(type, 0));
            }
        }

        private void GetTotalProduction()
        {
            float[] totalProduction = new float[4];

            foreach (ProductionBuilding building in buildingManager.ProductionBuildings)
            {
                int index = Array.IndexOf(Enum.GetValues(typeof(ResourceType)), building.GetResource());
                totalProduction[index] += building.GetCurrentProduction();
            }

            for (int i = 0; i < resources.Count; ++i)
                resources[i].AddValue(totalProduction[i]);

            MessagingSystem.TriggerEvent("ResourcesUpdated", resources);

            UINotificationInfo newNotification = new UINotificationInfo("Resources Gathered", false);
            MessagingSystem.TriggerEvent("NotificationSent", newNotification);
        }
    }
}