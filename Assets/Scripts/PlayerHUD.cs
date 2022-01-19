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
    public GameObject ammoViec;
    public Text maxAmmoText;
    public Text currAmmoText;

    public GameObject activeQuestWindow;
    public Button giveUpButton;
    public Button previousQuest;
    public Button nextQuest;
    public Text titleText;
    public Text descryptionText;
    public Text expText;
    public Text goldText;
    public GameObject score;
    public GameObject rewards;
    public GameObject arrows;
    public Text currScore;
    public Text reqScore;

    [SerializeField] GameObject uiInventory;
    public bool openedInventory;

    public int visibleQuestIndex = 0;
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
    public void UpdateAmmo(int currAmmo, int maxAmmo)
    {   //Wyœwietlanie stanu amunicji
        currAmmoText.text = currAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }
    public void OpenInventory()
    {
        if (uiInventory.activeSelf)
        {
            uiInventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            openedInventory = false;
        }
        else
        {
            uiInventory.SetActive(true);
            uiInventory.gameObject.GetComponent<UI_Inventory>().RefreshInventory();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            openedInventory = true;
            
        }
    }

    public void OpenActiveQuest()
    {
        Cursor.lockState = CursorLockMode.None;
        activeQuestWindow.SetActive(true);
        if (player.quests.Count == 0)
        {
            titleText.text = "Brak zadań";
            descryptionText.text = "Nie masz aktualnie żadnych misji od wykonania";
            score.SetActive(false);
            rewards.SetActive(false);
            arrows.SetActive(false);
            
        }
        else 
        {

            for (int i = 0; i < player.quests[visibleQuestIndex].largeQuest.Count; i++)
            {
                Debug.Log(titleText.text = player.quests[visibleQuestIndex].largeQuest[i].description);
                if (player.quests[visibleQuestIndex].largeQuest[i].isActive)
                {
                    titleText.text = player.quests[visibleQuestIndex].largeQuest[i].title;
                    descryptionText.text = player.quests[visibleQuestIndex].largeQuest[i].description;
                    score.SetActive(true);
                    rewards.SetActive(true);
                    if (player.quests.Count == 1)
                        arrows.SetActive(false);
                    else
                        arrows.SetActive(true);

                    goldText.text = player.quests[visibleQuestIndex].largeQuest[i].gold.ToString();
                    expText.text = player.quests[visibleQuestIndex].largeQuest[i].exp.ToString();
                    reqScore.text = player.quests[visibleQuestIndex].largeQuest[i].goal.reqScore.ToString();
                    currScore.text = player.quests[visibleQuestIndex].largeQuest[i].goal.currScore.ToString();
                    break;
                }
            }
        }

    }
    public void CloseActiveQuest()
    {
        activeQuestWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GiveUPQuest()
    {
        player.quests.Remove(player.quests[visibleQuestIndex]);
        if (visibleQuestIndex != 0)
            visibleQuestIndex--;
        OpenActiveQuest();
    }
    public void NextQuest() 
    {
        if (visibleQuestIndex == player.quests.Count - 1)
            visibleQuestIndex = 0;
        else
            visibleQuestIndex++;
        OpenActiveQuest();
    }

    public void PreviousQuest()
    {
        if (visibleQuestIndex == 0)
            visibleQuestIndex = player.quests.Count - 1;
        else
            visibleQuestIndex--;
        OpenActiveQuest();
    }
}
