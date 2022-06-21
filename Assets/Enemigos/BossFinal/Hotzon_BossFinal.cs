using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotzon_BossFinal : MonoBehaviour
{
    private BossFinalIA enemy;

    void Awake()
    {
        enemy = GetComponentInParent<BossFinalIA>();

    }
    private void Update()
    {
        if (!enemy.isAttacking && !enemy.cooldown)
        {
            enemy.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.target = collision.transform;
            enemy.inRange = true;
        }
    }
}
