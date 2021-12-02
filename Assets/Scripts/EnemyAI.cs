using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    public Camera cam;
    public GameObject npc;
    public GameObject player;
    public NavMeshAgent agent;
    public float rangeToShoot = 5f;
    public float targetRange = 10f;
    private Rigidbody npcRb;
    private Transform waypointy;
    private GameObject[] enemys;
    
    private AIShooting aishoot;
   
    

    void Start()
    {
        aishoot = npc.transform.GetChild(0).GetComponent<AIShooting>();
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform;
        
        
        agent.SetDestination(waypointy.GetChild(Random.Range(0, waypointy.childCount)).transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      
       FindTarget();
        // FIXME strzela przy punktach waypoint
        if (agent.remainingDistance < rangeToShoot)
        {
            aishoot.Shoot();
        }



    }


 /*   void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Damage(100);
            }
            if (hit.rigidbody != null)
            {
            }
        }
    }*/
    void FindTarget()
    {
        bool haveT = false;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
 
     

        foreach (GameObject currentEnemy in enemys)
        {
            float distanceToEnemy = (currentEnemy.transform.position - npc.transform.position).sqrMagnitude;
            
            if ( distanceToEnemy <= targetRange && distanceToEnemy < agent.remainingDistance)
            {

                if (npc.name != currentEnemy.name)
                {
                    agent.SetDestination(currentEnemy.transform.position);
                    haveT = true;
                   
                }
               
            }
           
           
        }

        if (!haveT || enemys.Length < 1)
        {
            movetoWaypoint();
        }


    }
    void movetoWaypoint()
    {
        if(agent.remainingDistance < 1)
        {
            agent.SetDestination(waypointy.GetChild(Random.Range(0, waypointy.childCount)).transform.position);
        }
    }
}