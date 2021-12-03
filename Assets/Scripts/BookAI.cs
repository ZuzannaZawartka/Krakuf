using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public GameObject exEffect;
    public float force = 700f;
    public float vect = 5f;
    float countdown;
    bool hasExploded = false;

    void Start()
    {

    }

    
    public void OnCollisionEnter(Collision collision)
    {
        Target obj = collision.gameObject.GetComponent<Target>();
        if (collision.gameObject.GetComponent<Target>() != null)
        {
            obj.Damage(5);
            Debug.Log(obj);
            Destroy(gameObject);
        }
    }
}
