using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float currentHealth;
    public int numPotions;
    public float[] position;
    public string level;
    public int numMonedas;
    public bool resolved0;
    public bool resolved1;
    public bool resolved2;
    public int numIntentosAcertijo2;
    public bool resolved3;
    public bool resolved4;
    public bool resolved5;
    public bool resolved6;
    public float playedTime;
    public int numMuertes;
    public int numEnemigosEliminados;

    public bool isHazLaser1Desactivated = false;
    public bool isHazLaser2Desactivated = false;
    public bool isHazLaser3Desactivated = false;

    public PlayerData (PlayerHealth player)
    {
        currentHealth = player.currentHealth;
        numPotions = player.numPotions;
        numMonedas = SistemaMonedas.numMonedas;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        //acertijos resueltos
        resolved0 = Acertijo0.resolved;
        resolved1 = Acertijo1.resolved;
        resolved2 = Acertijo2.resolved;
        numIntentosAcertijo2 = Acertijo2.numIntentos;
        resolved3 = Acertijo3.resolved;
        resolved4 = Acertijo4.resolved;
        resolved5 = Acertijo5.resolved;
        resolved6 = Acertijo6.resolved;

        playedTime = PlayerHealth.playedTime + (Time.time - PlayerHealth.startTime);

        numMuertes = PlayerHealth.numMuertes;
        numEnemigosEliminados = PlayerHealth.numEnemigosEliminados;

        level = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "Fase-5.2")
        {
            isHazLaser1Desactivated = player.HazLaser1.GetComponent<HazLaserActivacion>().isDeactivated;
            isHazLaser2Desactivated = player.HazLaser2.GetComponent<HazLaserActivacion>().isDeactivated;
            isHazLaser3Desactivated = player.HazLaser3.GetComponent<HazLaserActivacion>().isDeactivated;
        }
    }

}
