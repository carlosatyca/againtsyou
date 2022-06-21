using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA_AlmaErrante : MonoBehaviour
{

    //Limites del patron de movimiento
    public Transform leftLimit;
    public Transform rightLimit;
    public float moveSpeed;
    [HideInInspector]public Transform target;
    public bool inRange;
    public bool isAttacking;
    private float nextAttackTime = 0f;
    public Animator animator;
    private float distance; //Distancia entre el enemigo y player
    public float attackDistance; //Distancia minima
    public PlayerHealth player;
    public GameObject hotZone;
    public GameObject triggerArea;
    public float maxHealth = 30f;
    public float currentHealth;
    public bool cooldown;
    private float hurtTime = 0f;
    private float healTime;

    private void Awake()
    {
        SelectTarget();
        isAttacking = false;
        currentHealth = maxHealth;
        healTime = Time.time;
    }

    private void GetPatrollingPosition()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AlmaErrante_Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("AlmaErrante_Health"))
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
    //Enemy is inside his move area
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void TakeDmg(float damage)
    {
        if(hurtTime < Time.time)
        {
            currentHealth -= damage;
            FuryCounter.AddHit(false);
            animator.SetTrigger("Hurt");
            hurtTime = Time.time + 2f;
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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;

    }
    IEnumerator esperaCD(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
    }

    private void Cooldown()
    {
        if(nextAttackTime <= Time.time)
        {
            cooldown = false;
        }
        if(nextAttackTime > Time.time)
        {
            cooldown = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Cooldown();
        if (!isAttacking)
        {
            GetPatrollingPosition();
        }
        if(!InsideOfLimits() && !inRange)
        {
            SelectTarget();
        }
        if (inRange)
        {
            distance = Vector2.Distance(transform.position, target.position);
            if(distance > attackDistance)
            {
                nextAttackTime = 0f;
                isAttacking = false;
                animator.SetBool("Attack", false);
            }
            if(distance <= attackDistance && !cooldown)
            {
                //ATACA
                isAttacking = true;
                if(player.GetComponent<PlayerHealth>().currentHealth > 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack", true);
                    nextAttackTime = Time.time + 0.75f;
                    //HACER DAÑO se encuentra en HITBOX
                }
                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack", false);
                    this.enabled = false;
                }
                  
            }
            if(distance <= attackDistance && cooldown)
            {
                animator.SetBool("Attack", false);
                StartCoroutine(esperaCD(nextAttackTime - Time.time));                
            }
        } else if (!inRange)
        {
            if (currentHealth < maxHealth && !cooldown && (Time.time - healTime) > 8f)
            {
                int random = Random.Range(1, 20);

                if (random == 1)
                {
                    healTime = Time.time;
                    animator.SetBool("Heal", true);
                    currentHealth = currentHealth + (maxHealth / 2) > maxHealth ? maxHealth : (currentHealth + (maxHealth / 4));
                    StartCoroutine(esperaCuracion());
                }
            }
        }

    }

    IEnumerator esperaCuracion()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Heal", false);
        nextAttackTime = Time.time + 0.5f;
    }
}
