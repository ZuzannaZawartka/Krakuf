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

        if (playerStats.quests.Count > 0)
        {
            for (int i= 0; i< playerStats.quests.Count; i++ ) 
            {
                playerStats.quests[i].goal.KillEnemy(id);
                if (playerStats.quests[i].goal.IsComplited())
                {
                    playerStats.GetExp(playerStats.quests[i].exp);
                    playerStats.GetGold(playerStats.quests[i].gold);
                    playerStats.quests[i].Compleated();
                    playerStats.quests.Remove(playerStats.quests[i]);
                    if (i >= 0 && playerStats.quests.Count>0)  // poniewaz po usuniêciu czegos z listy nastepuje przesuniêcie aby zape³niæ usuniêty index
                        i--;

                }
            }
        }

        Destroy(gameObject);
    }
}


