using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Inventory System/Crafting Recipe")]
public class CraftingObject : ScriptableObject
{
    public Recipe[] recipe;
    public Recipe output;
}

[System.Serializable]
public class Recipe
{
    public ItemObject item;
    public int amount;
}