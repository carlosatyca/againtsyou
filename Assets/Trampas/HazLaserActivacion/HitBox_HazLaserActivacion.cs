using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_HazLaserActivacion : MonoBehaviour
{
    public PlayerHealth player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D)
        {
            player.GetComponent<PlayerHealth>().TakeDmg(8f);
        }
    }
}
