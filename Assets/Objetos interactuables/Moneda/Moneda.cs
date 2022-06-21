using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public GameObject moneda;
    public CoinsCounter counter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D)
        {
            moneda.SetActive(false);
            SistemaMonedas.AddCoin();
            counter.GetComponent<CoinsCounter>().CoinHUD();
        }
    }
}
