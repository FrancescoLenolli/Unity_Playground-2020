using DemoRTS.Buildings;
using DemoRTS.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace DemoRTS.Agents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Animator animator;
        private Task task;

        public bool IsEmployed { get => task != null; }
        public Building TargetBuilding { get; set; }

        public void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            // TODO: Separate class to handle more animations.

            if (agent.velocity != Vector3.zero)
                animator.SetBool("IsWalking", true);
            else
                animator.SetBool("IsWalking", false);
        }

        public void GoTo(Vector3 target)
        {
            agent.SetDestination(target);
            TargetBuilding = null;
        }

        public void GoTo(Building building)
        {
            agent.SetDestination(building.Entrance);
            TargetBuilding = building;
        }

        public void StartTask(Task newTask)
        {
            TargetBuilding = null;
            task = newTask;
            task.Start();
        }

        public void EndTask()
        {
            task.End();
            task = null;
        }
    }
}