using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    public InventoryObject inventory;

    void Update()
    {
        if(Input.GetButtonDown("Right Mouse"))
        {
            if(inventory.database.GetItem[HandItem.itemId].type == ItemType.Food)
            {
                if(inventory.database.GetItem[HandItem.itemId].edible)
                {
                    bool hasItem = true;
                    for (int i = 0; i < inventory.Container.Items.Count; i++)
                    {
                        if (inventory.Container.Items[i].ID == HandItem.itemId)
                        {
                            if (inventory.Container.Items[i].amount < 1)
                            {
                                hasItem = false;
                                break;
                            }
                        }
                    }

                    if (!hasItem) return;
                    if (PlayerHealth.hp >= 1) return;

                    for (int i = 0; i < inventory.Container.Items.Count; i++)
                    {
                        if (inventory.Container.Items[i].ID == HandItem.itemId)
                        {
                            inventory.Container.Items[i].RemoveAmount(1);
                            break;
                        }
                    }


                    PlayerHealth.hp += inventory.database.GetItem[HandItem.itemId].restoreHealthValue;
                }
            }
        }
    }
}
