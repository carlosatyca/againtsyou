using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck_Calabazas : MonoBehaviour
{
    private EnemyIA_Calabazas enemy;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Calabazas>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange)
        {
            enemy.Flip();
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
            animator.SetBool("FastAttack", false);
            animator.SetBool("PotentialAttack", false);
        }
    }

}
