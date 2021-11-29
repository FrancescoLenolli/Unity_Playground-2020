using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LumberjackCamp : ProductionBuilding
{
    [SerializeField] private float range = 1f;

    private List<GameObject> trees = new List<GameObject>();

    public override bool CanBePlaced()
    {
        return !isOverlapping && AnyTreesInRange();
    }

    protected override void Init()
    {
        base.Init();
        trees = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Tree")).ToList();
    }

    private bool AnyTreesInRange()
    {
        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(tree.transform.position, transform.position);
            if (distance <= range)
                return true;
        }

        return false;
    }
}
