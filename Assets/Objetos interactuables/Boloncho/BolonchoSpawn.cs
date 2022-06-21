using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolonchoSpawn : MonoBehaviour
{
    public Boloncho boloncho;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(esperaCD());      
        }
    }
    IEnumerator esperaCD()
    {
        yield return new WaitForSeconds(1f);
        boloncho.SpawnBoloncho();
    }
}
