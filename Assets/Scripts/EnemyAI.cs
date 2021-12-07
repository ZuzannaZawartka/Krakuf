using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    
    public GameObject npc;
    public Transform player;
    public NavMeshAgent agent;
    public float rangeToShoot = 5f;
    private float targetRange = 20f;
    private Rigidbody npcRb;
    private Transform waypointy;
    private AIShooting aishoot;

    public LayerMask whatIsPlayer;
    public bool playerInSightRange, playerInAttackRange;
    private Vector3 walkpoint;
    private bool walkPointSet , alreadyAttacked;
    public float timeBetweenAttacks;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Start()
    {
       
        aishoot = npc.transform.GetChild(0).GetComponent<AIShooting>();
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform;
        
    }

  
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, targetRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangeToShoot, whatIsPlayer);
       
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) Attack();
       
    
    }
   


    void ChasePlayer()
    {

        agent.SetDestination(player.position);
       
       
      
    }
    void Patrolling()
    {
        if (!walkPointSet) SearchWalk();
        if (walkPointSet) agent.SetDestination(walkpoint);

        Vector3 distanceToEnemy = transform.position - walkpoint;
        if (distanceToEnemy.magnitude < 1f)
        {
            walkPointSet = false;
            
        }
    }
    void SearchWalk()
    {
        walkpoint = waypointy.GetChild(Random.Range(0, waypointy.childCount)).transform.position;
        walkPointSet = true;
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player.position);
        if (!alreadyAttacked)
        {
            aishoot.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}