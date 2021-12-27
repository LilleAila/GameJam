using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]

public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        ReloadDictionary();
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }

    [ContextMenu("Reload Dictionary")]
    public void Reload()
    {
        GetItem = new Dictionary<int, ItemObject>();
        ReloadDictionary();
        // Debug.Log("Reloaded Dictionary");
    }

    public void ReloadDictionary() {
        // GetItem = new Dictionary<int, ItemObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }
}