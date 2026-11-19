using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ItemVisual3D : MonoBehaviour
{
    public int instanceID;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public void Apply(Mesh mesh, Material material)
    {
        if (mesh == null)
        {
            Debug.LogError($"Для {name} не передан Mesh.");
            return;
        }

        if (material == null)
        {
            Debug.LogWarning($"Для {name} не передан Material.");
        }

        EnsureComponents();

        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial = material;
    }

    private void EnsureComponents()
    {
        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();

        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
    }
}
