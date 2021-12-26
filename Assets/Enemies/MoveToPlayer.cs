using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    Transform playerTransform;
    public string playerName = "First Person Player";
    public float speed = 5f;
    bool canMove = true;
    [Range(0, 1)] public float damagePerSecond= 0;

    private void Start()
    {
        playerTransform = GameObject.Find(playerName).transform;
    }

    void Update()
    {
        transform.LookAt(playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
        if (!canMove) PlayerHealth.hp -= damagePerSecond * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerDmgRange")
        {
            canMove = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerDmgRange")
        {
            canMove = true;
        }
    }
}
