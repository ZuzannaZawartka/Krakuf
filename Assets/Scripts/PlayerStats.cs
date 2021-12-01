using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHP;
    public float currHP;
    public bool isDead;

    public float maxStamina;
    public float currStamina;

    public float maxMana;
    public float currMana;

    public PlayerControl playerControl;
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();

        maxHP = 100;
        currHP = maxHP;
        isDead = false;

        maxStamina = 100;
        currStamina = maxStamina;

        maxMana = 100;
        currMana = maxMana;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            currStamina -= 20;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Heal(10);
        }

        if (playerControl.sprint) 
        {
            currStamina -= 10   *Time.deltaTime;
            CheckStamina();
        }
        if (!playerControl.sprint)
        {
            currStamina += 7 * Time.deltaTime;
            CheckStamina();
        }

        if (playerControl.sprint == true && currStamina < 3) 
        {
            playerControl.speed /= 2;
            playerControl.sprint = false;
        }

        if (currMana < maxMana) 
        {
            currMana += 5 * Time.deltaTime;
            CheckStamina();
        }
    }
    public void CheckHP()
    {
        if (currHP <= 0)
        {
            currHP = 0;
            Dead();
        }
        if (currHP > maxHP)
        {
            currHP = maxHP;
        }
    }
    public void TakeDamage(float damage)
    {
        currHP -= damage;
        CheckHP();
    }
    public void Heal(float heal)
    {
        currHP += heal;
        CheckHP();
    }
    public void Dead()
    {
        Debug.Log("Game Over!");
        isDead = true;
    }

    public void CheckStamina()
    {
        if (currStamina > maxStamina)
            currStamina = maxStamina;
        if (currStamina <= 0)
        {
            currStamina = 0;
        }

    }
    public void CheckMana()
    {
        if (currStamina > maxStamina)
            currStamina = maxStamina;
        if (currStamina <= 0)
        {
            currStamina = 0;
        }

    }
    public void UseMagic(float manaCost) 
    {
        currMana -= manaCost;
        CheckMana();
    }
}
