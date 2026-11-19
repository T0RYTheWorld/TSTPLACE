using UnityEngine;

public enum ItemLocation
{
    World,
    RightHand,
    LeftHand,
    RightHandHiddenSlot,
    LeftHandHiddenSlot,
    Inventory
}
[System.Serializable]
public class ItemInstanceData
{
    public int InstanceID;
    public string ItemTypeID;
    public GameObject Visual3D;
    public ItemLocation location;
    public InstanceTypeData instanceTypeData;

    public ItemInstanceData(int newInstanceID, string newItemTypeID)
    {
        InstanceID = newInstanceID;
        ItemTypeID = newItemTypeID;
        Visual3D = null;
        location = ItemLocation.World;
        instanceTypeData = null;
    }
}
 