using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furnace Recipe", menuName = "Inventory System/Furnace Recipe")]
public class FurnaceRecipeObject : ScriptableObject
{
    public int time;
    public FurnaceRecipe[] recipe;
    public FurnaceRecipe output;
}

[System.Serializable]
public class FurnaceRecipe
{
    public ItemObject item;
    public int amount;
}