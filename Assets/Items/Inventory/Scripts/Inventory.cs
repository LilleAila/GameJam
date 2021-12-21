using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    /* private void Start()
    {
        Load();
    } */

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    /* private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Load();
        }
    } */

    public void Save()
    {
        inventory.Save();
    }

    public void Load()
    {
        inventory.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
}
