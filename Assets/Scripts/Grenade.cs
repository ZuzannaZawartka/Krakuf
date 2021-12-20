using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public GameObject exEffect;
    public float force = 700f;
    public float vect = 5f;
    
    private float damage;
    float countdown;
    bool hasExploded = false;

    [SerializeField] private PlayerStats pS;

    void Start()
    {
        damage = GameObject.Find("bookInHand").GetComponent<GrenadeThrow>().damage;
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        Debug.Log("BOOM!");
        Instantiate(exEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, vect);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, vect);
            }
            Target tar = nearbyObject.GetComponent<Target>();
            if (tar != null)
            {
               // Destroy(tar.gameObject);
                tar.Damage(damage);
            }
        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, vect);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, vect);
            }


        }
        Destroy(gameObject);

    }
    
}
