using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanva;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogueCanva.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}
