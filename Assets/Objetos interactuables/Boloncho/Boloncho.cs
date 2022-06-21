using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boloncho : MonoBehaviour
{
    public GameObject boloncho;
    void Start()
    {
        boloncho.SetActive(false);
    }
    public void SpawnBoloncho()
    {
        boloncho.SetActive(true);
        GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(660f, 0f));
        GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(3 * Time.deltaTime, 0f);
    }

}
