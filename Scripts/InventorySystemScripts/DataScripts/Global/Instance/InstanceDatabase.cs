using System.Collections.Generic;
using UnityEngine;

public class InstanceDataBase
{
    private static Dictionary<int, ItemInstanceData> instances = new Dictionary<int, ItemInstanceData>();
    private static int nextID = 1;

    public static ItemInstanceData CreateInstance(string itemTypeID)
    {
        if (string.IsNullOrEmpty(itemTypeID))
	            return null;
	
	        ItemTypeData typeData = ItemTypeDatabase.GetItemTypeStatic(itemTypeID);
	        if (typeData == null)
	            return null;
        
        int newID = nextID;
        nextID++;

        ItemInstanceData newInstance = new ItemInstanceData(newID, itemTypeID);

        GameObject visualObject = new GameObject(ItemTypeDatabase.GetItemTypeStatic(itemTypeID).displayName + "ItemVisual3D_" + newID);
        ItemVisual3D visual3D = visualObject.AddComponent<ItemVisual3D>();
        visual3D.Apply(typeData.mesh,typeData.material);
        newInstance.Visual3D = visualObject;

        ItemLocation location = ItemLocation.World;
        newInstance.location = location;

        InstanceTypeData instanceTypeData = InstanceTypeDatabase.GetInstanceType(itemTypeID);
        newInstance.instanceTypeData = instanceTypeData;

        AddInstance(newInstance);
        return newInstance;
    }
    public static void AddInstance(ItemInstanceData instanceData)
    {
        if (instanceData == null)
            return;
        if (instances.ContainsKey(instanceData.InstanceID))
        {
            Debug.LogWarning("instance already exists:" + instanceData.InstanceID);
            return;
        }

        instances.Add(instanceData.InstanceID, instanceData);
    }

    public static ItemInstanceData GetInstance(int InstanceID)
    {
        if (instances.TryGetValue(InstanceID, out ItemInstanceData instanceData))
            return instanceData;
        return null;
    }

    public static bool ContainsInstance(int InstanceID)
    {
        return instances.ContainsKey(InstanceID);
    }

    public static void RemoveInstance(int InstanceID)
    {
        if (instances.ContainsKey(InstanceID))
            instances.Remove(InstanceID);
    }
}
