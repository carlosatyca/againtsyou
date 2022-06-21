using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaFlameante_HitBox : MonoBehaviour
{
    private Animator animator;
    public PlayerHealth player;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(6f);
        }
    }
}
