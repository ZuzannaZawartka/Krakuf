using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public QuestGiver questGiver;
    public GameObject npcText;
    public LayerMask layer;
    AudioSource audioSource;
    public AudioClip[] shoot;
    private AudioClip shootClip;
    public bool stopNpc = false;

    // Start is called before the first frame update
    void Start()
    {
        questGiver = GetComponent<QuestGiver>();
        audioSource = GetComponent<AudioSource>();
        npcText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, 3, layer))
        {
            npcText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                questGiver.OpenQuestWindow();
                int index = UnityEngine.Random.Range(0, shoot.Length);
                stopNpc = true;
                shootClip = shoot[index];
                audioSource.clip = shootClip;
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