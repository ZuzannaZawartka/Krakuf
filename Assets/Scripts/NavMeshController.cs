using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{

    public Camera cam;
    public GameObject npc;
    public NavMeshAgent agent;
    public float jumpForce = 1000.0f;
    private Rigidbody npcRb;


    void Start()
    {
        npcRb = npc.GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obiekty"))
        {
            npcRb.AddForce(Vector3.up * jumpForce);
        }
    }
}
