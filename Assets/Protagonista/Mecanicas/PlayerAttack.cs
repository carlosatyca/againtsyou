using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D controller;
   
    //Attack stuff
    public Transform attackPoint;
    public float attackRange = 0.92f;
    public LayerMask enemyLayer;
    
    //Attack1 stuff
    public float attackDmg1 = 5f;
    public float attackRate1 = 1f;
    float nextAttackTime = 0f;
    
    //Attack2 stuff
    public float attackDmg2 = 7f;
    public float downTime = 0f;
    public float pressedTime = 0f;
    public float attack2Delay = 2f;
    public bool canAttack2 = false;
    float nextAttackTime2 = 0f;
    public float attackRate2 = 3f;

    //Air Attack stuff
    public float attackDmg3 = 6f;
    public float attackRate3 = 1f;
    float nextAttackTime3 = 0f;
    public Transform airAttackPoint;
    public float airAttackRange = 1.71f;


 
    // Update is called once per frame
    void Update()
    {
        //Check Fury TimeOut
        FuryCounter.CheckTimer();

        //Fast attack
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !controller.usingLadder && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Slide"))
            {
                this.airAttackRange = 0f;
                Attack1();
                nextAttackTime = Time.time + 1f / attackRate1;
            }

        }

        //Strong attack
        if (Time.time >= nextAttackTime2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack2 == false && !controller.usingLadder)
            {
                animator.SetTrigger("attack2Charged");
                downTime = Time.time;
                pressedTime = downTime + attack2Delay;
                canAttack2 = true;
                animator.SetBool("attack2Cancel", !canAttack2);
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && !controller.usingLadder)
            {
                canAttack2 = false;
                animator.SetBool("attack2Cancel",!canAttack2);
            
            } 
            if(Time.time >= pressedTime && canAttack2 == true && !controller.usingLadder)
            {
                animator.SetBool("attack2Cancel", !canAttack2);
                canAttack2 = false;
                this.airAttackRange = 0f;
                Attack2();
                nextAttackTime2 = Time.time + 2f / attackRate2;
            }
        }

        //Air attack
        if (Time.time >= nextAttackTime3)
        { 
            if (!controller.GetComponent<CharacterController2D>().PlayerIsGrounded() && Input.GetKeyDown(KeyCode.Mouse0) && !controller.usingLadder)
            {
                animator.SetBool("isGrounded", false);
                this.attackRange = 0f;
                AirAttack();
                nextAttackTime3 = Time.time + 2f / attackRate3;
            }
            if (controller.GetComponent<CharacterController2D>().PlayerIsGrounded())
            {
                animator.SetBool("isGrounded", true);
            }
        }

    }

    void Attack1()
    {
        //Play attack animation
        animator.SetTrigger("attack1");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayer);
        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if (FuryCounter.isFuryLevel1)
            {
                Debug.Log("Level - 1"); 
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg1 * 1.1f);
                        break; 
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg1 * 1.1f);
                        break;
                }
            }
            else if(FuryCounter.isFuryLevel2)
            {
                Debug.Log("Level - 2");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg1 * 1.2f);
                        break;
                }
            }
            else if(FuryCounter.isFuryLevel3)
            {
                Debug.Log("Level - 3");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg1 * 1.3f);
                        break;
                }
            }
            else
            {
                Debug.Log("Level - 0");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg1);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg1);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg1);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg1);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg1);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg1);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg1);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg1);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg1);
                        break;
                }
            }
             
        }
        this.airAttackRange = 1.71f;
    }
    //quizas mirar para el ataque anterior que si clickamos hago otro combo
    void Attack2()
    {
        //Play attack animation
        animator.SetTrigger("attack2");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if (FuryCounter.isFuryLevel1)
            {
                Debug.Log("LevelP - 1");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg2 * 1.1f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg2 * 1.1f);
                        break;

                }

            }
            else if (FuryCounter.isFuryLevel2)
            {
                Debug.Log("LevelP - 2");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg2 * 1.2f);
                        break;
                }
            }
            else if (FuryCounter.isFuryLevel3)
            {
                Debug.Log("LevelP - 3");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg2 * 1.3f);
                        break;
                }
            }
            else
            {
                Debug.Log("LevelP - 0");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg2);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg2);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg2);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg2);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg2);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg2);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg2);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg2);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg2);
                        break;
                }
            }
            
        }
        this.airAttackRange = 1.71f;
    }

    void AirAttack()
    {
        //Play attack animation
        animator.SetTrigger("airAttack");
        //Detect enemies in range of attack
        Collider2D[] AirHitEnemies = Physics2D.OverlapCircleAll(airAttackPoint.position, airAttackRange, enemyLayer);
        //Damage enemies
        foreach (Collider2D enemy in AirHitEnemies)
        {
            if (FuryCounter.isFuryLevel1)
            {
                Debug.Log("LevelA - 1");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg3 * 1.1f);
                        break;
                }
            }
            else if (FuryCounter.isFuryLevel2)
            {
                Debug.Log("LevelA - 2");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg3 * 1.2f);
                        break;
                }
            }
            else if (FuryCounter.isFuryLevel3)
            {
                Debug.Log("LevelA - 3");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg3 * 1.3f);
                        break;
                }
            }
            else
            {
                Debug.Log("LevelA - 0");
                switch (enemy.name)
                {
                    case "BossFinal":
                        enemy.GetComponent<BossFinalIA>().TakeDmg(attackDmg3);
                        break;
                    case "Ent":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3);
                        break;
                    case "EntNoFollow":
                        enemy.GetComponent<EnemyIA_Ent>().TakeDmg(attackDmg3);
                        break;
                    case "EsqueletoLanza":
                        enemy.GetComponent<EnemyIA>().TakeDmg(attackDmg3);
                        break;
                    case "EsqueletoEscudo":
                        enemy.GetComponent<EnemyIA_EsqueletoEscudo>().TakeDmg(attackDmg3);
                        break;
                    case "Calabazas":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3);
                        break;
                    case "CalabazasNoFollow":
                        enemy.GetComponent<EnemyIA_Calabazas>().TakeDmg(attackDmg3);
                        break;
                    case "AlmaErrante":
                        enemy.GetComponent<EnemyIA_AlmaErrante>().TakeDmg(attackDmg3);
                        break;
                    case "EsqueletoEspadaLarga":
                        enemy.GetComponent<EnemyIA_EsqueletoEspadaLarga>().TakeDmg(attackDmg3);
                        break;
                    case "Arquero":
                        enemy.GetComponent<EnemyIA_Arquero>().TakeDmg(attackDmg3);
                        break;
                    case "Barril":
                        enemy.GetComponent<Barril>().TakeDmg(attackDmg3);
                        break;
                }
            }
        }
        this.attackRange = 1f;
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        if (airAttackPoint != null)
        {
            Gizmos.DrawWireSphere(airAttackPoint.position, airAttackRange);
        }

    }
}
