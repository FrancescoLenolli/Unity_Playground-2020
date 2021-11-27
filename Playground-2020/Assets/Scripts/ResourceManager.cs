using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private BuildingManager buildingManager = null;

    private List<Resource> resources = new List<Resource>();

    private void Awake()
    {
        int resourcesCount = Enum.GetNames(typeof(ResourceType)).Length;

        for(int i = 0; i < resourcesCount; ++i)
        {
            ResourceType type = (ResourceType)(Enum.GetValues(typeof(ResourceType)).GetValue(i));
            resources.Add(new Resource(type, 0));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetTotalProduction();
        }
    }

    private void GetTotalProduction()
    {
        float[] totalProduction = new float[4];

        foreach(Building building in buildingManager.Buildings)
        {
            int index = Array.IndexOf(Enum.GetValues(typeof(ResourceType)), building.GetResource());
            totalProduction[index] += building.Produce();
        }

        for (int i = 0; i < resources.Count; ++i)
            resources[i].AddValue(totalProduction[i]);
    }
}
