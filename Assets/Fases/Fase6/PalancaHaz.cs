using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaHaz : MonoBehaviour
{
    public HazLaserActivacion hazLaser;
    private bool isOn;
    public Animator animator;
    private bool isInRange;

    // Start is called before the first frame update
    void Awake()
    {
        isOn = false;
        isInRange = false;
    }
    void Update()
    {
        if (isInRange && !isOn && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("On", true);
            hazLaser.DeactivadedHaz();
            isOn = true;
        }
        else if (isInRange && isOn && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("On", false);
            hazLaser.ActivatedHaz();
            isOn = false;
        }
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

}
