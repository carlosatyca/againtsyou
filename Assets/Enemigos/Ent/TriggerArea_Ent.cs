using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea_Ent : MonoBehaviour
{

    private EnemyIA_Ent enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Ent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.inRange = true;
            enemy.GetComponent<Animator>().SetBool("isSitUp",true);
            enemy.hotZone.SetActive(true);
        }
    }
}
