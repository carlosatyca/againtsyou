using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedToDynamic : MonoBehaviour
{
    public GameObject pared;
    public GameObject enemigos;

    private void Update()
    {
        EnemyIA_EsqueletoEspadaLarga esqueletoEspadaLarga3 = enemigos.transform.Find("EsqueletoEspadaLarga3").Find("EsqueletoEspadaLarga").GetComponent<EnemyIA_EsqueletoEspadaLarga>();
        EnemyIA_AlmaErrante almaErrante1 = enemigos.transform.Find("Alma1").Find("AlmaErrante").GetComponent<EnemyIA_AlmaErrante>();

        if (esqueletoEspadaLarga3.currentHealth <= 0 && almaErrante1.currentHealth <= 0)
        {
            pared.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pared.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
}
