using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    //zmienne ktore ma kazdy quest, w skrypcie questgiver wypisuje je w UI
    public bool isActive;
    public int questId;
    public string title;
    public string description;
    public int exp;
    public int gold;
    //zmienna goal aby umo¿liwiæ tworzenie kilku rodzajów questów
    public QuestGoal goal;

    public void Compleated() 
    {   //gdy ukonczymy quest
        goal.currScore = 0;
        isActive = false;
        Debug.Log("Zadanie '" + title + "' ukonczone");
    }

}
