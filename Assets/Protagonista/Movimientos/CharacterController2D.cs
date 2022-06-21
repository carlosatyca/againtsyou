using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 600f;                          // Amount of force added when the player jumps.
	[SerializeField] private float slideSpeed = 5f;
	[SerializeField] private float m_rollForce = 800f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	private bool m_rolling;
	private bool m_jumping;
	private float timeRolling = 0f;
	private float timeJumping= 0f;
	//Ladder
	[HideInInspector]
	public bool usingLadder = false;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;


	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public Animator animator;


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	}

	private void FixedUpdate()
	{

		bool wasGrounded = m_Grounded;
		animator.SetBool("isGrounded", m_Grounded);
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, float vMove, bool jump, bool slide, bool roll)
	{

		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_ChargedAttack2")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_CancelAttack2")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack2")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack1")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Healing")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Pray")
			 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hurt")
			 )
		{ 
				//Ladder
				Climb(jump, vMove);


			// If the player should jump
			if (!m_rolling)
			{
				Jump(jump);
			}
      
			// If the player should roll
			if (!m_jumping)
			{
				Roll(roll);
			}

			//If grounded player can slide
			if (m_Grounded && slide)
			{
				Slide();
			}

			//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_AirControl)
			{
				Movement(move);
			}
		}
		else
		{
			Vector3 targetVelocity = Vector2.zero;
			Vector3 m_Velocity = Vector3.zero;
			GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref m_Velocity, .05f);
		}
	}

	private void Movement(float move)
    {
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			Flip();
		}
	}

	private void Climb(bool jump, float vMove)
    {
		//Player is on the ground in the ladder
		if(m_Grounded && usingLadder && vMove == 0)
        {
			animator.SetBool("usingLadder", false);
			animator.SetBool("isGrounded", true);
        }
		//Player is on the ground in the ladder but moving
		else if (usingLadder && vMove !=0 && m_Grounded)
        {
			animator.SetBool("usingLadder", true);
			animator.SetBool("isGrounded", false);
        }
		//Player jump while is on the ladder
		else if (usingLadder && !m_Grounded && jump)
        {
			animator.SetTrigger("takeOff");
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			animator.SetBool("isJumping", true);
		}
		else if (usingLadder && !m_Grounded && !jump)
		{

			animator.SetBool("isJumping", false);
		}

	}

	private void Jump(bool jump)
    {
		if (m_Grounded && jump)
		{
			animator.SetTrigger("takeOff");
			timeJumping = Time.time;
			// Add a vertical force to the player.
			m_Grounded = false;
			m_jumping = true;
			m_rolling = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		else if(!m_Grounded && m_jumping && !m_rolling)
		{
			animator.SetBool("isGrounded", false);
			animator.SetBool("isJumping", true);
			m_jumping = false;
		}
	}

	private void Roll(bool roll)
    {
		if (m_Grounded && roll)
		{
			animator.SetTrigger("onRol");
			timeRolling = Time.time;
			// Add a vertical force to the player.
			m_Grounded = false;
			m_rolling = true;
			m_jumping = false;
			m_Rigidbody2D.AddForce(new Vector2(m_rollForce, m_rollForce));
		}
		else if (!m_Grounded && !m_jumping && m_rolling)
		{
			animator.SetBool("isRolling", true);
			animator.SetBool("isGrounded", false);
			m_rolling = false;
		}
	}
	void exitTakeOfJumping()
	{
		if (Time.time - timeJumping >= 0.1f)
		{
			animator.SetTrigger("OutJump");
		}
	}
	void exitInitialRoll()
    {
		if(Time.time - timeRolling >= 0.2f)
        {
			animator.SetTrigger("OutRoll");
		}
    }
	private void Slide()
    {
		animator.SetBool("isJumping", false);
		animator.SetBool("isRolling", false);
		animator.SetBool("isSliding", true);
		m_Grounded = true;
		if (m_FacingRight)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x * slideSpeed, 0f);
		}
		else
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x * slideSpeed, 0f);
		}	
		StartCoroutine(stopSlide());
	}
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool PlayerIsGrounded()
    {
        return m_Grounded;
    }
	
	private IEnumerator stopSlide()
    {
		yield return new WaitForSeconds(0.4f);
		animator.SetBool("isSliding", false);
	}

}
