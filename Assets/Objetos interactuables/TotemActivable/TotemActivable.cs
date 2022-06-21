using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemActivable : MonoBehaviour
{
    public Animator animator;
    private bool isInRange;
    private bool isOn;
    public HazLaserActivacion hazLaser;

    void Awake()
    {
        isOn = false;
        isInRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
  

    void Update()
    {
        if (isInRange && !isOn && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Activate", true);
            hazLaser.DeactivadedHaz();
            isOn = true;
        }
        else if (isInRange && isOn && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Activate", false);
            hazLaser.ActivatedHaz();
            isOn = false;
        }
    }

}
