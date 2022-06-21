using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //Health main character stuff
    public float maxHealth = 100f;
    public int numPotions;
    public float currentHealth;
    public Animator animator;
    public GameObject deathTransition;
    public GameObject PauseMenu;
    public HealthBar healthBar;
    public FuryBar furyBar;
    public GameObject Potions;
    public static float playedTime = 0;
    public static float startTime = -1;
    public static int numMuertes = 0;
    public static int numEnemigosEliminados = 0;

    public GameObject HazLaser1;
    public GameObject HazLaser2;
    public GameObject HazLaser3;

    void Start()
    {   
        if (SaveSystem.buttonPressed == "NewGame")
        {
            currentHealth = maxHealth;
            numPotions = 2;
            SistemaMonedas.numMonedas = 0;
            healthBar.transform.parent.Find("Coins").Find("CoinsCounter").GetComponent<CoinsCounter>().CoinHUD();

            startTime = Time.time;
            SaveSystem.SavePlayer(this);
        }
        else if (SaveSystem.buttonPressed == "LoadGame")
        {
            PlayerData data = SaveSystem.LoadPlayer();
            playedTime = data.playedTime;
            startTime = startTime != -1 ? startTime : Time.time;

            currentHealth = data.currentHealth;
            numPotions = data.numPotions;
            numMuertes = numMuertes != 0 ? numMuertes : data.numMuertes;
            numEnemigosEliminados = data.numEnemigosEliminados;
            
            SistemaMonedas.numMonedas = data.numMonedas;
            healthBar.transform.parent.Find("Coins").Find("CoinsCounter").GetComponent<CoinsCounter>().CoinHUD();

            //acertijos resueltos
            Acertijo0.resolved = data.resolved0;
            Acertijo1.resolved = data.resolved1;
            Acertijo2.resolved = data.resolved2;
            Acertijo2.numIntentos = data.numIntentosAcertijo2;
            Acertijo3.resolved = data.resolved3;
            Acertijo4.resolved = data.resolved4;
            Acertijo5.resolved = data.resolved5;
            Acertijo6.resolved = data.resolved6;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;

            if (SceneManager.GetActiveScene().name == "Fase-5.2")
            {
                if (data.isHazLaser1Desactivated)
                {
                    HazLaser1.GetComponent<HazLaserActivacion>().DeactivadedHaz();
                }

                if (data.isHazLaser2Desactivated)
                {
                    HazLaser2.GetComponent<HazLaserActivacion>().DeactivadedHaz();
                }

                if (data.isHazLaser3Desactivated)
                {
                    HazLaser3.GetComponent<HazLaserActivacion>().DeactivadedHaz();
                }
            }
        }
        else if (SaveSystem.buttonPressed == "levelTransition")
        {
            PlayerData data = SaveSystem.LoadPlayer();
            playedTime = data.playedTime;

            currentHealth = data.currentHealth;
            numPotions = data.numPotions;
            
            //numMuertes = data.numMuertes;
            numEnemigosEliminados = data.numEnemigosEliminados;

            SistemaMonedas.numMonedas = data.numMonedas;
            healthBar.transform.parent.Find("Coins").Find("CoinsCounter").GetComponent<CoinsCounter>().CoinHUD();

            //acertijos resueltos
            Acertijo0.resolved = data.resolved0;
            Acertijo1.resolved = data.resolved1;
            Acertijo2.resolved = data.resolved2;
            Acertijo2.numIntentos = data.numIntentosAcertijo2;
            Acertijo3.resolved = data.resolved3;
            Acertijo4.resolved = data.resolved4;
            Acertijo5.resolved = data.resolved5;
            Acertijo6.resolved = data.resolved6;

            SaveSystem.SavePlayer(this);
        }
        else
        {
            currentHealth = maxHealth;
            numPotions = 2;
            SistemaMonedas.numMonedas = 0;
            healthBar.transform.parent.Find("Coins").Find("CoinsCounter").GetComponent<CoinsCounter>().CoinHUD();
            SaveSystem.SavePlayer(this);
        }

        //set max health and update HUD health
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        //set fury level start
        FuryCounter.hitCounter = 0;
        furyBar.SetMaxFury();

        //set potions hud
        HUDPotions();

        //set fury text
        FuryTextDisable();

        //unpause game
        Time.timeScale = 1f;

        //set button press to default
        SaveSystem.buttonPressed = "";
    }
    void Update()
    {
        //Hay que borrar es prueba
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDmg(5f);
            //Hurt animation
            animator.SetTrigger("Hurt");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PotionsHealing();
        }

        //update HUD health
        healthBar.SetHealth(currentHealth);

        //set fury level update
        furyBar.SetFuryLevel(FuryCounter.hitCounter);

        //set potions hud
        HUDPotions();

        //set fury counter text
        FuryTextUpdate();
    }

    public void PotionsHealing()
    {
        //Tenemos el numero de pociones y no es igual a la vida maxima
        if(numPotions>0 && numPotions <= 2 && maxHealth > 0 && currentHealth != maxHealth)
        {
            animator.SetTrigger("Healing");
            if(currentHealth + 40 >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += 40f;
            }
            numPotions--;
        }
    }

    public void TakeDmg(float damage)
    {
        currentHealth -= damage;
        FuryCounter.AddHit(true);
        //Hurt animation
        animator.SetTrigger("Hurt");
        if ( currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Death animation
        healthBar.SetHealth(0f);
        animator.SetBool("isDead", true);
        //Main character game over screen and disabled
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        this.enabled = false;

        //desactivar posibilidad de acceder al menu de pausa al morir
        PauseMenu.SetActive(false);

        numMuertes++;

        StartCoroutine(waitDeathAnimation());
    }

    IEnumerator waitDeathAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        deathTransition.transform.parent.gameObject.SetActive(true);
        deathTransition.GetComponent<Animator>().SetTrigger("DieTransition");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("DeathScene");
    }

    private void HUDPotions()
    {
        if (numPotions == 2)
        {
            Potions.transform.Find("Potion1").gameObject.SetActive(true);
            Potions.transform.Find("Potion2").gameObject.SetActive(true);
        }
        else if (numPotions == 1)
        {
            Potions.transform.Find("Potion1").gameObject.SetActive(true);
            Potions.transform.Find("Potion2").gameObject.SetActive(false);
        }
        else if (numPotions == 0)
        {
            Potions.transform.Find("Potion1").gameObject.SetActive(false);
            Potions.transform.Find("Potion2").gameObject.SetActive(false);
        }
    }

    private void FuryTextDisable()
    {
        furyBar.transform.Find("FuryText").gameObject.SetActive(false);
        furyBar.transform.Find("FuryText").gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void FuryTextUpdate()
    {   
        if (FuryCounter.hitCounter == 0)
        {
            FuryTextDisable();
        } else
        {
            furyBar.transform.Find("FuryText").gameObject.SetActive(true);
            furyBar.transform.Find("FuryText").gameObject.GetComponent<TextMeshProUGUI>().text = FuryCounter.hitCounter.ToString() + " GOLPES";
        }
    }
}
