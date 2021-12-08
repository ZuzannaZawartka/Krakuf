using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefabs;
    public float fireRate = 15;
    private float nextTime = 0f;
    public PlayerStats playerStats;
    public GameObject player;
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTime && playerStats.currMana>=40)
        {
            nextTime = Time.time + 4f / fireRate;
            ThrowGrenade();
            playerStats.UseMagic(40f);
        }
    }
    // Update is called once per frame
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefabs, transform.position, transform.rotation);
        // GameObject gun = Instantiate(gunPrefab, new Vector3(fpsCamera.transform.position.x, fpsCamera.transform.position.y, fpsCamera.transform.position.z), fpsCamera.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * throwForce, ForceMode.VelocityChange);


    }
}
