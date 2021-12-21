using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFOV : MonoBehaviour
{
    [Range(1, 5)]public int index = 1;
    public float speed = 25f;
    public GameObject head;
    bool cubeClicked = false;

    private void Start()
    {
        float FOV = SettingsMenu.FOV;
        float cubeIndex = FOV / 30;
        float xPosition = cubeIndex * 15 - 45;

        head.transform.position = new Vector3(xPosition, head.transform.position.y, head.transform.position.z);
    }

    private void OnMouseDown()
    {
        if (head.transform.position == transform.position) return;

        cubeClicked = true;

        SettingsMenu.FOV = 30 * index;
    }

    private void Update()
    {
        if (cubeClicked)
            head.transform.position = Vector3.MoveTowards(head.transform.position, transform.position, Time.deltaTime * speed);

        if (head.transform.position == transform.position)
            cubeClicked = false;
    }
}
