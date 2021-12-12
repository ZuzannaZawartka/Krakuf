using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int currScore;
    public int reqScore;
    public int id;
    public bool IsComplited()
    {   //sprawdzanie czy quest jest zrobiony
        return (currScore>= reqScore);
    }

    public void KillEnemy(int enemyId) 
    {   //wywo�anie funkcji np. przy zabiciu przeciwnika, dodaje currScore gdy enemyID zgadza si� z podanym w quescie
        if (goalType == GoalType.kill && id==enemyId)
            currScore++;
    }
    public void CollectItems(int ItemId)
    {   //to samo tylko dla zbierania item�w
        if (goalType == GoalType.collect && id==ItemId)
            currScore++;
    }

}

public enum GoalType 
{   //typy quest�w
    kill,
    collect
}
