using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceUIs : MonoBehaviour
{
    public static int furnaceIindex = 0;
    public static List<GameObject> furnaceUIs = new List<GameObject>();

    public void CloseFurnaces() {
        foreach(GameObject thisUI in furnaceUIs)
        {
            thisUI.SetActive(false);
        }
        // Menus.furnaceOpen = false;
    }
}
