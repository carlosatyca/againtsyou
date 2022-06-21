using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{

    #region Public Variables
    public float attackDistance; //Distancia minima
    public float moveSpeed;
    public float timer; //Timer rate per attack
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    public float maxHealth = 30f;
    public PlayerHealth player;
    public float currentHealth;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayer;
    public bool canAttack;
    #endregion

    #region Private Variables
    private Animator animator;
    private float distance; //Distancia entre el enemigo y player
    private bool attackMode;
    private bool cooling; //Enemy is cooling per attack
    private float intTimer;
    private float weaponDamage;
    #endregion
    private int j;
    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        weaponDamage = 8f;
        j = 0;
    }

    void FixedUpdate()
    {
        if (!attackMode)
        {
            Move();
        }
        if (!InsideOfLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Esqueleto_Attack"))
        {
            SelectTarget();
        }
        if (inRange)
        {
            EsqueletoLanzaLogic();
        }
    }

    void EsqueletoLanzaLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            animator.SetBool("Attack", false);
        }
    }

    void Move()
    {
        animator.SetBool("canWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Esqueleto_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDmg(float damage)
    {
        currentHealth -= damage;
        FuryCounter.AddHit(false);
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Death animation
        animator.SetBool("isDead", true);
        //Esqueleto disabled
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        this.enabled = false;

    }

    void Attack()
    {
        timer = intTimer; //Reset timer
        attackMode = true;
        if (player.GetComponent<PlayerHealth>().currentHealth > 0)
        {
        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
        if (canAttack) 
        {  
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D hit in hits)
            {
                if (hit is BoxCollider2D)
                {
                 
                    Debug.Log("manolo tu entras" + Time.time);
                    hit.GetComponent<PlayerHealth>().TakeDmg(weaponDamage);
                    cooling = true;
                    canAttack = false;
                    break;
                }
            }
        }
        
        }
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            animator.SetBool("canWalk", false);
            animator.SetBool("Attack", false);
            this.enabled = false;
        }

    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void TriggerCanHit()
    {
        cooling = true;
    }

    //Enemy is inside his move area
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();

    }

    public void Flip()
    {

        Vector3 rotation = transform.eulerAngles;

        rotation.y = (target.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
