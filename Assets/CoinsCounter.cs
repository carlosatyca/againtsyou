using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsCounter : MonoBehaviour
{

    private void Awake()
    {
        CoinHUD(); 
    }

    public void CoinHUD()
    {
        GetComponent<TextMeshProUGUI>().text = "x" + SistemaMonedas.numMonedas;
    }
}
