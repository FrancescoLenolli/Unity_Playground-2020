using DemoRTS.Agents;
using DemoRTS.Resources;
using DemoRTS.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DemoRTS.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] protected Cost cost = new Cost();
        [SerializeField] protected BuildingEntrance entrance = null;

        protected List<AIController> agents = new List<AIController>();
        protected bool isOverlapping = false;

        private ResourceManager resourceManager;
        private NavMeshObstacle navMeshObstacle;

        public bool IsPlaced { get; private set; }
        public Cost Cost { get => cost; }
        public Vector3 Entrance { get => entrance.transform.position; }

        public static Building CreateBuilding(Building prefab, ResourceManager resourceManager, string name)
        {
            Building newBuilding = Instantiate(prefab);
            newBuilding.resourceManager = resourceManager;
            newBuilding.name = name;
            newBuilding.Init();

            return newBuilding;
        }

        public virtual void ShowDetails() { }
        public virtual bool CanBePlaced() { return !isOverlapping && resourceManager && resourceManager.CanAffordItem(cost); }
        public virtual bool CanEnter() { return false; }
        public virtual Task GetTask(AIController agent) { return new TaskEnterBuilding(this, agent); }

        public virtual void AddAgent(AIController agent)
        {
            agents.Add(agent);
            agent.StartTask(GetTask(agent));
        }

        public virtual void RemoveSpecificAgent(AIController agent)
        {
            RemoveAgent(agent);
        }

        public virtual AIController RemoveLastAgent()
        {
            if (agents.Count <= 0)
                return null;

            AIController lastAgent = agents[agents.Count - 1];
            RemoveAgent(lastAgent);

            return lastAgent;
        }

        public virtual void RemoveAgentAt(int index)
        {
            if (index >= 0 && index < agents.Count)
            {
                AIController agent = agents[index];
                RemoveAgent(agent);
            }
        }

        protected virtual void Init()
        {
            navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
            entrance.gameObject.SetActive(false);
            navMeshObstacle.enabled = false;
            StartCoroutine(CheckOverlapRoutine());
        }

        public void Place()
        {
            IsPlaced = true;
            entrance.gameObject.SetActive(true);
            navMeshObstacle.enabled = true;
        }

        private void RemoveAgent(AIController agent)
        {
            agents.Remove(agent);
            agent.EndTask();
        }

        private IEnumerator CheckOverlapRoutine()
        {
            // OverlapBox is weird, it doesn't reproduce the exact scale of the object.
            float scaleMultiplier = 2.3f;

            while (!IsPlaced)
            {
                List<Collider> colliders = Physics.OverlapBox(transform.position, transform.localScale * scaleMultiplier, Quaternion.identity).ToList();
                colliders.RemoveAll(collider => collider.gameObject.CompareTag("Terrain") || collider.gameObject == gameObject);

                int overlapCount = colliders.Count;
                isOverlapping = overlapCount > 0;

                yield return null;
            }
        }
    }
}