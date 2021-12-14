using DemoRTS.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

namespace DemoRTS.UI
{
    public class UIView_BuildingInfoMenu_Main : UIView
    {
        [SerializeField] private List<TextMeshProUGUI> infoLabels = new List<TextMeshProUGUI>();
        [SerializeField] private TextMeshProUGUI buildingName = null;
        [SerializeField] private TextMeshProUGUI buildingResource = null;
        [SerializeField] private TextMeshProUGUI buildingEmployees = null;
        [SerializeField] private TextMeshProUGUI buildingEmploymentStatus = null;
        [SerializeField] private TextMeshProUGUI buildingEfficiency = null;
        [SerializeField] private TextMeshProUGUI buildingProduction = null;

        public override void ShowView()
        {
            base.ShowView();
            CleanLabels();
        }

        public override void HideView()
        {
            CleanLabels();
            base.HideView();
        }

        public void SetInfo(object info)
        {
            ProductionBuildingInfo buildingInfo = null;
            try { buildingInfo = (ProductionBuildingInfo)info; }
            catch (InvalidCastException) { }
            if (buildingInfo != null)
            {
                ShowProductionInfo(buildingInfo);
                return;
            }

            House house = null;
            try { house = (House)info; }
            catch (InvalidCastException) { }
            if (house)
            {
                ShowHouseInfo(house);
                return;
            }
        }

        public void CleanLabels()
        {
            infoLabels.ForEach(label => label.text = "");
        }

        private void ShowProductionInfo(ProductionBuildingInfo buildingInfo)
        {
            string employeeNames = "";
            for (int i = 0; i < buildingInfo.agentsEmployed.Count; ++i)
            {
                employeeNames += buildingInfo.agentsEmployed[i].name;
                if (i < buildingInfo.agentsEmployed.Count - 1)
                    employeeNames += ", ";
            }

            buildingName.text = buildingInfo.name;
            buildingResource.text = buildingInfo.resourceProduced.ToString();
            buildingEmployees.text = employeeNames;
            buildingEmploymentStatus.text = $"Employment:\n{buildingInfo.jobsOccupied}/{buildingInfo.jobsCounter}";
            buildingEfficiency.text = $"Efficiency: {buildingInfo.efficiency}%";
            buildingProduction.text = $"Current production: {buildingInfo.currentProduction}/hr";
        }

        private void ShowHouseInfo(House house)
        {
            buildingName.text = house.name;
            buildingEmployees.text = $"Capacity: {house.GetCapacity()}";
        }

    }
}