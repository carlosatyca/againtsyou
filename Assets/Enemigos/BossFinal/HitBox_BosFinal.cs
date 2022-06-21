using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_BosFinal : MonoBehaviour
{
    private BossFinalIA enemy;
    private Animator animator;
    public PlayerHealth player;
    public float normalDmg;
    public float hardDmg;
    private void Awake()
    {
        enemy = GetComponentInParent<BossFinalIA>();
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("BossFinal_Attack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(normalDmg);
        }
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("BossFinal_HardAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(hardDmg);
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("HardAttack", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Attack", false);
            animator.SetBool("HardAttack", false);
        }
    }
}
