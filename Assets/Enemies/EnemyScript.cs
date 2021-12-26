using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Range(0.1f, 10)]public float maxHealth = 1;
    private float health;
    private bool inTrigger = false;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAtkRange")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAtkRange")
        {
            inTrigger = false;
        }
    }

    private void Update()
    {
        if(inTrigger && HitEnemy.attacking)
        {
            health -= HitEnemy.staticAtk * Time.deltaTime;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
