using DemoRTS.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace DemoRTS.UI
{
    public class UIView_BuildingsMenu_Main : UIView
    {
        [SerializeField] private Button buttonPrefab = null;
        [SerializeField] private Transform buttonsContainer = null;

        private Action<int> onSelectBuilding;

        public Action<int> OnSelectBuilding { get => onSelectBuilding; set => onSelectBuilding = value; }

        public void SpawnButtons(List<Building> buildings)
        {
            for (int i = 0; i < buildings.Count; ++i)
            {
                int index = i;
                Button newButton = Instantiate(buttonPrefab, buttonsContainer);
                newButton.onClick.AddListener(() => { SelectBuilding(index); });
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{buildings[i].name}\n{buildings[i].Cost}";
            }
        }

        public void SelectBuilding(int index)
        {
            onSelectBuilding?.Invoke(index);
        }
    }
}