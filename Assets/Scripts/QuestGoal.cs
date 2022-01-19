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
    public string itemType;

    public bool IsComplited()
    {   //sprawdzanie czy quest jest zrobiony
        return (currScore>= reqScore);
    }

    public void KillEnemy(int enemyId) 
    {   //wywo�anie funkcji np. przy zabiciu przeciwnika, dodaje currScore gdy enemyID zgadza si� z podanym w quescie
        if (goalType == GoalType.kill && id == enemyId)
            currScore++;
    }
    public void CollectItems(string type, int ammount)
    {   //to samo tylko dla zbierania item�w
        if (goalType == GoalType.collect && type==itemType)
            currScore+= ammount;
    }
    public void Talk(int npcId) 
    {   // funkcja do questow typu idz pogadaj z tym/tamtym na razie nieuzywana
        if (goalType == GoalType.talk && id == npcId)
            currScore=reqScore;
    }

}

public enum GoalType 
{   //typy quest�w
    kill,
    collect,
    talk,
}
