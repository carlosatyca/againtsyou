using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea_AlmaErrante : MonoBehaviour
{
    private EnemyIA_AlmaErrante enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_AlmaErrante>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.inRange = true;
            enemy.hotZone.SetActive(true);
        }
    }
}
