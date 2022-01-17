using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{   //skrypt zadawany na npc kt�ry ma dawa� questa graczowi
    public Quest quest;
    public PlayerStats player;

    public GameObject questWindow;
    public Button acceptButton;
    public Button rejectButton;
    public Text titleText;
    public Text descryptionText;
    public Text expText;
    public Text goldText;
    public bool isWindowActive = false;

    private void Start()
    {
        acceptButton = acceptButton.GetComponent<Button>();
        rejectButton = rejectButton.GetComponent<Button>();
    }

    public void OpenQuestWindow()
    {   //Funkcja otwieraj�ca UI odpowiadaj�ce za nadanie questu i przupisanie mu odpowiednich warto��i
        Cursor.lockState = CursorLockMode.None;
        titleText.text = quest.title;
        descryptionText.text = quest.description;
        expText.text = quest.exp.ToString();
        goldText.text = quest.gold.ToString();
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(AcceptQuest);
        rejectButton.onClick.RemoveAllListeners();
        rejectButton.onClick.AddListener(CloserQuestWindow);
        questWindow.SetActive(true);
        isWindowActive = true;
    }
    public void CloserQuestWindow()
    {   //Zamykanie okna quest�w
        Cursor.lockState = CursorLockMode.Locked;
        questWindow.SetActive(false);
        isWindowActive = false;
    }
    public void AcceptQuest() 
    {   //Akceptacja questa, przypisanie go do gracza i zamkni�cie okna
        quest.isActive = true;
        player.quest = quest;
        CloserQuestWindow(); 
    }
}
