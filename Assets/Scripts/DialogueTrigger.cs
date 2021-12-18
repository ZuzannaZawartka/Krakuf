using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    public LayerMask layer;


    private void Update()
    {
        if (Physics.CheckSphere(transform.position, 3, layer) && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            playerInRange = true;
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }

    private void Awake()
    {
        visualCue.SetActive(false);
    }
}