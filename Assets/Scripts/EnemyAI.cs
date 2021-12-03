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
   private bool haveT = false;
    private AIShooting aishoot;


    public bool enemyInSightRange, enemyInAttackRange;
    public LayerMask whatIsTeam1 , whatIsTeam2 ;
    private Vector3 walkPoint;
    private bool walkPointSet = false;
    private Transform look;
    public bool isItTeam1;
    public bool isItTeam2;
    public bool isItPlayer;

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
       

        if (isItTeam1)
        {
            FindTarget(true);
        }

        if (isItTeam2)
        {
            FindTarget(false);
        }
        
        // FIXME strzela przy punktach waypoint
       

       
    }
   

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, rangeToShoot))
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
    }
    void FindTarget(bool isitT1)
    {
        if (isitT1)
        {
            enemys = GameObject.FindGameObjectsWithTag("Team2");
        }
        else
        {
            enemys = GameObject.FindGameObjectsWithTag("Team1");
        }
        haveT = false;
     
        foreach (GameObject currentEnemy in enemys)
        {
            float distanceToEnemy = (currentEnemy.transform.position - npc.transform.position).magnitude;
            
            if ( distanceToEnemy <= targetRange && distanceToEnemy < agent.remainingDistance)
            {
                    agent.SetDestination( new Vector3(currentEnemy.transform.position.x -2, currentEnemy.transform.position.y, currentEnemy.transform.position.z -2));
                    haveT = true;
                    if(agent.remainingDistance < rangeToShoot)
                    {
                    transform.position = this.transform.position;
                        aishoot.Shoot();
                    transform.LookAt(currentEnemy.transform);
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