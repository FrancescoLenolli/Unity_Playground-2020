using CoreCharacter.Utilities;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float range = 1f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Path Valid");
        }
        if (!CharacterUtilities.IsTargetInRange(transform, target, range))
            Move();
    }

    private void Move()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        agent.Move(Time.deltaTime * speed * direction);
    }
}
