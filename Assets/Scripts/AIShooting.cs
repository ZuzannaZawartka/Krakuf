using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float rangeToShoot = 5f;
    public float shootForce = 30f;
    public float fireRate = 1f;
    public bool toThrow = false;
    public GameObject enemy;
    public GameObject grenadePrefabs;
    
    //public ParticleSystem muzzleShot;
    //public GameObject impactEffect;

    // Update is called once per frame
    private float nextTime = 0f;
    //public Animator animator;


    public void Shoot()
    {

       
        RaycastHit hit;
        if (!toThrow)
        {
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, rangeToShoot))
            {

                if (Time.time >= nextTime)
                {

                    //muzzleShot.Play();
                    nextTime = Time.time + 4f / fireRate;
                    Debug.Log("STRZAL");
                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        //target.Damage(damage);
                        target.Damage(30);
                    }
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * shootForce);
                    }
                }
            }
            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 2);
        }
        else {

            if (Time.time >= nextTime)
            {
                nextTime = Time.time + 4f / fireRate;
                Debug.Log("STRZAL");
      
                GameObject grenade = Instantiate(grenadePrefabs, transform.position, transform.rotation);
                
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * shootForce, ForceMode.VelocityChange);
            }
        }



    }


}