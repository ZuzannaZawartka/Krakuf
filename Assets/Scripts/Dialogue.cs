using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject npcText;
    public LayerMask layer;
    AudioSource audioSource;
    public bool stopNpc = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        npcText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, 3, layer))
        {
            npcText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                stopNpc = true;
                audioSource.Play();
            }
        }
        else
        {
            stopNpc = false;
            npcText.SetActive(false);
        }
    }
}
