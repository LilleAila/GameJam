using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    // public InventoryObject inventory;
    //public Menus menusScript;

    private void OnMouseOver()
    {
        // Debug.Log("test");

        if(Input.GetButtonDown("Right Mouse"))
        {
            // Debug.Log("Crafting table right-clicked");
            //menusScript.openCrafting();

            GameObject player = GameObject.Find("First Person Player");

            if(Vector3.Distance(player.transform.position, transform.position) <= 10) {
                GameObject canvas = GameObject.Find("Canvas");
                canvas.GetComponent<Menus>().openCrafting();
            }
        }
    }
}
