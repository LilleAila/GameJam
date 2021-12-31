using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

                    furnaceUI.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    furnaceUI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => closeFurnace());

                    FurnaceUIs.furnaceUIs.Add(furnaceUI);
                    // Menus.furnaceOpen = true;
                    Menus.menuOpen = true;
                } else
                {
                    furnaceUI.SetActive(true);
                    // Menus.furnaceOpen = true;
                    Menus.menuOpen = true;
                }
            }
        }
    }

    public void closeFurnace()
    {
        furnaceUI.SetActive(false);
        // Menus.furnaceOpen = false;
        Menus.menuOpen = false;
    }
}
