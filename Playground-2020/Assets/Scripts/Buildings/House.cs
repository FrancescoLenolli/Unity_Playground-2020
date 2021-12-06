using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    [SerializeField] private int capacity = 3;

    public int GetCapacity()
    {
        return capacity;
    }

    public void UpgradeCapacity(int value)
    {
        capacity += value;
    }
}
