using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    GameObject cameraObj; 
    public GameObject dialogueCanva;
    public Dialogue dialogue;

    public static bool isStartedDialogue;

    public void TriggerDialogue()
    {
        cameraObj = GameObject.Find("Third Person Camera");

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogueCanva.SetActive(true);
        isStartedDialogue = true;

        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}
