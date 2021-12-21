using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [Header("Active Slot (0-indexe, max 9 slots)")]
    [Range(0, 8)]public int ActiveSlot = 0;
    public GameObject[] HotbarSlots;
    public Sprite Slot;
    public Sprite SelectedSlot;

    private void Start()
    {
        ResetSlots();
    }

    private void FixedUpdate()
    {
        // Debug.Log(Input.mouseScrollDelta.y);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ActiveSlot--;
            ResetSlots();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ActiveSlot++;
            ResetSlots();
        }
    }

    private void ResetSlots()
    {
        ActiveSlot = Mathf.Clamp(ActiveSlot, 0, HotbarSlots.Length - 1);
        for (int i = 0; i < HotbarSlots.Length; i++)
        {
            HotbarSlots[i].GetComponent<Image>().sprite = Slot;
        }
        HotbarSlots[ActiveSlot].GetComponent<Image>().sprite = SelectedSlot;
    }
}
