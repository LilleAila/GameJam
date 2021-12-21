using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe List", menuName = "Inventory System/Recipe List")]
public class CraftingRecipes : ScriptableObject
{
    public CraftingObject[] recipes;
}
