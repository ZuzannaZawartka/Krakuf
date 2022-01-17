using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{   //skrypt odpoweidzialny za zwracanie si� spritow/ obiekt�w w strone gracza
    new public Transform camera;
    // Update is called once per frame
    private void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
