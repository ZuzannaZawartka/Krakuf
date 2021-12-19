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
        if (playerStats.quest.isActive)
        {
            playerStats.quest.goal.KillEnemy(id);
            if (playerStats.quest.goal.IsComplited())
            {
                playerStats.GetExp(playerStats.quest.exp);
                playerStats.GetGold(playerStats.quest.gold);
                playerStats.quest.Compleated();
            }
        }
        Destroy(gameObject);
    }
}


