using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DemoRTS.Buildings
{
    public class StoneMason : ProductionBuilding
    {
        [SerializeField] private float range = 1f;

        private List<GameObject> stones = new List<GameObject>();

        public override bool CanBePlaced()
        {
            return !isOverlapping && AnyStonesInRange();
        }

        protected override void Init()
        {
            base.Init();
            stones = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Stone")).ToList();
        }

        private bool AnyStonesInRange()
        {
            foreach (GameObject stone in stones)
            {
                float distance = Vector3.Distance(stone.transform.position, transform.position);
                if (distance <= range)
                    return true;
            }

            return false;
        }
    }
}