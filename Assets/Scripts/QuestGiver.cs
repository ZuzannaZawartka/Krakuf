using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{   //skrypt zadawany na npc który ma dawaæ questa graczowi
    public serializableClass quest;
    public PlayerStats player;

    public GameObject questWindow;
    public Button acceptButton;
    public Button rejectButton;
    public Text titleText;
    public Text descryptionText;
    public Text expText;
    public Text goldText;

    private void Start()
    {

    }

    public void OpenQuestWindow()
    {   //Funkcja otwieraj¹ca UI odpowiadaj¹ce za nadanie questu i przupisanie mu odpowiednich wartoœæi
        Cursor.lockState = CursorLockMode.None;
        titleText.text = quest.largeQuest[0].title;
        descryptionText.text = quest.largeQuest[0].description;
        expText.text = quest.largeQuest[0].exp.ToString();
        goldText.text = quest.largeQuest[0].gold.ToString();
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(AcceptQuest);
        rejectButton.onClick.RemoveAllListeners();
        rejectButton.onClick.AddListener(CloserQuestWindow);
        questWindow.SetActive(true);
        Debug.Log(player.quests.Count);
    }
    public void CloserQuestWindow()
    {   //Zamykanie okna questów
        Cursor.lockState = CursorLockMode.Locked;
        questWindow.SetActive(false);
    }
    public void AcceptQuest() 
    {   
        bool cantake = true;

        if (player.quests.Count > 0)
            for (int i = 0; i < player.quests.Count; i++)
            {
                if(player.quests[i].largeQuest[0].title == quest.largeQuest[0].title)
                    cantake = false;
            }
        else
            cantake = true;

        if (cantake)
        {
            quest.largeQuest[0].isActive = true;
            player.quests.Add(quest);
            CloserQuestWindow();
        }
        else 
        {
            Debug.Log("masz juz tego questa");
            CloserQuestWindow();
        }

    }
}

