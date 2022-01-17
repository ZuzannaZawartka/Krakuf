using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private float damage = 10f;
    [SerializeField] private float range = 50f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private float fireRate = 15f;

    [SerializeField] private int maxAmmo = 10;
    private int currentAmmo = -1;
    [SerializeField] private float reloadTime = 1f;
    private bool isReloading = false;
    [SerializeField] private bool ammo = false;
    [SerializeField] private Camera fpsCamera;
    //public ParticleSystem muzzleShot;
    //public GameObject impactEffect;

    // Update is called once per frame
    private float nextTime = 0f;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats pS;
    [SerializeField] private PlayerHUD hud;

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
            Shoot();
        }
        if (range > 10)
        {
            hud.ammoViec.SetActive(true);
            hud.UpdateAmmo(currentAmmo, maxAmmo);
        }
        else
            hud.ammoViec.SetActive(false);

    }


    void Shoot()
    {
        if (currentAmmo <= 0 && ammo == true)
        {
            Debug.Log("BRAK AMMO");
            return;
        }

        animator.SetTrigger("Shoot1");
        nextTime = Time.time + 4f / fireRate;
        //muzzleShot.Play();
        RaycastHit hit;
        --currentAmmo;
        hud.UpdateAmmo(currentAmmo, maxAmmo);
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, range)) 
        {
            Debug.DrawRay(transform.position,transform.forward * 1000, Color.cyan);
            Target target = hit.transform.GetComponent<Target>();

            //zdaanie obra¿eñ
            if (target != null)
            {
                //zadanie obrazen zaleznie od statystki, jesli range mniejszy niz 10 wtedy jest to maczeta
                if(range > 10)
                {
                    //pistolet
                    target.Damage(2 * pS.dex + 0.2f * pS.str + 0.2f * pS.intel);
                }
                else
                {
                    //maczeta
                    target.Damage(2.5f * pS.str + 0.2f * pS.dex + 0.2f * pS.intel);
                }
                
            }
            if (hit.rigidbody != null)
            {
                //odrzut obiektu
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
        hud.UpdateAmmo(currentAmmo, maxAmmo);
        isReloading = false;
    }
}