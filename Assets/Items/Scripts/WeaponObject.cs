using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]

public class WeaponObject : ItemObject
{
    // public int Atk = 0;
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
