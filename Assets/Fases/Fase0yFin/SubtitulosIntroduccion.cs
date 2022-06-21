using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubtitulosIntroduccion : MonoBehaviour
{
    private float typeSpeed = 15f;
    private GameObject textObject;
    private string text1 = "En un dia lluvioso en el reino de Sintusa, se encontraba un caballero en su lecho descansando " +
            "tras un duro dia en el que habia conseguido encontrar el suficiente alimento para poder alimentar a su hijo. " +
            "\n\nParecia un dia normal y corriente, como otro cualquiera. Pero... sin embargo nadie era consciente de lo que estaba a punto de ocurrir, " +
            "tras aquel gran estruendo. Algo muy raro se estaba perpetrando.";
    private string text2 = "Poco mas tarde, se levantaria empapado en sudor de su lecho. Tras observar a su alrededor se daria cuenta de algo inusual, " +
                            "una enigmatica nota habia sido dejada a los pies de su cama.";
    private string text3 = "Enzo... no te preocupes, ya va papa a buscarte.";
    public GameObject acertijo0;
    private GameObject Background;

    void Awake()
    {
        Cursor.visible = false;
        textObject = gameObject.transform.Find("Texto").gameObject;
        Background = gameObject.transform.Find("Background").gameObject;
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSecondsRealtime(4f);

        Background.GetComponent<Animator>().SetTrigger("LightFlash");
        yield return new WaitForSecondsRealtime(3.5f);

        yield return(TypeText(text1, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(2.5f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);
        
        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        yield return (TypeText(text2, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(2f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(1f);
        yield return (Acertijo0Routine());
        
        yield return new WaitForSecondsRealtime(1f);

        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        yield return (TypeText(text3, textObject.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSecondsRealtime(1.5f);
        clearSubtitulos();

        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator TypeText(string text, TextMeshProUGUI textLabel)
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
            textObject.GetComponent<AudioSource>().enabled = true;
            yield return null;
        }
        textLabel.text = text;
        textObject.GetComponent<AudioSource>().enabled = false;
    }

    IEnumerator Acertijo0Routine()
    {
        acertijo0.GetComponent<Acertijo0>().Pause();
        while (!Acertijo0.resolved)
        {
            yield return null;
        }
        yield return null;
    }

    void clearSubtitulos()
    {
        textObject.GetComponent<TextMeshProUGUI>().text = "";
    }
}
