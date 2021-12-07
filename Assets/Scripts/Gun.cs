using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float range = 50f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    


    public Camera fpsCamera;
    //public ParticleSystem muzzleShot;
    //public GameObject impactEffect;

    // Update is called once per frame
    private float nextTime = 0f;
  //  public Animator animator;



    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTime)
        {
            //

            // animator.SetTrigger("Shoot1");
            nextTime = Time.time + 4f / fireRate;
            //muzzleShot.Play();
           
            Shoot();

        }
    }


    void Shoot()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            Debug.Log(hit.transform.name);
            if (target != null)
            {
                
                target.Damage(damage);
                
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impact, 2);

    }


}