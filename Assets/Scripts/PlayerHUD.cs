using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text playerClassText;

    public Text levelText;
    public Text maxExpText;
    public Text currExpText;

    public Text currHPText;
    public Text maxHPText;
    public Text currStaminaText;
    public Text maxStaminaText;
    public Text currManaText;
    public Text maxManaText;

    public Bar hpBar;
    public Bar staminaBar;
    public Bar manaBar;
    public Bar expBar;

    public void UpdatePlayerClass(string playerclass)
    {   //wyœwietlanie klasy gracza 
        playerClassText.text = playerclass;
    }

    public void UpdateExp(int level, float currExp, float maxExp)
    {   //Wyœwietlanie lv i expa gracza
        levelText.text = level.ToString();
        currExpText.text = currExp.ToString();
        maxExpText.text = maxExp.ToString();
        expBar.SetMaxValue(maxExp);
        expBar.SetCurrentValue(currExp);
    }

    public void UpdateHP(float currHP, float maxHP) 
    {   //Wyœwietlanie HP
        currHPText.text = currHP.ToString();
        maxHPText.text = maxHP.ToString();
        hpBar.SetMaxValue(maxHP);
        hpBar.SetCurrentValue(currHP);
    }
    public void UpdateStamina(float currStamina, float maxStamina)
    {   //Wyœwietlanie stanu staminy
        currStaminaText.text = currStamina.ToString();
        maxStaminaText.text = maxStamina.ToString();
        staminaBar.SetCurrentValue(currStamina);
        staminaBar.SetMaxValue(maxStamina);
    }
    public void UpdateMana(float currMana, float maxMana)
    {   //Wyœwietlanie stanu many
        currManaText.text = currMana.ToString();
        maxManaText.text = maxMana.ToString();
        manaBar.SetMaxValue(maxMana);
        manaBar.SetCurrentValue(currMana);
    }
}
