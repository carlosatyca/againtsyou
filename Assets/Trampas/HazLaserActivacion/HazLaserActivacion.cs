using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazLaserActivacion : MonoBehaviour
{
    public Animator animator;
    public bool isDeactivated;

    private void Awake()
    {
        isDeactivated = false;
    }
    public void DeactivadedHaz()
    {
        isDeactivated = true;
        animator.SetBool("Desactivado", isDeactivated);
    }

    public void ActivatedHaz()
    {
        isDeactivated = false;
        animator.SetBool("Desactivado", isDeactivated);
    }

}
