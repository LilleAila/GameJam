using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public GameObject itemInfoSprite;
    public GameObject itemNameText;
    public GameObject itemAmountText;

    public GameObject handItem;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        inventory.Load();

        bool foundPick = false;
        bool foundAxe = false;
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            // Pickaxe ID is 0
            if (inventory.Container.Items[i].ID == 0)
            {
                foundPick = true;
            }

            // Axe ID is 2
            if (inventory.Container.Items[i].ID == 2)
            {
                foundAxe = true;
            }
        }

        if (!foundPick) inventory.Container.Items.Add(new InventorySlot(0, new Item(inventory.database.GetItem[0]), 1));
        if (!foundAxe) inventory.Container.Items.Add(new InventorySlot(2, new Item(inventory.database.GetItem[2]), 1));

        CreateDisplay();
        itemInfo(HandItem.itemId);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            itemsDisplayed.Add(slot, obj);
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            int id = slot.ID;
            obj.GetComponent<Button>().onClick.AddListener(() => itemInfo(id));
            if(slot.amount <= 0) {
                obj.SetActive(false);
            } else
            {
                obj.SetActive(true);
            }
        }
    }

    public void itemInfo(int id) {
        itemInfoSprite.GetComponent<Image>().sprite = inventory.database.GetItem[id].uiDisplay;
        itemNameText.GetComponent<TextMeshProUGUI>().text = inventory.database.GetItem[id].name;

        int itemAmount = 0;

        for(int i = 0; i < inventory.Container.Items.Count; i++)
        {
            if(id == inventory.Container.Items[i].ID)
            {
                itemAmount = inventory.Container.Items[i].amount;
                break;
            }
        }

        itemAmountText.GetComponent<TextMeshProUGUI>().text = itemAmount.ToString("n0");

        if(inventory.database.GetItem[id].holdable)
        {
            handItem.GetComponent<HandItem>().setSprite(id);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                if (slot.amount <= 0)
                {
                    itemsDisplayed[slot].SetActive(false);
                } else
                {
                    itemsDisplayed[slot].SetActive(true);
                }
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
                obj.SetActive(true);
                obj.GetComponent<Button>().onClick.RemoveAllListeners();
                int id = slot.ID;
                obj.GetComponent<Button>().onClick.AddListener(() => itemInfo(id));
            }
        }
    }
}
