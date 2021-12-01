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

    void Start()
    {
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform;
        agent.SetDestination(waypointy.GetChild(0).transform.position);
    }

    // Update is called once per frame
    void Update()
    { 
        if(agent.remainingDistance < 1)
        {
            agent.SetDestination(waypointy.GetChild(Random.Range(0, waypointy.childCount)).transform.position);
        }   
    }
}
