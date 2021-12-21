using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSensitivity : MonoBehaviour
{
    [Range(1, 5)] public int index = 1;
    public float speed = 25f;
    public GameObject head;
    bool cubeClicked = false;

    private void Start()
    {
        float Sensitivity = SettingsMenu.Sensitivity;
        float cubeIndex = Sensitivity / 100 - 2;
        float xPosition = cubeIndex * 15 - 45;

        head.transform.position = new Vector3(xPosition, head.transform.position.y, head.transform.position.z);
    }

    private void OnMouseDown()
    {
        if (head.transform.position == transform.position) return;

        cubeClicked = true;

        SettingsMenu.Sensitivity = 100 * index + 200;
    }

    private void Update()
    {
        if (cubeClicked)
            head.transform.position = Vector3.MoveTowards(head.transform.position, transform.position, Time.deltaTime * speed);

        if (head.transform.position == transform.position)
            cubeClicked = false;
    }
}
