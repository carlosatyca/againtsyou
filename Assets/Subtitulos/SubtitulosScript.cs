using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SubtitulosScript : MonoBehaviour
{
    private float typeSpeed = 50f;
    public GameObject subtituloBox;
    //Fase1
    public Fase1Subtitulos fase1Subtitulos;
    public Fase1_Subtitulos2 fase1_Subtitulos2;
    //Fase2
    public Fase2_Subtitulos1 fase2_Subtitulos1;
    public Fase2_Subtitulos2 fase2_Subtitulos2;
    public Acertijo2 acertijo2_Subtitulos;
    public NotaScript nota_Subtitulos;
    //Fase 3
    public Fase3_Subtitulos1 fase3_Subtitulos1;
    //Fase 4
    public Fase4_Subtitulos1 fase4_Subtitulos1;
    //Fase 5
    public Fase5_Subtitulos1 fase5_Subtitulos1;
    //Fase 6
    public Fase6_Subtitulos1 fase6_Subtitulos1;
    
    public MusicalTotemDo totemDo;
    public MusicalTotemRe totemRe;
    public MusicalTotemMi totemMi;
    public MusicalTotemFa totemFa;
    public MusicalTotemSol totemSol;


    public BossFinal1Trigger fase6_Subtitulos2;
    public BossFinal2Trigger fase6_Subtitulos3;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().enabled = false;
        subtituloBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Fase1Subtitulos.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase1Subtitulos.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(10f));
            Fase1Subtitulos.isTrigger = false;
        }
        else if (Fase1_Subtitulos2.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase1_Subtitulos2.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Fase1_Subtitulos2.isTrigger = false;
        }
        if (Fase2_Subtitulos1.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase2_Subtitulos1.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(10f));
            Fase2_Subtitulos1.isTrigger = false;
        }else if (NotaScript.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(nota_Subtitulos.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            NotaScript.isTrigger = false;
        }
        else if (Acertijo2.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(acertijo2_Subtitulos.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Acertijo2.isTrigger = false;
        }
        else if (Fase2_Subtitulos2.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase2_Subtitulos2.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(10f));
            Fase2_Subtitulos2.isTrigger = false;
        }
        if (Fase3_Subtitulos1.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase3_Subtitulos1.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Fase3_Subtitulos1.isTrigger = false;
        }
        if (Fase4_Subtitulos1.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase4_Subtitulos1.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Fase4_Subtitulos1.isTrigger = false;
        }
        if (Fase5_Subtitulos1.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase5_Subtitulos1.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Fase5_Subtitulos1.isTrigger = false;
        }
        if (Fase6_Subtitulos1.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase6_Subtitulos1.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            Fase6_Subtitulos1.isTrigger = false;
        }
        if (BossFinal1Trigger.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase6_Subtitulos2.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(8f));
            BossFinal1Trigger.isTrigger = false;
        }
        else if (BossFinal2Trigger.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(fase6_Subtitulos3.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(5f));
            BossFinal2Trigger.isTrigger = false;
        }
        if (MusicalTotemDo.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(totemDo.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(3.5f));
            MusicalTotemDo.isTrigger = false;
        }
        else if (MusicalTotemRe.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(totemRe.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(3.5f));
            MusicalTotemRe.isTrigger = false;
        }
        else if (MusicalTotemMi.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(totemMi.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(3.5f));
            MusicalTotemMi.isTrigger = false;
        }
        else if (MusicalTotemFa.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(totemFa.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(3.5f));
            MusicalTotemFa.isTrigger = false;
        }
        else if (MusicalTotemSol.isTrigger)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            subtituloBox.SetActive(true);
            Run(totemSol.subtituloTexto, GetComponent<TextMeshProUGUI>());
            StartCoroutine(subtitulosCD(3.5f));
            MusicalTotemSol.isTrigger = false;
        }
    }
   void clearSubtitulos()
    {
        GetComponent<TextMeshProUGUI>().text = "";
        subtituloBox.SetActive(false);
        GetComponent<TextMeshProUGUI>().enabled = false;
    }

    IEnumerator subtitulosCD(float time)
    {
        yield return new WaitForSeconds(time);
        clearSubtitulos();
    }
    
    public void Run(string text, TextMeshProUGUI textLabel)
    {
        StartCoroutine(TypeText(text, textLabel));
    }
    private IEnumerator TypeText(string text, TextMeshProUGUI textLabel)
    {
        yield return new WaitForSeconds(0.1f);
        float t = 0;
        int charIndex = 0;
        while(charIndex < text.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);
            textLabel.text = text.Substring(0, charIndex);
            yield return null;
        }
        textLabel.text = text;
    }
}
