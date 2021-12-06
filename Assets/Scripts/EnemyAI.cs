using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    
    public GameObject npc;
    public NavMeshAgent agent;
    public float rangeToShoot = 5f;
    public float targetRange = 10f;
    private Rigidbody npcRb;
    private Transform waypointy;
    private GameObject[] enemys;
    private bool haveT = false;
    private AIShooting aishoot;
    private bool enemyInSightRange, enemyInAttackRange;
    public LayerMask whatIsTeam1;
    public LayerMask whatIsTeam2;
    public bool isItTeam1;


    void Start()
    {
       
        aishoot = npc.transform.GetChild(0).GetComponent<AIShooting>();
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform;
        agent.SetDestination(waypointy.GetChild(Random.Range(0, waypointy.childCount)).transform.position);
    }

    void Update()
    {
      
        if (isItTeam1)
        {
            FindTarget(true);
        }
        else { 
            FindTarget(false);
        }
    
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
            
            if ( distanceToEnemy <= targetRange)
            {
                    agent.SetDestination(currentEnemy.transform.position);
                    haveT = true;
                    if(agent.remainingDistance < rangeToShoot)
                    {

                    transform.position = this.transform.position;
                   
                    aishoot.Shoot();

                   
                    transform.LookAt(currentEnemy.transform);
                    }
                
            }   
        }

        if (!haveT || enemys.Length < 1 )
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