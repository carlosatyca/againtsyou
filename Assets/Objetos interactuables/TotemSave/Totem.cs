using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    public PlayerHealth player;
    private int timeSaved;
    public Animator animatorTotem;
    private bool onTotem = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Solo recarga una vez la vida y pociones pero guardar las que quiera
        timeSaved = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTotem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTotem = false;
        }
    }

    void Save()
    {
        //activacion del texto de guardando
        gameObject.transform.parent.Find("SaveTextCanvas").gameObject.SetActive(true);

        animatorTotem.SetTrigger("Save");
        player.GetComponent<Animator>().SetBool("isPraying", true);
        //Recarga la vida al completo
        if (player.currentHealth != player.maxHealth && timeSaved == 0)
        {
            player.currentHealth += (player.maxHealth - player.currentHealth);
        }
        //Recarga las pociones
        if (player.numPotions <= 2 && timeSaved == 0)
        {
            player.numPotions = 2;
        }
        //Guardar la partida
        SaveSystem.SavePlayer(player);

        //Solo puede recargar vida y pociones 1 vez
        timeSaved++;

        StartCoroutine("stopPraying");

        StartCoroutine(disableSaveText());

    }


    void Update()
    {
        if(onTotem && Input.GetKeyDown(KeyCode.F))
        {
            Save();
        }
    }

    private IEnumerator stopPraying()
    {
        yield return new WaitForSeconds(0.4f);
        player.GetComponent<Animator>().SetBool("isPraying", false);
    }

    IEnumerator disableSaveText()
    {
        yield return new WaitForSeconds(1.5f);
        //desactivacion del texto de guardando
        gameObject.transform.parent.Find("SaveTextCanvas").gameObject.SetActive(false);
    }

}
