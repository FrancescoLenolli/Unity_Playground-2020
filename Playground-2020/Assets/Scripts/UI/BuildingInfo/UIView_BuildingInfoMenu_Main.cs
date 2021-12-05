using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_BuildingInfoMenu_Main : UIView
{
    [SerializeField] private TextMeshProUGUI buildingName = null;
    [SerializeField] private TextMeshProUGUI buildingResource = null;
    [SerializeField] private TextMeshProUGUI buildingEmployees = null;
    [SerializeField] private TextMeshProUGUI buildingEmploymentStatus = null;
    [SerializeField] private TextMeshProUGUI buildingEfficiency = null;
    [SerializeField] private TextMeshProUGUI buildingProduction = null;

    public void SetInfo(ProductionBuildingInfo buildingInfo)
    {
        string employeeNames = "";
        for(int i = 0; i < buildingInfo.agentsEmployed.Count; ++i)
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

}
