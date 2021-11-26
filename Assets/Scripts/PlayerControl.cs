using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpPower = 30.0f;
    public CharacterController player;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float graundDistance = 0.2f;
    public float gravity = -10;
    private bool isOnGround;
    private bool sprint = false;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Poruszanie sie WASD
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        player.Move(move * speed * Time.deltaTime);

        //Grawitacja
        isOnGround = Physics.CheckSphere(groundCheck.position, graundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime; 
        player.Move(velocity * Time.deltaTime);

        if (isOnGround)
            velocity.y = -1.0f;

        //Skok
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            isOnGround = false;
            transform.Translate(new Vector3(0, graundDistance+1f, 0));
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            speed *= 2;
            sprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && sprint)
        {
            speed /= 2;
            sprint = false;
        }

    }
}
