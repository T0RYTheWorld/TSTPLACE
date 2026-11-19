using UnityEngine;

[RequireComponent(typeof(SceneItemPickUp))]
public class TypeRandomizer : MonoBehaviour
{
    [SerializeField] private string[] possibleItemTypeIds = {};

    private void Awake()
    {
        if (possibleItemTypeIds == null || possibleItemTypeIds.Length == 0)
        {
            Debug.LogWarning("Список типов предметов пуст.", this);
            return;
        }

        SceneItemPickUp sceneItem = GetComponent<SceneItemPickUp>();

        int index = Random.Range(0, possibleItemTypeIds.Length);

        sceneItem.itemTypeID = possibleItemTypeIds[index];
    }
}

