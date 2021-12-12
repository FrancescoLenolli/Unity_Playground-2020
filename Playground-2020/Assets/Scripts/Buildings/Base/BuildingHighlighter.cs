using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingHighlighter : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial = null;

    private MeshRenderer meshRenderer;
    private Building building;
    private List<Material> materials = new List<Material>();

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        building = GetComponent<Building>();

        SetMaterialsCount();
        Highlight();
    }

    public void Highlight()
    {
        StartCoroutine(HighlightRoutine());
    }

    private void SetMaterialsCount()
    {
        if (materials.Count == 0)
        {
            for (int i = 0; i < meshRenderer.materials.Length; ++i)
                materials.Add(highlightMaterial);
        }
    }

    private IEnumerator HighlightRoutine()
    {
        // additional check to avoid swapping materials every frame.
        bool lastState = true;
        List<Material> buildingMaterials = meshRenderer.materials.ToList();

        while (!building.IsPlaced)
        {
            if(!building.CanBePlaced() && lastState == true)
            {
                lastState = false;
                meshRenderer.materials = materials.ToArray();
            }

            if(building.CanBePlaced() && lastState == false)
            {
                lastState = true;
                meshRenderer.materials = buildingMaterials.ToArray();
            }

            yield return null;
        }
    }
}
