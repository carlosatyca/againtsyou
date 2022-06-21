using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA_Arquero : MonoBehaviour
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
    public float maxHealth = 35f;
    public float currentHealth;
    public bool cooldown;
    public Sprite sprite;
    private float hurtTime = 0f;

    private void Awake()
    {
        SelectTarget();
        isAttacking = false;
        currentHealth = maxHealth;
    }

    private void GetPatrollingPosition()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Arquero_Attack"))
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
        if (hurtTime < Time.time)
        {
            hurtTime = Time.time + 3.25f;
            currentHealth -= damage;
            FuryCounter.AddHit(false);
            animator.SetTrigger("Hurt");
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
                nextAttackTime = Time.time + 2f;  
                isAttacking = true;
                if(player.GetComponent<PlayerHealth>().currentHealth > 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack", true);
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
                StartCoroutine(esperaCD(nextAttackTime - Time.time));                
            }
            
        }
    }

    public void shootArrow()
    {
        GameObject arrow = new GameObject("arrow");
        arrow.transform.SetParent(gameObject.transform);
        arrow.layer = LayerMask.NameToLayer("Map");
        arrow.transform.localPosition = new Vector3(2.23f, -0.59f, 0f);

        arrow.AddComponent<SpriteRenderer>();
        arrow.AddComponent<BoxCollider2D>();
        arrow.AddComponent<HitBox_Arquero>();
        arrow.AddComponent<Rigidbody2D>();

        arrow.GetComponent<HitBox_Arquero>().player = player;

        arrow.GetComponent<SpriteRenderer>().sprite = sprite;

        arrow.GetComponent<BoxCollider2D>().isTrigger = true;
        arrow.GetComponent<BoxCollider2D>().offset = new Vector2(0.006317139f, 0.002487183f);
        arrow.GetComponent<BoxCollider2D>().size = new Vector2(1.841858f, 0.2231293f);

        arrow.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        arrow.GetComponent<Rigidbody2D>().mass = 0.1f;
        arrow.GetComponent<Rigidbody2D>().gravityScale = 0f;

        if (gameObject.transform.rotation.eulerAngles.y == 0f)
        {
            arrow.GetComponent<SpriteRenderer>().flipX = true;
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(75f, 0f));
        }
        else if (gameObject.transform.rotation.eulerAngles.y == 180f)
        {
            arrow.GetComponent<SpriteRenderer>().flipX = false;
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(-75f, 0f));
        }
        
        StartCoroutine(waitToDestroyArrow(arrow));
    }

    IEnumerator waitToDestroyArrow(GameObject arrow)
    {
        yield return new WaitForSeconds(3f);
        Destroy(arrow);
    }
}
