using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace: MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject furnaceUIPrefab;

    bool hasUI = false;
    GameObject furnaceUI;
    GameObject canvas;
    GameObject UIParent;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        UIParent = GameObject.Find("Furnaces");
    }

    private void OnMouseOver()
    {
        // Debug.Log("test");

        if (Input.GetButtonDown("Right Mouse"))
        {
            // Debug.Log("Crafting table right-clicked");
            //menusScript.openCrafting();

            GameObject player = GameObject.Find("First Person Player");

            if (Vector3.Distance(player.transform.position, transform.position) <= 10)
            {
                if (!hasUI)
                {
                    // canvas.GetComponent<Menus>().openCrafting();
                    furnaceUI = Instantiate(furnaceUIPrefab, Vector2.zero, Quaternion.identity);
                    furnaceUI.transform.SetParent(UIParent.transform, false);
                    hasUI = true;
                } else
                {
                    furnaceUI.SetActive(true);
                }
            }
        }
    }
}
