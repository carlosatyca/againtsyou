using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haz : MonoBehaviour
{
    public Animator animator;
    private float nextAttackTime;
    // Start is called before the first frame update
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
            nextAttackTime = Time.time + 6f;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
