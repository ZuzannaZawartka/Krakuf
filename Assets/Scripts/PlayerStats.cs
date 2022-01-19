using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerClass;
    public float maxHP;
    public float currHP;
    public bool isDead;
    public int level;
    public float maxExp;
    public float currExp;
    public float maxStamina;
    public float currStamina;
    public float maxMana;
    public float currMana;
    public int str;
    public int intel;
    public int dex;
    public int strPerLv;
    public int intelPerLv;
    public int dexPerLv;
    public int gold;

    public GameObject deadScrean;

    public List<serializableClass> quests = new List<serializableClass>();
    public PlayerControl playerControl;
    public PlayerHUD hud;
    private void Start()
    {   //ustawianie klasy i odpowiednich do niej zmiennych
        SetClass();
        SetVariables();
        CheckExp();
        CheckHP();
        CheckStamina();
        CheckMana();
    }
    private void Update()
    {
        // Inputy tylko po to ¿eby sprawdzaæ dzia³anie :)

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            hud.OpenActiveQuest();
        }

        if (Input.GetKeyDown(KeyCode.Y))
            SpendGold(10);
        
        if (Input.GetKeyDown(KeyCode.G)) 
            GetExp(60);
        
        //Stamina  i Mana regen 
        CheckSprint();
        ManaRegen();
    }
    public void CheckHP()
    {   //sprawdzanie stanu HP gracza
        if (currHP <= 0)
        {
            currHP = 0;
            Dead();
        }
        if (currHP > maxHP)
            currHP = maxHP;
        
        hud.UpdateHP(currHP, maxHP);
    }
    public void TakeDamage(float damage)
    {   //Wywolanie tej funkcji zada graczowi obrazenia 
        currHP -= damage;
        CheckHP();
    }
    public void Heal(float heal)
    {   //Wywo³anie tej funkcji uleczy gracza
        currHP += heal;
        CheckHP();
    }
    public void Dead()
    {   //Funkcja ktora wykonuje siê gdy gracz umrze
        Debug.Log("Game Over!");
        Cursor.lockState = CursorLockMode.None;
        deadScrean.SetActive(true);
        transform.position = new Vector3(0, 3, 0);
        isDead = true;
    }

    public void Restart() 
    {
        isDead = false;
        Cursor.lockState = CursorLockMode.Locked;
        deadScrean.SetActive(false);
        SetVariables();
        CheckExp();
        CheckHP();
        CheckStamina();
        CheckMana();

    }

    public void CheckStamina()
    {   //Sprawdzanei stanu stamini gracza
        if (currStamina > maxStamina)
            currStamina = maxStamina;
        if (currStamina <= 0)
            currStamina = 0;
        
        hud.UpdateStamina(currStamina, maxStamina);
    }
    public void CheckSprint()
    {   //Zu¿ycie staminy na sprint i regeneracja gdy gracz nie sprinttuje
        if (playerControl.sprint && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
        {
            currStamina -= 10 * Time.deltaTime;
            CheckStamina();
        }
        if (!playerControl.sprint || (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0))
        {
            currStamina += 7 * Time.deltaTime;
            CheckStamina();
        }
        if (playerControl.sprint == true && currStamina < 5)
        {
            playerControl.speed /= 2;
            playerControl.sprint = false;
        }
    }
    public void CheckMana()
    {   //Sprawdzanie stanu many gracza
        if (currMana > maxMana)
            currMana = maxMana;
        if (currMana <= 0)
            currMana = 0;
        
        hud.UpdateMana(currMana, maxMana);
    }
    public void UseMagic(float manaCost) 
    {   // Zu¿ycie many gdy gracz u¿ywa magi
        currMana -= manaCost;
        CheckMana();
    }
    public void ManaRegen() 
    {   //Regeneracja many 
        if (currMana < maxMana)
        {
            currMana += 5 * Time.deltaTime;
            CheckMana();
        }
    }
    public void GetExp(float exp) 
    {   // Wywo³anie dodaje graczowi Expa
        currExp += exp;
        CheckExp();
    }
    public void CheckExp()
    {   //Sprawdzanie poziomu Expa, jeœli przekracza maxExp => level up
        hud.UpdateExp(level, currExp, maxExp);
        if (currExp >= maxExp)
        {
            level += 1;
            currExp -= maxExp;
            maxExp += 150;
            str += strPerLv;
            intel += intelPerLv;
            dex += dexPerLv;
            maxHP += 10;
            currHP = maxHP;
            CheckHP();
            CheckExp();
        }
    }
    public void GetGold(int earnGold)
    {   // Funkcja gdy zarobimy golda 
        gold += earnGold;
    }
    public void SpendGold(int lostGold)
    {   // Funkcja gdy tracimy golda/kupujemy itemy
        if (gold >= lostGold)
            gold -= lostGold;
        else
            Debug.Log("Nie staæ ciê :P");
    }
    public void SetClass()
    {   //Ustawiane klasy na podstawie wygoru gracza ze sceny ClassSelector
        playerControl = GetComponent<PlayerControl>();
        hud = GetComponent<PlayerHUD>();
        if (PlayerPrefs.GetInt("playerClass") == 0)
            playerClass = "Archer";
        else if (PlayerPrefs.GetInt("playerClass") == 1)
            playerClass = "Fighter";
        else
            playerClass = "Mage";

        hud.UpdatePlayerClass(playerClass);
    }
    public void SetVariables() 
    {   //Przyznanie wartoœci pocz¹tkowych, w zale¿noœci od wybranej klasy
        str = 10;
        intel = 10;
        dex = 10;
        strPerLv = 1;
        intelPerLv = 1;
        dexPerLv = 1;

        if (playerClass == "Mage")
        {
            intel = 15;
            intelPerLv = 3;
        }
        else if (playerClass == "Fighter")
        {
            str = 15;
            strPerLv = 3;
        }
        else if(playerClass == "Archer")
        {
            dex = 15;
            dexPerLv = 3;
        }

        level = 1;
        maxExp = 300;
        currExp = 0;

        maxHP = 100;
        currHP = maxHP;
        isDead = false;

        maxStamina = 100;
        currStamina = maxStamina;

        maxMana = 100;
        currMana = maxMana;

        gold = 10;
    }
}

[System.Serializable]
public class serializableClass
{
    public List<Quest> largeQuest;
}
