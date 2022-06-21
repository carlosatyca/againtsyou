using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaFlameante : MonoBehaviour
{
    public Animator animator;
    private float nextAttackTime;
    private void Awake()
    {
        nextAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextAttackTime <= Time.time)
        {
            animator.SetBool("Attack", true);
            nextAttackTime = Time.time + 3f;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
