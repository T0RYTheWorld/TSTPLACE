using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private InventoryInputActions input;
    [SerializeField] private InventoryRaySensor sensor;
    [SerializeField] private Transform player;
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject LeftHandAnchor;
    [SerializeField] private GameObject RightHandAnchor;

    void Update()
    {
      if (input == null ||
            sensor == null ||
            !sensor.isActiveAndEnabled ||
            !sensor.IsReady||
            player==null||
            Camera==null)
        {
            return;
        }

        if (input.InventoryIsOpen)
            InventoryIsOpenUpdate();
        else
            InventoryIsClosedUpdate();
    }
    
    private void InventoryIsOpenUpdate()
    {
        GameObject hitObject = sensor.TargetObject;
        if (input.RightHandUsed)
        {   if (hitObject.CompareTag("InventoryItem"))
                ItemPick.WorldorInventory_Pick(RightHandAnchor);
            else
                ItemDrop.Inventory_Drop(RightHandAnchor);}
        if(input.LeftHandUsed)
        {   if (hitObject.CompareTag("InventoryItem"))
                ItemPick.WorldorInventory_Pick(LeftHandAnchor);
            else
                ItemDrop.Inventory_Drop(LeftHandAnchor);}
    }
    
    private void InventoryIsClosedUpdate()
    {
        GameObject hitObject = sensor.TargetObject;
        if (input.RightHandUsed)
        {  
             if (hitObject.CompareTag("SceneItem")){ItemPick.SIPU_Pick(RightHandAnchor);}
            else if (hitObject.CompareTag("WorldItem")){ItemPick.WorldorInventory_Pick(RightHandAnchor);}
            else {UseItemAbility(RightHandAnchor);}
        }
        if(input.LeftHandUsed)
        {   
            if (hitObject.CompareTag("SceneItem")){ItemPick.SIPU_Pick(LeftHandAnchor);}
            else if (hitObject.CompareTag("WorldItem")){ItemPick.WorldorInventory_Pick(LeftHandAnchor);}
            else {UseItemAbility(LeftHandAnchor);}
        }
        if (input.RightHandDropped){ItemDrop.World_Drop(RightHandAnchor);}
        if (input.LeftHandDropped){ItemDrop.World_Drop(LeftHandAnchor);}
        if (input.RightHandRestore){ItemRestore.HandRestore(RightHandAnchor);}
        if (input.LeftHandRestore){ItemRestore.HandRestore(LeftHandAnchor);}
    }

    private void UseItemAbility(GameObject HandAnchor)
    {
        ItemUser itemUser = new ItemUser();
        if (HandAnchor == null)
            return;
        if (!HandAnchor.TryGetComponent<HandAnchorScript>(out var HAS))
            return;
        ItemInstanceData instance = InstanceDataBase.GetInstance(HAS.instanceID);
        if (instance == null)
            return;
        ItemTypeData type = ItemTypeDatabase.GetItemTypeStatic(instance.ItemTypeID);
        if (type.usageTypeName == null)
            return;
        itemUser.Use(type.usageTypeName,"Use");
    }
}

