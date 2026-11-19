using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTypeDatabase 
{
    public static  Dictionary<string, ItemTypeData> Database = new Dictionary<string, ItemTypeData>();
    static ItemTypeDatabase() 
    {
        ItemTypeData Common_Cube = FBXItemExtractor.ExtractFromResources(
            modelPath: "Models/CommonCube",
            texturePath: "Texture/20261012233629",
            itemTypeID: "Common_cube",
            displayName: "Common Cube"
        );
        if (Common_Cube == null)
            return;
        Common_Cube.canBeHiddenInHandHotSlot = true;
        Common_Cube.Weight = 1f;
        Common_Cube.usageTypeName = null;
        Database.Add(Common_Cube.itemTypeID, Common_Cube);

    }
    public static ItemTypeData GetItemTypeStatic(string id)
    {
        if (Database.TryGetValue(id, out ItemTypeData data))
            return data;
        return null;
    }
}