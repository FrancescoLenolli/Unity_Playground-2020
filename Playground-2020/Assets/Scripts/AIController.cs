using CoreCharacter.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float range = 1f;
    [SerializeField]
    private float threshold = 1f;
    [SerializeField]
    private float maxAcceleration = 1f;

    private NavMeshAgent agent;
    private Transform target = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        Debug.Log(name);
    }

    public void UpdatePosition(List<AIController> otherAgents, Transform target)
    {
        if (!this.target)
            this.target = target;

        Move();
        MoveAwayFromOtherAgents(otherAgents);
    }

    private void Move()
    {
        if (target)
        {
            if (!CharacterUtilities.IsTargetInRange(transform, target, range))
                agent.SetDestination(target.position);
        }
    }

    private void MoveAwayFromOtherAgents(List<AIController> otherAgents)
    {
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
}
