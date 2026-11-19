using UnityEngine;

public static class FBXItemExtractor
{
    public static ItemTypeData ExtractFromResources(
        string modelPath,
        string texturePath,
        string itemTypeID,
        string displayName)
    {
        GameObject model = Resources.Load<GameObject>(modelPath);

        if (model == null)
        {
            Debug.LogError("FBX model not found: " + modelPath);  
            return null;
        }

        ItemTypeData data = new ItemTypeData();

        data.itemTypeID = itemTypeID;
        data.displayName = displayName;

        MeshFilter meshFilter = model.GetComponentInChildren<MeshFilter>();
        if (meshFilter != null)
            data.mesh = meshFilter.sharedMesh;

        Renderer renderer = model.GetComponentInChildren<Renderer>();
        if (renderer != null)
            data.material = renderer.sharedMaterial;

        if (!string.IsNullOrEmpty(texturePath))
            data.texture = Resources.Load<Texture>(texturePath);

        return data;
    }
}