using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFinalIA : MonoBehaviour
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
    public float maxHealth = 100f;
    public float currentHealth;
    public bool cooldown;
    private float hurtTime = 0f;
    private int randomAttack;
    public GameObject boss1LimitRight;
    public GameObject pauseMenu;
    public GameObject transicionFases;
    public GameObject boss2LimitRight;


    private void Awake()
    {
        SelectTarget();
        isAttacking = false;
        currentHealth = maxHealth;
    }

    private void GetPatrollingPosition()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BossFinal_Attack")&& !animator.GetCurrentAnimatorStateInfo(0).IsName("BossFinal_HardAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("BossFinal_Health"))
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
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    public void Flip()
    {

        Vector3 rotation = transform.eulerAngles;

        rotation.y = (target.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }

    public void TakeDmg(float damage)
    {
        if (hurtTime < Time.time)
        {
            hurtTime = Time.time + 2f;
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

    void Die()
    {
        //Death animation
        animator.SetBool("isDead", true);
        //Esqueleto disabled
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - 1.5f, GetComponent<Transform>().position.z);
        
        this.enabled = false;

        if (boss1LimitRight.activeSelf == true && !boss2LimitRight.activeSelf)
        {
            boss1LimitRight.SetActive(false);
        } else if (boss2LimitRight.activeSelf == true)
        {
            //despues de morir transicion al final del juego
            StartCoroutine(endGame());
        }
    }
    
    private void prepareEndGame()
    {
        Time.timeScale = 0f;
        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = false;
        }

        //desactivar la posibilidad de acceder al menu de pausa mientras transicionan las fases
        pauseMenu.SetActive(false);
        transicionFases.SetActive(true);
    }

    IEnumerator endGame()
    {
        yield return new WaitForSecondsRealtime(3f);

        prepareEndGame();

        //pantalla de transicion
        transicionFases.transform.Find("Crossfade").GetComponent<Animator>().SetTrigger("DieTransition");
        yield return new WaitForSecondsRealtime(1.5f);

        transicionFases.transform.Find("Text").gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    IEnumerator esperaCD(float seconds)
    {
        yield return new WaitForSeconds(seconds);

    }

    void RollTheDice()
    {
        randomAttack = Random.Range(0, 5);
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
                animator.SetBool("Attack", false);
                animator.SetBool("HardAttack", false);
                animator.SetBool("Health", false);
            }
            if (distance <= attackDistance && !cooldown)
            {
                animator.SetBool("canWalk", false);
                //ATACA
                
                isAttacking = true;
                RollTheDice();
                if (randomAttack == 0 || randomAttack == 3)
                {
                    if (player.GetComponent<PlayerHealth>().currentHealth > 0)
                    {
                        animator.SetBool("Attack", true);
                        nextAttackTime = Time.time + 1f;
                    }
                }
                if (randomAttack == 1 || randomAttack == 5)
                {
                    if (player.GetComponent<PlayerHealth>().currentHealth > 0)
                    {
                        animator.SetBool("HardAttack", true);
                        nextAttackTime = Time.time + 1.5f;
                    }
                }
                if (randomAttack == 2 && currentHealth < maxHealth/2)
                {
                    if (player.GetComponent<PlayerHealth>().currentHealth > 0)
                    {
                        animator.SetBool("Health", true);
                        nextAttackTime = Time.time + 1f;
                        currentHealth = currentHealth + (maxHealth / 4) > maxHealth ? maxHealth : (currentHealth + (maxHealth / 4));
                        StartCoroutine(esperaCuracion());
                    }
                }

                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    animator.SetBool("canWalk", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("HardAttack", false);
                    animator.SetBool("Health", false);
                    this.enabled = false;
                }
            }
            if (distance <= attackDistance && cooldown)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("HardAttack", false);
                animator.SetBool("AirAttack", false);
                StartCoroutine(esperaCD(nextAttackTime - Time.time));
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
