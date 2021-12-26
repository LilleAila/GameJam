using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandItem : MonoBehaviour
{
    public GameObject handSprite;
    public GameObject handSpriteContainer;
    public GameObject item3DContainer;
    public InventoryObject inventory;
    public static int itemId = 0;

    public void setSprite(int id)
    {
        ItemObject item = inventory.database.GetItem[id];
        if(item.squareSprite)
        {
            handSpriteContainer.SetActive(true);
            item3DContainer.SetActive(false);
            handSprite.GetComponent<Renderer>().material.SetTexture("_MainTex", item.uiDisplay.texture);
        } else
        {
            handSpriteContainer.SetActive(false);
            item3DContainer.SetActive(true);
            foreach(Transform child in item3DContainer.transform)
            {
                Destroy(child.gameObject);
            }
            var heldItem = Instantiate(item.Prefab3DModel, Vector3.zero, Quaternion.identity);
            heldItem.transform.SetParent(item3DContainer.transform, false);
        }
        itemId = item.Id;
        ChopTree.chopping = false;
        MineRock.mining = false;
        HitEnemy.attacking = false;
    }
}
