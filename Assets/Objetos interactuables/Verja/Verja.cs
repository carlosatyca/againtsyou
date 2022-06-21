using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verja : MonoBehaviour
{
    private bool open;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        open = false;
        animator = GetComponent<Animator>();
    }
    public void OpenVerja()
    {
        open = true;
        animator.SetBool("Open", true);
    }
    public void CloseVerja()
    {
        if (open)
        {
            open = false;
            animator.SetBool("Close", true);
        }
    }
    
    public void IdleVerja()
    {
        animator.SetBool("Close", false);
        animator.SetBool("Open", false);
    }


}
