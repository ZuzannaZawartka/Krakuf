using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{

    public Camera cam;
    public GameObject npc;
    public NavMeshAgent agent;
    public float jumpForce = 1000.0f;
    private Rigidbody npcRb;
    private Transform waypointy;
    private int change_dest = 0;
    float[] array1;

    void Start()
    {
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform;
        change_dest = UnityEngine.Random.Range(0, waypointy.childCount);
        agent.SetDestination(waypointy.GetChild(0).transform.position);
    }

    // Update is called once per frame
    void Update()
    { 
        if(agent.remainingDistance == 0)
        {
            agent.SetDestination(waypointy.GetChild(Where()).transform.position);
            //Debug.Log(agent.destination);
        }   
    }

    public int Where()
    {
        if(change_dest == 0)
        {
            Debug.Log("No zmieniam");
            change_dest = UnityEngine.Random.Range(0, waypointy.childCount);
            return UnityEngine.Random.Range(0, waypointy.childCount);
        }
        change_dest--;
        float[] array1 = new float[waypointy.childCount];
        float[] array2 = new float[waypointy.childCount];
        for (int i = 0; i < waypointy.childCount; i++)
        {
            if (waypointy.GetChild(i).transform.position.x == transform.position.x && waypointy.GetChild(i).transform.position.z == transform.position.z)
            {
                array1[i] = 999999999999;
                array2[i] = 999999999999;
            }
            else
            {
                array1[i] = Math.Abs(waypointy.GetChild(i).transform.position.x - transform.position.x);
                array2[i] = Math.Abs(waypointy.GetChild(i).transform.position.z - transform.position.z);
            }
        }


        float min_l1 = array1[0];
        float min_l2 = array2[0];
        int indeks1 = 1;
        int indeks2 = 1;
        float r1;
        float r2;

        for (int i = 0; i < waypointy.childCount; i++)
        {
            if(min_l1 > array1[i])
            {
                min_l1 = array1[i];
                indeks1 = i;
            }
            if (min_l2 > array2[i])
            {
                min_l2 = array2[i];
                indeks2 = i;
            }
        }

        if (indeks1 == indeks2)
        {
            //Debug.Log("1");
            return indeks1;
        }
        else
        {
            r1 = Math.Abs(min_l1 - array1[indeks2]);
            r2 = Math.Abs(min_l2 - array2[indeks1]);
            if(r1 < r2)
            {
                //Debug.Log("2");
                return indeks2;
            }
            else if(r2 < r1)
            {
                //Debug.Log("3");
                return indeks1;
            }
            else
            {
                //Debug.Log("4");
                return UnityEngine.Random.Range(0, waypointy.childCount);
            }
        }
    }
}
