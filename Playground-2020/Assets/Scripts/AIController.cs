using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool canAvoidOtherAgents = false;
    [SerializeField] private float threshold = 1f;
    [SerializeField] private float maxAcceleration = 1f;

    private NavMeshAgent agent;
    private Animator animator;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
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
    }

    public void GoTo(Transform building)
    {
        agent.SetDestination(building.position);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    #region Separation Logic
    private void MoveAwayFromOtherAgents(List<AIController> otherAgents)
    {
        if (!canAvoidOtherAgents)
            return;

        Vector3 separationVector = Vector3.zero;
        for (int i = 0; i < otherAgents.Count; ++i)
        {
            if (otherAgents[i] != this)
                separationVector += Separation(otherAgents[i].transform, threshold, maxAcceleration);
        }

        if (separationVector.magnitude > 0.1f && separationVector != Vector3.zero)
            agent.Move(Time.deltaTime * speed * separationVector.normalized);
    }

    private Vector3 Separation(Transform target, float threshold, float maxAcceleration, float minThreshold = -1.0f)
    {
        Vector3 result = Vector3.zero;

        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        float strength = 0.0f;

        if (distance > threshold || minThreshold > 0 && distance <= minThreshold)
        {
            result = Vector3.zero;
            return result;
        }

        strength = LinearSeparation(maxAcceleration, threshold, distance);
        direction.Normalize();
        result += -strength * direction;

        return result;
    }

    private float LinearSeparation(float maxAcceleration, float threshold, float distance)
    {
        float strength = maxAcceleration * (threshold - distance) / threshold;
        return strength;
    }
    #endregion
}
