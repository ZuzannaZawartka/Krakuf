using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public float damage;
    public GameObject exEffect;
    public float force = 700f;
    public float vect = 5f;
    
    

    private void Start()
    {
        //damage = enemy.gameObject.GetComponent<AIShooting>().damage;
    }
    public void OnCollisionEnter(Collision collision)
    {
        PlayerStats obj = collision.gameObject.GetComponent<PlayerStats>();
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            obj.TakeDamage(damage);
            
           // Instantiate(exEffect, transform.position, transform.rotation);
            Delete();
        }
        else
        {
            Invoke("Delete", 3.0f);
        }
        
    }
    void Delete()
    {
        Destroy(gameObject);
    }
}
