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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerClass(string playerclass)
    {
        playerClassText.text = playerclass;
    }

    public void UpdateExp(int level, float currExp, float maxExp)
    {
        levelText.text = level.ToString();
        currExpText.text = currExp.ToString();
        maxExpText.text = maxExp.ToString();
    }

    public void UpdateHP(float currHP, float maxHP) 
    {
        currHPText.text = currHP.ToString();
        maxHPText.text = maxHP.ToString();
    }
    public void UpdateStamina(float currStamina, float maxStamina)
    {
        currStaminaText.text = currStamina.ToString();
        maxStaminaText.text = maxStamina.ToString();
    }
    public void UpdateMana(float currMana, float maxMana)
    {
        currManaText.text = currMana.ToString();
        maxManaText.text = maxMana.ToString();
    }
}
