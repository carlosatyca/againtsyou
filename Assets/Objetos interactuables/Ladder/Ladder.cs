using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    CharacterController2D controller;

    //public BoxCollider2D platformGround;
    public float climbSpeed;
    public float exitHop = 6f;
    [HideInInspector]
    public bool onLadder = false;
    public BoxCollider2D platformGround;
    public CharacterController2D player;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        controller = player.GetComponent<CharacterController2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<PlayerHealth>().currentHealth > 0)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.transform.position = new Vector3(transform.position.x, rb.position.y);
                rb.gravityScale = 0;
                onLadder = true;
                platformGround.enabled = false;
                controller.usingLadder = onLadder;
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && onLadder)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);

            }
            else if(onLadder && controller.PlayerIsGrounded()&& Input.GetAxisRaw("Vertical") == 0)
            {
                animator.SetBool("usingLadder", false);
            }

            animator.SetBool("usingLadder", onLadder);
            animator.SetFloat("verticalSpeed", Mathf.Abs(Input.GetAxisRaw("Vertical")));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && onLadder)
        {
            rb.gravityScale = 3f;
            onLadder = false;
            controller.usingLadder = onLadder;
            platformGround.enabled = true;
            animator.SetBool("usingLadder", false);
            if(!controller.PlayerIsGrounded() && !onLadder && Input.GetAxisRaw("Vertical") == 0)
            {
                animator.SetBool("isFalling", true);
            }
            if (!controller.PlayerIsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, exitHop);
            }
        }
    }


}
