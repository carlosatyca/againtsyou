using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubtitulosFin : MonoBehaviour
{
    private float typeSpeed = 12.5f;
    private GameObject textObject;
    
    private string text1 = "Todo aquello vivido por el caballero, fue fruto de una pesadilla. De hecho, aun sigue durmiendo, debido a la enfermedad que contrajo " +
        "hace meses.";

    private string text2 = "Su hijo Enzo, desaparecio hace varios meses sin dejar rastro. \n\nNadie en la villa supo que le sucedio. Algunos rumores hablaron " +
        "de un rapto, mientras que otros acusaron al padre como principal sospechoso de su desaparicion.";

    private string text3 = "Pese a todo ello, nuestro protagonista nunca se dio por vencido y estuvo buscando a su hijo sin cesar... hasta que en uno " +
        "de sus viajes recibio una picadura de un animal que le provoco la enfermedad que hasta hoy no le permite levantarse de su lecho.";

    private string text6 = "Against You.\n\nVideojuego desarrollado para el Trabajo de Fin de Grado por Carlos Atienza Carretero y Tomas Galera Barrera durante " +
    "el curso 2021/2022";

    private string text7 = "GRACIAS POR JUGAR";

    private string text8 = "Una noche, un nuevo rayo cayo cerca de la casa e ilumino toda la habitacion. El protagonista sintio un cosquilleo que le hizo abrir " +
        "los ojos";
    
    private string text5 = "CONTINUARA...";

    void Awake()
    {
        Cursor.visible = false;

        gameObject.SetActive(false);
        gameObject.SetActive(true);

        textObject = gameObject.transform.Find("Texto").gameObject;
        Time.timeScale = 1f;

        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSecondsRealtime(5f);

        yield return (TypeText(text1, textObject.GetComponent<TextMeshProUGUI>()));

        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);
        
        yield return (TypeText(text2, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();
        
        yield return new WaitForSecondsRealtime(0.5f);

        yield return (TypeText(text3, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);

        yield return (TypeText(text6, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();

        textObject.GetComponent<TextMeshProUGUI>().fontSize = 200;
        yield return (TypeText(text7, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);

        textObject.GetComponent<TextMeshProUGUI>().fontSize = 80;
        yield return (TypeText(text8, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(3f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);

        textObject.GetComponent<TextMeshProUGUI>().fontSize = 250;
        yield return (TypeText(text5, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(7f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(3.5f);

        Cursor.visible = true;
        SceneManager.LoadScene("MainMenuScene");
    }

    IEnumerator TypeText(string text, TextMeshProUGUI textLabel)
    {
        yield return new WaitForSeconds(0.1f);

        float t = 0;
        int charIndex = 0;
        while (charIndex < text.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);
            textLabel.text = text.Substring(0, charIndex);
            textObject.GetComponent<AudioSource>().enabled = true;
            yield return null;
        }
        textLabel.text = text;
        textObject.GetComponent<AudioSource>().enabled = false;
    }

    void clearSubtitulos()
    {
        textObject.GetComponent<TextMeshProUGUI>().text = "";
    }
}
