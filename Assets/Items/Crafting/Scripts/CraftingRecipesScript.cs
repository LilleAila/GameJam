using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipesScript : MonoBehaviour
{
    public CraftingRecipes recipes;
    CraftingObject[] craftingObjects;
    public InventoryObject inventory;

    public GameObject craftingPrefab;

    public GameObject recipePanel;
    public GameObject craftButton;
    public GameObject itemToCraft;

    // Start is called before the first frame update
    void Start()
    {
        inventory.database.Reload();
        craftingObjects = recipes.recipes;
        for (int i = 0; i < craftingObjects.Length; i++)
        {
            var craftGameObject = Instantiate(craftingPrefab, Vector3.zero, Quaternion.identity);
            craftGameObject.transform.SetParent(transform, false);
            // craftGameObject.transform.localScale = new Vector3(1, 1, 1);
            craftGameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[craftingObjects[i].output.item.Id].uiDisplay;
            craftGameObject.GetComponentInChildren<TextMeshProUGUI>().text = craftingObjects[i].output.amount.ToString("n0");
            int a = i;
            craftGameObject.GetComponent<Button>().onClick.AddListener(() => showCraft(a));
        }
        showCraft(0);
    }

    public void showCraft(int index) {
        inventory.database.Reload();
        foreach (Transform child in recipePanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < craftingObjects[index].recipe.Length; i++)
        {
            var itemGameObject = Instantiate(craftingPrefab, Vector3.zero, Quaternion.identity);
            itemGameObject.transform.SetParent(recipePanel.transform, false);
            itemGameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[craftingObjects[index].recipe[i].item.Id].uiDisplay;
            itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = craftingObjects[index].recipe[i].amount.ToString("n0");
        }

        itemToCraft.GetComponent<Image>().sprite = inventory.database.GetItem[craftingObjects[index].output.item.Id].uiDisplay;

        int a = index;
        craftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        craftButton.GetComponent<Button>().onClick.AddListener(() => craft(a));
    }

    public void craft(int index) {
        Recipe[] recipe = recipes.recipes[index].recipe;

        bool hasItems;
        int itemsFound = 0;

        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            for (int a = 0; a < recipe.Length; a++)
            {
                if(recipe[a].item.Id == inventory.Container.Items[i].ID)
                {
                    itemsFound++;
                }
            }
        }

        hasItems = itemsFound == recipe.Length;

        if (!hasItems) return;

        bool hasAmount = true;

        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            for (int a = 0; a < recipe.Length; a++)
            {
                if(recipe[a].item.Id == inventory.Container.Items[i].ID) {
                    if(inventory.Container.Items[i].amount - recipe[a].amount < 0)
                    {
                        hasAmount = false;
                    }
                }
            }
        }

        if (!hasAmount) return;

        inventory.AddItem(new Item(craftingObjects[index].output.item), craftingObjects[index].output.amount);

        for (int b = 0; b < recipe.Length; b++)
        {
            for (int c = 0; c < inventory.Container.Items.Count; c++)
            {
                if(inventory.Container.Items[c].ID == recipe[b].item.Id)
                {
                    inventory.Container.Items[c].RemoveAmount(recipe[b].amount);
                    break;
                }
            }
        }
    }
}