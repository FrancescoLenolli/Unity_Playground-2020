using DemoRTS.Agents;
using DemoRTS.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoRTS.Buildings
{
    [System.Serializable]
    public class ProductionBuildingInfo
    {
        public int jobsCounter = 1;
        public ResourceType resourceProduced = ResourceType.Food;
        public float maxProduction = 100f;

        [HideInInspector] public string name = "";
        [HideInInspector] public List<AIController> agentsEmployed = new List<AIController>();
        [HideInInspector] public int jobsOccupied { get => agentsEmployed.Count; }
        [HideInInspector] public int jobsFree { get => jobsCounter - jobsOccupied; }
        [HideInInspector] public float efficiency { get => GetEfficiency(jobsOccupied, jobsCounter); }
        [HideInInspector] public float currentProduction { get => PercentOf(efficiency, maxProduction); }
        [HideInInspector] public bool isFull { get => jobsFree == 0; }

        private float GetEfficiency(float value, float maxValue)
        {
            return Mathf.Round(value * 100 / maxValue);
        }

        private float PercentOf(float percentage, float value)
        {
            return Mathf.Round(percentage * value / 100);
        }

        public override string ToString()
        {
            string result = $"Resource Produced: {resourceProduced}\nTotal Jobs: {jobsCounter}\nJobs occupied: {jobsOccupied}\n" +
                $"Efficiency: {efficiency}%\nCurrent Production: {currentProduction}/h";

            return result;
        }
    }
}
