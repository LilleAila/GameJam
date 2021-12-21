using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Tool,
    Weapon,
    Default
}

public enum Attributes
{
    Agility,
    Intellect,
    Stamina,
    Strength
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    // public bool Stackable = true;
    public ItemType type;

    [Header("Holdable?")]
    public bool holdable;
    public bool squareSprite;

    [Header("Add 3D Model if not square sprite")]
    public GameObject Prefab3DModel;

    [Header("Placeable?")]
    public bool placeable;
    public GameObject placePrefab;
    [Range(1, 10)]public int placeDistance = 5;

    /*[TextArea(15,20)]
    public string description;*/
    // public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    // public ItemBuff[] Buffs;
    // public bool Stackable;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        /* Buffs = new ItemBuff[item.buffs.Length];
        for(int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            Buffs[i].attribute = item.buffs[i].attribute;
        }*/ 
    }
}

/* [System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
} */