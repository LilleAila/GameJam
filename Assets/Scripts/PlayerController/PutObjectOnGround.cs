using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObjectOnGround : MonoBehaviour
{
    public LayerMask mask;
    float radius;

    public void Ground()
    {
        if (GetComponent<Collider>() != null)
        {
            radius = GetComponent<Collider>().bounds.extents.y;
        }
        else
        {
            radius = 1f;
        }

        RaycastHit hit;

        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        var raycast = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

        Debug.Log(raycast);

        if (raycast)
        {
            if (hit.collider != null)
            {
                Debug.Log("Found Ground!");
                transform.position = new Vector3(transform.position.x, hit.point.y + radius, transform.position.z);
            }
        }
    }
}
