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

    public PlayerControl playerControl;
    public PlayerHUD hud;
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        hud = GetComponent<PlayerHUD>();

        playerClass = "Mage";

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
        else 
        {
            dex = 15;
            dexPerLv = 3;
        }

        level = 1;
        maxExp = 200;
        currExp = 0;

        maxHP = 100;
        currHP = maxHP;
        isDead = false;

        maxStamina = 100;
        currStamina = maxStamina;

        maxMana = 100;
        currMana = maxMana;


        hud.UpdatePlayerClass(playerClass);
        CheckExp();
        CheckHP();
        CheckStamina();
        CheckMana();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Heal(10);
        }

        if (Input.GetKeyDown(KeyCode.G)) 
        {
            GetExp(60);
        }

        if (playerControl.sprint) 
        {
            currStamina -= 10   *Time.deltaTime;
            CheckStamina();
        }
        if (!playerControl.sprint)
        {
            currStamina += 7 * Time.deltaTime;
            CheckStamina();
        }

        if (playerControl.sprint == true && currStamina < 3) 
        {
            playerControl.speed /= 2;
            playerControl.sprint = false;
        }

        if (currMana < maxMana) 
        {
            currMana += 5 * Time.deltaTime;
            CheckMana();
        }

    }
    public void CheckHP()
    {
        if (currHP <= 0)
        {
            currHP = 0;
            Dead();
        }
        if (currHP > maxHP)
        {
            currHP = maxHP;
        }
        hud.UpdateHP(currHP, maxHP);
    }
    public void TakeDamage(float damage)
    {
        currHP -= damage;
        CheckHP();
    }
    public void Heal(float heal)
    {
        currHP += heal;
        CheckHP();
    }
    public void Dead()
    {
        Debug.Log("Game Over!");
        isDead = true;
    }

    public void CheckStamina()
    {
        if (currStamina > maxStamina)
            currStamina = maxStamina;
        if (currStamina <= 0)
        {
            currStamina = 0;
        }
        hud.UpdateStamina(currStamina, maxStamina);
    }
    public void CheckMana()
    {
        if (currMana > maxMana)
            currMana = maxMana;
        if (currMana <= 0)
        {
            currMana = 0;
        }
        hud.UpdateMana(currMana, maxMana);
    }
    public void UseMagic(float manaCost) 
    {
        currMana -= manaCost;
        CheckMana();
    }
    public void GetExp(float exp) 
    {
        currExp += exp;
        CheckExp();
    }
    public void CheckExp()
    {
        hud.UpdateExp(level, currExp, maxExp);
        if (currExp >= maxExp)
        {
            level += 1;
            currExp -= maxExp;
            maxExp = 300 * level;
            str += strPerLv;
            intel += intelPerLv;
            dex += dexPerLv;
            maxHP += 10;
            currHP = maxHP;
            CheckHP();
            CheckExp();
        }
    }
}
