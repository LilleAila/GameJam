using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZero4 : MonoBehaviour
{
    [SerializeField]
    private TerrainController4 terrainController;

    [SerializeField]
    private float distance = 10;

    private void Update()
    {
        if(Vector3.Distance(Vector3.zero, transform.position) > distance)
        {
            terrainController.Level.position -= transform.position;
            transform.position = Vector3.zero;
        }
    }
}
