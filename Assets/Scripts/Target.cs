using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public PlayerStats playerStats;
    public float health;
    public float gainExp;
    public float damege;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        health = 50 + 10 * playerStats.level;
        gainExp = 50 + 10 * playerStats.level;
        damege = 10 + playerStats.level;
    }
    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerStats.GetExp(gainExp);
        Destroy(gameObject);
    }
}


