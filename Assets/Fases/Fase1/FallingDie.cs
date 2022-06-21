using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDie : MonoBehaviour
{
    public PlayerHealth player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(player.currentHealth);
        }
        //Para que mueran los enemigos
        if (collision.gameObject.layer == 6)
        {
            switch (collision.name)
            {
                
                case "Ent":
                    collision.GetComponent<EnemyIA_Ent>().DieByPinchos(collision.GetComponent<EnemyIA_Ent>().currentHealth);
            break;
            case "EntNoFollow":
                    collision.GetComponent<EnemyIA_Ent>().DieByPinchos(collision.GetComponent<EnemyIA_Ent>().currentHealth);
            break;
                case "EsqueletoLanza":
                    collision.GetComponent<EnemyIA>().DieByPinchos(collision.GetComponent<EnemyIA>().currentHealth);
            break;
                case "EsqueletoEscudo":
                    collision.GetComponent<EnemyIA_EsqueletoEscudo>().DieByPinchos(collision.GetComponent<EnemyIA_EsqueletoEscudo>().currentHealth);
            break; 
                case "Calabazas":
                    collision.GetComponent<EnemyIA_Calabazas>().DieByPinchos(collision.GetComponent<EnemyIA_Calabazas>().currentHealth);
            break;
                case "CalabazasNoFollow":
                    collision.GetComponent<EnemyIA_Calabazas>().DieByPinchos(collision.GetComponent<EnemyIA_Calabazas>().currentHealth);
            break;
                case "AlmaErrante":
                    collision.GetComponent<EnemyIA_AlmaErrante>().DieByPinchos(collision.GetComponent<EnemyIA_AlmaErrante>().currentHealth);
            break;
                case "EsqueletoEspadaLarga":
                    collision.GetComponent<EnemyIA_EsqueletoEspadaLarga>().DieByPinchos(collision.GetComponent<EnemyIA_EsqueletoEspadaLarga>().currentHealth);
            break;
                case "Arquero":
                    collision.GetComponent<EnemyIA_Arquero>().DieByPinchos(collision.GetComponent<EnemyIA_Arquero>().currentHealth);
            break;
            }
        }

    }
}
