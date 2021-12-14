using DemoRTS.Agents;
using DemoRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoRTS.Tasks
{
    public class TaskChopTree : Task
    {
        MonoBehaviour concreteObject;

        public TaskChopTree(Building building, AIController agent) : base(building, agent)
        {
            concreteObject = agent;
        }

        public override void Start()
        {
            concreteObject.StartCoroutine(GoToRandomTreeRoutine());
        }

        public override void End()
        {

        }

        // TODO: replace random tree with random tree in nearest forest when increasing map size.
        public IEnumerator GoToRandomTreeRoutine()
        {
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            int randomIndex = Random.Range(0, trees.Length);
            agent.GoTo(trees[randomIndex].transform.position);

            yield return null;
        }
    }
}
