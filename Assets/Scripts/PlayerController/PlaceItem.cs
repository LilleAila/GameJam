using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    public InventoryObject inventory;
    public Transform placeLocation;
    public LayerMask mask;
    public Transform mainCamera;
    public Transform levelTransform;

    void Update()
    {
        if(Input.GetButtonDown("Right Mouse"))
        {
            if(inventory.database.GetItem[HandItem.itemId].placeable)
            {
                bool hasItem = true;
                for(int i = 0; i < inventory.Container.Items.Count; i++)
                {
                    if(inventory.Container.Items[i].ID == HandItem.itemId)
                    {
                        if(inventory.Container.Items[i].amount < 1)
                        {
                            hasItem = false;
                            break;
                        }
                    }
                }

                if (!hasItem) return;

                placeLocation.localPosition = new Vector3(0, 0, inventory.database.GetItem[HandItem.itemId].placeDistance);
                GameObject placedItem = Instantiate(inventory.database.GetItem[HandItem.itemId].placePrefab, new Vector3(placeLocation.position.x, transform.position.y, placeLocation.position.z), Quaternion.identity);
                Transform placedItemTransform = inventory.database.GetItem[HandItem.itemId].placePrefab.transform;
                placedItem.transform.LookAt(mainCamera);
                placedItem.transform.localEulerAngles = new Vector3(placedItemTransform.localEulerAngles.x, placedItem.transform.localEulerAngles.y, placedItemTransform.localEulerAngles.z);
                placedItem.transform.SetParent(levelTransform, true);

                float radius = placedItem.GetComponent<Collider>().bounds.extents.y;

                RaycastHit hit;

                Ray ray = new Ray(placedItem.transform.position + Vector3.up * 100, Vector3.down);

                var raycast = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

                if (raycast)
                {
                    if (hit.collider != null)
                    {
                        placedItem.transform.position = new Vector3(placedItem.transform.position.x, hit.point.y + radius, placedItem.transform.position.z);
                    }
                }

                for (int i = 0; i < inventory.Container.Items.Count; i++)
                {
                    if(inventory.Container.Items[i].ID == HandItem.itemId)
                    {
                        inventory.Container.Items[i].RemoveAmount(1);
                        break;
                    }
                }
            }
        }
    }
}
