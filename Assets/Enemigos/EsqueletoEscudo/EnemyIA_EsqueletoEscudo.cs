using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA_EsqueletoEscudo : MonoBehaviour
{

    public Transform leftLimit;
    public Transform rightLimit;
    public float moveSpeed;
    [HideInInspector] public Transform target;
    public bool inRange;
    public bool isAttacking;
    private float nextAttackTime = 0f;
    public Animator animator;
    private float distance; //Distancia entre el enemigo y player
    public float attackDistance; //Distancia minima
    public PlayerHealth player;
    public GameObject hotZone;
    public GameObject triggerArea;
    public float maxHealth = 25f;
    public float currentHealth;
    public bool cooldown;
    private int randomAttack;
    public bool blockedHit;
    private float hurtTime = 0f;
    private void Awake()
    {
        SelectTarget();
        isAttacking = false;
        currentHealth = maxHealth;
    }

    private void GetPatrollingPosition()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_FastAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_HardAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_Shield"))
        {
            animator.SetBool("canWalk", true);
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 targetVelocity = Vector2.zero;
            Vector3 m_Velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref m_Velocity, .05f);
        }
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
        if (!isAttacking)
        {
            Flip();
        }

    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = (target.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;
        transform.eulerAngles = rotation;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void TakeDmg(float damage)
    {
        if (hurtTime < Time.time)
        {
            hurtTime = Time.time + 1.75f;
            if (!blockedHit)
            {
                currentHealth -= damage;
                FuryCounter.AddHit(false);
                animator.SetTrigger("Hurt");
            }
            if (currentHealth <= 0)
            {
                PlayerHealth.numEnemigosEliminados++;
                Die();
            }
        }
    }

    public void DieByPinchos(float damage)
    {
        currentHealth -= damage;
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
        GetComponent<Rigidbody2D>().gravityScale = 90;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        this.enabled = false;

    }
    IEnumerator esperaCD(float seconds)
    {
        yield return new WaitForSeconds(seconds);

    }

    private void Cooldown()
    {
        if (nextAttackTime <= Time.time)
        {
            cooldown = false;
        }
        if (nextAttackTime > Time.time)
        {
            cooldown = true;
        }
    }

    void RollTheDice()
    {
        randomAttack = Random.Range(0, 3);
    }

    void Update()
    {
        Cooldown();
        if (!isAttacking)
        {
            GetPatrollingPosition();
        }
        if (!InsideOfLimits() && !inRange)
        {
            SelectTarget();
        }
        if (inRange)
        {
            distance = Vector2.Distance(transform.position, target.position);
            if (distance > attackDistance)
            {
                nextAttackTime = 0f;
                isAttacking = false;
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Shield", false);
            }
            if (distance <= attackDistance && !cooldown)
            {
                //ATACA
                nextAttackTime = Time.time + 0.75f;
                isAttacking = true;
                RollTheDice();
                if(player.GetComponent<PlayerHealth>().currentHealth > 0)
                {
                    if(randomAttack == 0)
                    {
                        animator.SetBool("canWalk", false);
                        animator.SetBool("Attack1", true);
                    }
                    else if(randomAttack == 1)
                    {
                        animator.SetBool("canWalk", false);
                        animator.SetBool("Attack2", true);
                    }else if(randomAttack == 2)
                    {
                        animator.SetBool("canWalk", false);
                        animator.SetBool("Shield", true);
                    }
                }
                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack1", false);
                    animator.SetBool("Attack2", false);
                    animator.SetBool("Shield", false);
                    this.enabled = false;
                }
            }
            if (distance <= attackDistance && cooldown)
            {
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Shield", false);
                StartCoroutine(esperaCD(nextAttackTime - Time.time));
            }

        }
    }

}
