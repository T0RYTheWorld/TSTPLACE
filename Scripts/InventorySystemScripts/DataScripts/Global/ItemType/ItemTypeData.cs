using UnityEngine;
[System.Serializable]
public class ItemTypeData
{
    [Header ("IDs")]
    public string itemTypeID;
    public string displayName;

    [Header ("World/Inventory 3D")]
    public Mesh mesh;
    public Material material;

    [Header ("Hand Visual")]
    public Texture texture;
    [Header ("Over")]
    public string usageTypeName;
    public float Weight;
    public bool canBeHiddenInHandHotSlot;
}

