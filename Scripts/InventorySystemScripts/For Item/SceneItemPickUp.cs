using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]

public class SceneItemPickUp : MonoBehaviour
{
    public string itemTypeID;

    private void Start()
    {
        gameObject.tag = "SceneItem";
        ItemTypeData itemData = ItemTypeDatabase.GetItemTypeStatic(itemTypeID);

        if (itemData == null)
        {
            Debug.LogError("Item type not found:" + itemTypeID);
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (itemData.material != null)
            meshRenderer.sharedMaterial = itemData.material;
        if (itemData.mesh != null)
            meshFilter.sharedMesh = itemData.mesh;

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
            meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = itemData.mesh;
    }
}

