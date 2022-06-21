using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalTotemManager : MonoBehaviour
{
    private MusicalTotemDo TotemDo;
    private MusicalTotemRe TotemRe;
    private MusicalTotemMi TotemMi;
    private MusicalTotemFa TotemFa;
    private MusicalTotemSol TotemSol;

    public static int notaTotems= 0;
    public static bool wrongOrder = false;

    void Awake()
    {
        wrongOrder = false;
        notaTotems = 0;
        TotemDo = gameObject.transform.Find("Do").GetComponent<MusicalTotemDo>();
        TotemRe = gameObject.transform.Find("Re").GetComponent<MusicalTotemRe>();
        TotemMi = gameObject.transform.Find("Mi").GetComponent<MusicalTotemMi>();
        TotemFa = gameObject.transform.Find("Fa").GetComponent<MusicalTotemFa>();
        TotemSol = gameObject.transform.Find("Sol").GetComponent<MusicalTotemSol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wrongOrder)
        {
            resetAllTotems();
        }
    }

    public void resetAllTotems()
    {
        wrongOrder = false;
        notaTotems = 0;
        TotemDo.resetTotem();
        TotemRe.resetTotem();
        TotemMi.resetTotem();
        TotemFa.resetTotem();
        TotemSol.resetTotem();
    }
}
