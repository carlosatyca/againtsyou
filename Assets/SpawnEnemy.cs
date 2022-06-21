using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    private float timer;
    private float endTimer;
    public GameObject esqueletoEspadaLarga;
    public GameObject esqueletoEscudo;
    private float nextSpawn;
    private Vector3 spawnPoint;

    void Start()
    {
        nextSpawn = 0f;
        spawnPoint = new Vector3(-17.44f, -10.91f, -94.39372f);
    }
    void Update()
    {
        if (endTimer > timer)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + 10f;
                int randomAttack = Random.Range(0, 3);
                if (randomAttack == 1)
                {
                    Instantiate(esqueletoEscudo, spawnPoint, Quaternion.identity);
                }
                else if (randomAttack == 2)
                {
                    Instantiate(esqueletoEscudo, spawnPoint, Quaternion.identity);

                }
                else
                {
                    Instantiate(esqueletoEscudo, spawnPoint, Quaternion.identity);
                }
            }
        }
        else
        {
            this.enabled = false;
        }
    }
    void SpawnRandomlyEnemy()
    {
        timer = Time.time;
        endTimer = Time.time + 90f;
        Debug.Log(timer);
        Debug.Log(endTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            SpawnRandomlyEnemy();
        }
    }


    }
