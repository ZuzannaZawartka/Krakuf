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

    public int maxAmmo = 10;
    private int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public bool ammo = false;
    public Camera fpsCamera;
    //public ParticleSystem muzzleShot;
    //public GameObject impactEffect;

    // Update is called once per frame
    private float nextTime = 0f;
    //  public Animator animator;

    private void Start()
    {
        if(currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
        
    }

    void Update()
    {   

        if(Input.GetKeyDown(KeyCode.R) && !isReloading && ammo == true)
        {
            Debug.Log("dziala");
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTime && (currentAmmo > 0 || ammo == false))
        {
            //

            // animator.SetTrigger("Shoot1");
            nextTime = Time.time + 4f / fireRate;
            //muzzleShot.Play();
           
            Shoot();

        }
        if(currentAmmo ==0 && ammo == true)
        {
            Debug.Log("BRAK AMMO");
        }
    }


    void Shoot()
    {
        RaycastHit hit;
        --currentAmmo;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, range)) 
        {
            Debug.DrawRay(transform.position,transform.forward * 1000, Color.cyan);
            Target target = hit.transform.GetComponent<Target>();
          
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
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("RELOADING ..");
        
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }


}