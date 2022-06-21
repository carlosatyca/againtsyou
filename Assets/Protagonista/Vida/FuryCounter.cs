using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryCounter
{
    public static bool isFuryLevel0;
    public static bool isFuryLevel1;
    public static bool isFuryLevel2;
    public static bool isFuryLevel3;

    [SerializeField] private static int hitsUntilFuryLevel1 = 5;
    [SerializeField] private static int hitsUntilFuryLevel2 = 10;
    [SerializeField] private static int hitsUntilFuryLevel3 = 15;
    [SerializeField] private static float furyDurationWithoutHit = 7f;

    public static int hitCounter;
    private static float lastHitTime = 0f;
    private static float furyResetTimer;


    //Si alcanzamos el nivel de furia X y pasa el tiempo se quita
    public static void CheckTimer()
    {
        //Nivel de furia
        if (isFuryLevel0 || isFuryLevel1 || isFuryLevel2 || isFuryLevel3)
        {
            furyResetTimer -= Time.deltaTime;
            if(furyResetTimer <= 0)
            {
                isFuryLevel1 = true;
                isFuryLevel1 = false;
                isFuryLevel2 = false;
                isFuryLevel3 = false;
                hitCounter = 0;
            }
        }
    }
 
    public static void AddHit(bool wasBeaten)
    {       
        if (Time.time - lastHitTime < furyDurationWithoutHit && !wasBeaten)
       {
            hitCounter++;

            if (hitCounter < hitsUntilFuryLevel1)
            {
                isFuryLevel0 = true;
                isFuryLevel1 = false;
                isFuryLevel2 = false;
                isFuryLevel3 = false;
                furyResetTimer = furyDurationWithoutHit;
            }
            else if (hitCounter >= hitsUntilFuryLevel1 && hitCounter < hitsUntilFuryLevel2)
            {
                isFuryLevel0 = false;
                isFuryLevel1 = true;
                isFuryLevel2 = false;
                isFuryLevel3 = false;
                furyResetTimer = furyDurationWithoutHit;
            }
            else if (hitCounter >= hitsUntilFuryLevel2 && hitCounter < hitsUntilFuryLevel3)
            {
                isFuryLevel0 = false;
                isFuryLevel1 = false;
                isFuryLevel2 = true;
                isFuryLevel3 = false;
                furyResetTimer = furyDurationWithoutHit;
            }
            else if (hitCounter >= hitsUntilFuryLevel3)
            {
                isFuryLevel0 = false;
                isFuryLevel1 = false;
                isFuryLevel2 = false;
                isFuryLevel3 = true;
                furyResetTimer = furyDurationWithoutHit;
            }
        }
        else if (wasBeaten)
        {
            isFuryLevel0 = false;
            isFuryLevel1 = false;
            isFuryLevel2 = false;
            isFuryLevel3 = false;
            hitCounter = 0;
        }
        else if(Time.time - lastHitTime >= furyDurationWithoutHit)
        {
            isFuryLevel0 = false;
            isFuryLevel1 = false;
            isFuryLevel2 = false;
            isFuryLevel3 = false;
            hitCounter = 1;
        }
        
        Debug.Log(hitCounter);
        lastHitTime = Time.time;
    }


}
