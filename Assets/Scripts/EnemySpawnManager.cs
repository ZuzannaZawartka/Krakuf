using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public GameObject[] enemys;
    public float enemysToSpawn;
    public float  timeToNextSpawn;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Spawn();
    }
    void Spawn()
    {
        
        if(timer >= timeToNextSpawn && gameObject.transform.childCount < enemysToSpawn)
        {
            GameObject inst = Instantiate(enemys[Random.Range(0, enemys.Length)],transform.position,transform.rotation,gameObject.transform);
            timer = 0;
        }
    }
}
