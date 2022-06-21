using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalTotemMi : MonoBehaviour
{
    public Animator animator;
    public bool isInRange;
    public bool isOn;
    public Verja verja;
    public string subtituloTexto = "";
    public static bool isTrigger = false;

    void Awake()
    {
        isOn = false;
        isInRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
    void Update()
    {
        if (isInRange && !isOn && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Activate", true);
            verja.OpenVerja();
            isOn = true;
            isTrigger = true;
            subtituloTexto = "Nota Mi";

            if (MusicalTotemManager.notaTotems == 2)
            {
                MusicalTotemManager.notaTotems++;
            }
            else
            {
                MusicalTotemManager.wrongOrder = true;
            }
            Debug.Log("Orden " + MusicalTotemManager.notaTotems.ToString());
            Debug.Log("wrongOrder " + MusicalTotemManager.wrongOrder.ToString());
        }
    }

    public void resetTotem()
    {
        animator.SetBool("Activate", false);
        verja.CloseVerja();
        isOn = false;
    }
}
