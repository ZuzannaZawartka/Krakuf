using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerStats player;

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

    public GameObject activeQuestWindow;
    public Button giveUpButton;
    public Text titleText;
    public Text descryptionText;
    public Text expText;
    public Text goldText;
    public GameObject score;
    public GameObject rewards;
    public Text currScore;
    public Text reqScore;

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

    
    public void OpenActiveQuest()
    {
        Cursor.lockState = CursorLockMode.None;
        activeQuestWindow.SetActive(true);
        if (!player.quest.isActive)
        {
            titleText.text = "Brak zadnia";
            descryptionText.text = "Nie masz aktualnie ¿adnej misji od wykonania";
            score.SetActive(false);
            rewards.SetActive(false);


        }
        else if (player.quest.isActive) 
        {
            titleText.text = player.quest.title;
            descryptionText.text = player.quest.description;
            score.SetActive(true);
            rewards.SetActive(true);
            goldText.text = player.quest.gold.ToString();
            expText.text = player.quest.exp.ToString();
            reqScore.text = player.quest.goal.reqScore.ToString();
            currScore.text = player.quest.goal.currScore.ToString();

        }

    }
    public void CloseActiveQuest()
    {
        activeQuestWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GiveUPQuest()
    {
        player.quest.isActive = false;
        OpenActiveQuest();
    }
}
