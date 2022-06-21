using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazLaser : MonoBehaviour
{
    public Animator animator;
    private float nextAttackTime;
    private void Awake()
    {
        nextAttackTime = 0f;
    }
    void Update()
    {
        if (nextAttackTime <= Time.time)
        {
            animator.SetBool("Attack", true);
            nextAttackTime = Time.time + 5f;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
