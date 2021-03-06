using DemoRTS.Agents;
using DemoRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoRTS.Tasks
{
    public class TaskEnterBuilding : Task
    {
        public TaskEnterBuilding(Building building, AIController agent) : base(building, agent) { }

        public override void Start()
        {
            EnterBuilding();
        }

        public override void End()
        {
            ExitBuilding();
        }

        private void EnterBuilding()
        {
            agent.transform.parent = building.transform;
            agent.gameObject.SetActive(false);
        }

        private void ExitBuilding()
        {
            agent.gameObject.SetActive(true);
            agent.transform.position = building.Entrance - building.transform.forward * 3;
            agent.transform.parent = null;
        }
    }
}