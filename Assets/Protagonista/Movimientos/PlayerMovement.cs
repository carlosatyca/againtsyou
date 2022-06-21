using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public Ladder ladder;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool slide = false;
    bool roll = false;
    private float nextMoveRoll = 0f;
    private float nextMoveSlide = 0f;
    public float cooldownSlide = 0.8f;
    public float cooldownRoll = 0.5f;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        verticalMove = Input.GetAxisRaw("Vertical") * ladder.climbSpeed;

        animator.SetFloat("verticalSpeed", Mathf.Abs(verticalMove));


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            roll = false;
            slide = false;

        }
        if (Input.GetButtonDown("Slide") && Time.time > nextMoveSlide)
        {
            nextMoveSlide = Time.time + cooldownSlide;
            slide = true;
            jump = false;
            roll = false;

        }
        if (Input.GetButtonDown("Roll") && Time.time > nextMoveRoll)
        {
            nextMoveRoll = Time.time + cooldownRoll;
            roll = true;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        
    }

    public void OnRolling()
    {
        animator.SetBool("isRolling", false);

    }

    private void FixedUpdate()
    {
            //Move character
            controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, jump, slide, roll);
            jump = false;
            slide = false;
            roll = false;
    }



}
