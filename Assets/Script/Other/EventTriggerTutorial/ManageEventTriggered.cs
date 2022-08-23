using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageEventTriggered : MonoBehaviour
{
    public DialogueTrigger trigger;

    private void Start()
    {
        TriggerEvent[] triggers = GameObject.FindObjectsOfType<TriggerEvent>();

        foreach(TriggerEvent trigger in triggers)
        {
            trigger.onTrigger.AddListener(showGemInstruction);
        }
    }

    public void showGemInstruction()
    {
        Debug.Log("Sono nella zona gemme");
        trigger.TriggerDialogue();
    }
}
