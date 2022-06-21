using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA_Ent : MonoBehaviour
{

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
    public float maxHealth = 45f;
    public float currentHealth;
    public bool cooldown;
    public Transform sitDownPoint;
    private int randomAttack;
    public Transform rightLimit;
    public Transform leftLimit;
    private float hurtTime = 0f;
    private void Awake()
    {
        SelectTarget();
        isAttacking = false;
        currentHealth = maxHealth;
    }

    public void SelectTarget()
    {
        float distanceToSit = Vector2.Distance(transform.position, sitDownPoint.position);
        float distanceToPlayer = Vector2.Distance(transform.position, player.GetComponentInParent<Transform>().position);
        //Mirar esto
        if (distanceToSit < distanceToPlayer)
        {
            target = sitDownPoint;
        }
        else {
            target = player.GetComponentInParent<Transform>();
        }

        if (!isAttacking)
        {
            Flip();
        }

    }
    private void Move()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_LowAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_NormalAttack"))
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

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = (target.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;
        transform.eulerAngles = rotation;
    }

    private bool IsSitDown()
    {
        return transform.position.x == sitDownPoint.position.x && animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_Sit");
    }

    public void TakeDmg(float damage)
    {
        if (hurtTime < Time.time)
        {
            currentHealth -= damage;
            FuryCounter.AddHit(false);
            animator.SetTrigger("Hurt");
            hurtTime = Time.time + 1.75f;
            if (currentHealth <= 0)
            {
                PlayerHealth.numEnemigosEliminados++;
                Die();
            }
        }
    }
    private bool CanSitDown()
    {
        return transform.position.x == sitDownPoint.position.x && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_Sit") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_SitUp");
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
        animator.SetBool("canWalk", false);
        animator.SetBool("isSitUp", false);
        animator.SetBool("isDead", true);
        //Esqueleto disabled
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }

    void TriggerDieAnimation()
    {
        transform.position = new Vector3(transform.position.x, sitDownPoint.position.y + 0.5f, transform.position.z);
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
        randomAttack = Random.Range(0, 2);
    }
    void SitPoint()
    {
        animator.SetBool("canWalk", false);
        animator.SetBool("isSitUp", false);
    }
    void Update()
    {
        Cooldown();

        if(!isAttacking && CanSitDown() && !inRange && !IsSitDown())
        {
            SitPoint();
        }

        if (!isAttacking && !IsSitDown() && !CanSitDown())
        {
            Move();
        }
        if (!IsSitDown() && !inRange)
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
            }
            if (distance <= attackDistance && !cooldown)
            {
                //ATACA
                nextAttackTime = Time.time + 1.5f;
                isAttacking = true;
                RollTheDice();
                if (randomAttack == 0)
                {
                    if (player.GetComponent<PlayerHealth>().currentHealth > 0)
                    {
                        animator.SetBool("canWalk", false);
                        animator.SetBool("Attack1", true);
                    }
                }
                if (randomAttack == 1)
                {
                    if (player.GetComponent<PlayerHealth>().currentHealth > 0)
                    {
                        animator.SetBool("canWalk", false);
                        animator.SetBool("Attack2", true);
                    }
                }

                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack1", false);
                    animator.SetBool("Attack2", false);
                    this.enabled = false;
                }
            }
            if (distance <= attackDistance && cooldown)
            {
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                StartCoroutine(esperaCD(nextAttackTime - Time.time));
            }

        }
    }
}
