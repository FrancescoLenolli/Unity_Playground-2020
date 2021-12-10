using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    [HideInInspector] public Building targetBuilding;

    private NavMeshAgent agent;
    private Animator animator;
    private Task task;

    public bool IsEmployed { get => task != null; }

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // TODO: Separate class to handle more animations.
        if(agent.velocity != Vector3.zero)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void GoTo(Vector3 target)
    {
        agent.SetDestination(target);
        targetBuilding = null;
    }

    public void GoTo(Building building)
    {
        agent.SetDestination(building.GetEntrance());
        targetBuilding = building;
    }

    public void StartTask(Task newTask)
    {
        targetBuilding = null;
        task = newTask;
        task.Start();
    }

    public void EndTask()
    {
        task.End();
        task = null;
    }
}
