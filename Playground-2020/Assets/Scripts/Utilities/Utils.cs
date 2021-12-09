using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static RaycastHit GetMouseWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }
}
