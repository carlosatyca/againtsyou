using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck_Arquero : MonoBehaviour
{
    private EnemyIA_Arquero enemy;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Arquero>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !enemy.cooldown)
        {
            enemy.Flip();

            Transform[] allChildren = enemy.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.gameObject.name == "arrow")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy.triggerArea.SetActive(true);
            enemy.inRange = false;
            enemy.SelectTarget();
            animator.SetBool("Attack", false);
        }
    }

}
