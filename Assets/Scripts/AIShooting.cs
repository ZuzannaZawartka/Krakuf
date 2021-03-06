using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    // Start is called before the first frame update
   
   
    public float shootForce = 30f;
    public float fireRate = 1f;
    public bool toThrow = false;
    public GameObject enemy;
    public GameObject grenadePrefabs;
    private EnemyAI enemyAI;
  
    private float rangeToShoot;
    public float damage;
    //public ParticleSystem muzzleShot;
    //public GameObject impactEffect;

    // Update is called once per frame

    //public Animator animator;

    private void Start()
    {
        enemyAI = enemy.GetComponent<EnemyAI>();
        rangeToShoot = enemyAI.rangeToShoot;
       
    }

    public void Shoot()
    {

      
        RaycastHit hit;
        if (!toThrow)
        {
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, rangeToShoot))
            {



                //muzzleShot.Play();
                
               
                    PlayerStats target = hit.transform.GetComponent<PlayerStats>();
                    if (target != null)
                    {
                        //target.Damage(damage);
                        target.TakeDamage(damage);
                    }
                    if (hit.rigidbody != null)

                    {
              
                        hit.rigidbody.AddForce(-hit.normal * shootForce);
                    }
                }
            
            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 2);
        }
        else {

           
                
            
      
                GameObject grenade = Instantiate(grenadePrefabs, transform.position, Quaternion.identity);
                
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
               // rb.AddForce(transform.up * (shootForce/8f), ForceMode.Impulse);

        }
        



    }

   
}