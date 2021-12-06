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

    void Start()
    {
        npcRb = npc.GetComponent<Rigidbody>();
        waypointy = GameObject.FindGameObjectWithTag("Waypointy").transform; //Przypisujemy do waypointy obiekty z danym tagiem 
        change_dest = UnityEngine.Random.Range(0, waypointy.childCount);  // Losujemy po ilu trasach npc ma zmieni� destynajc� 
        agent.SetDestination(waypointy.GetChild(0).transform.position); // Przypisujemy domy�lny pierwszy cel
    }

    // Update is called once per frame
    void Update()
    { 
        if(agent.remainingDistance == 0)
        {
            agent.SetDestination(waypointy.GetChild(Where()).transform.position); // Je�li npc doszed� do celu, wywo�aj funckj� where (co i  gdzie ma npc robi�)
            //Debug.Log(agent.destination);
        }   
    }

    public int Where()
    {
        if(change_dest == 0)
        {
            //Debug.Log("No zmieniam");
            change_dest = UnityEngine.Random.Range(0, waypointy.childCount); //Je�eli npc przeszed� wylowan� wcze�niej ilo�� tras do pokonania wyznacz nowy cel i pr�g tras
            return UnityEngine.Random.Range(0, waypointy.childCount);
        }
        change_dest--; // Je�eli npc jeszcze dalej nie przeszed� tylu tras to zmniejsz ile ju� przeszed�
        float[] array1 = new float[waypointy.childCount]; // Chcemy sprawdzi� jaki cel jest najbli�ej wzgl�dem osi x i z, y olewamy
        float[] array2 = new float[waypointy.childCount];
        for (int i = 0; i < waypointy.childCount; i++)
        {
            if (waypointy.GetChild(i).transform.position.x == transform.position.x && waypointy.GetChild(i).transform.position.z == transform.position.z)
            {
                array1[i] = 999999999999; //Wykluczamy aktualny punkt, wsadzi�em jak�� nierealn� liczb�, wiem u�omnie ale naj�atwiej chyba
                array2[i] = 999999999999;
            }
            else
            {
                array1[i] = Math.Abs(waypointy.GetChild(i).transform.position.x - transform.position.x); // Sprawdzamy odleg�osci x i z destynacji
                array2[i] = Math.Abs(waypointy.GetChild(i).transform.position.z - transform.position.z);
            }
        }


        float min_l1 = array1[0]; //Najmniejsze s� na razie na pocz�tku
        float min_l2 = array2[0];
        int indeks1 = 1; 
        int indeks2 = 1;
        float r1;
        float r2;

        for (int i = 0; i < waypointy.childCount; i++)
        {
            if(min_l1 > array1[i]) //Sprawdzamy kt�ry punkt jest  najbli�ej na osi x od nas
            {
                min_l1 = array1[i];
                indeks1 = i;
            }
            if (min_l2 > array2[i]) //Sprawdzamy kt�ry punkt jest  najbli�ej na osi z od nas
            {
                min_l2 = array2[i];
                indeks2 = i;
            }
        }

        if (indeks1 == indeks2) // Je�li ten punkt jest najbli�ej wzgl�dem osi x i z, staje si� celem
        {
            //Debug.Log("1");
            return indeks1;
        }
        else // Je�li ten punkt jest najbli�ej wzgl�dem osi x a inny z, lub na odwr�t, sprawdzamy kt�ry jest bli�ej wzgl�dem drugiej osi i wyznaczamy nowy cel
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
                return UnityEngine.Random.Range(0, waypointy.childCount); // Je�eli s� takie same to losujemy kt�ry ma byc celem 
            }
        }
    }
}
