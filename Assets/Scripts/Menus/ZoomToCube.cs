using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToCube : MonoBehaviour
{
    public int index = 1;
    public float speed = 25f;
    public GameObject head;
    bool cubeClicked = false;

    private void OnMouseDown()
    {
        if (head.transform.position == transform.position) return;

        cubeClicked = true;

        SettingsMenu.difficulty = index;
    }

    private void Update()
    {
        if (cubeClicked)
            head.transform.position = Vector3.MoveTowards(head.transform.position, transform.position, Time.deltaTime * speed);

        if (head.transform.position == transform.position)
            cubeClicked = false;
    }
}
