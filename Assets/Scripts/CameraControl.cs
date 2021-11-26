using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSpeed = 200.0f;
    float xRotaion = 0.0f;
    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        xRotaion -= mouseY;
        xRotaion = Mathf.Clamp(xRotaion, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(xRotaion, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);


    }
}
