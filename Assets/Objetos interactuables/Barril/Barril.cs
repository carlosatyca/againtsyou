using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    public Animator animator;
    public GameObject pared;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15f;
        currentHealth = maxHealth;
    }

    public void TakeDmg(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            animator.SetBool("Destroy", true);
        }
    }
    public void openWall()
    {
        pared.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        pared.GetComponent<Rigidbody2D>().freezeRotation = true; 
    }

}
