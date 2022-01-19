using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public PlayerStats playerStats;
    public Bar hpBar;
    public float health;
    public float gainExp;
    public float damege;
    public int id;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        health = 50 + 10 * playerStats.level;
        gainExp = 50 + 10 * playerStats.level;
        damege = 10 + playerStats.level;
        hpBar.SetMaxValue(health);
        hpBar.SetCurrentValue(health);
    }
    public void Damage(float amount)
    {
        health -= amount;
        hpBar.SetCurrentValue(health);
       
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerStats.GetExp(gainExp);
        QuestProgress();
        Destroy(gameObject);
    }

    void QuestProgress() 
    {
        if (playerStats.quests.Count > 0)
        {
            for (int i = 0; i < playerStats.quests.Count; i++)
            {
                for (int j = 0; j < playerStats.quests[i].largeQuest.Count; j++)
                {
                    if (playerStats.quests[i].largeQuest[j].isActive)
                    {
                        playerStats.quests[i].largeQuest[j].goal.KillEnemy(id);
                        if (playerStats.quests[i].largeQuest[j].goal.IsComplited())
                        {
                            playerStats.GetExp(playerStats.quests[i].largeQuest[j].exp);
                            playerStats.GetGold(playerStats.quests[i].largeQuest[j].gold);
                            playerStats.quests[i].largeQuest[j].Compleated();

                            if (j == playerStats.quests[i].largeQuest.Count - 1)
                                playerStats.quests.Remove(playerStats.quests[i]);
                            else
                                playerStats.quests[i].largeQuest[j + 1].isActive = true;

                            if (i >= 0 && playerStats.quests.Count > 0)
                                i--;
                        }
                        break;
                    }
                }
            }
        }
    }
}


