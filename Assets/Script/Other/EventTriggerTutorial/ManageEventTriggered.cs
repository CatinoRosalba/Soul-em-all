using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageEventTriggered : MonoBehaviour
{
    [Header("Trigger Zona Gemme")]
    public DialogueTrigger triggerGem;

    [Header("Trigger Zona Nemici")]
    public DialogueTrigger triggerEnemy;

    [Header("Trigger Zona Jiggly")]
    public DialogueTrigger triggerJiggly;

    [Header("Trigger Zona Finale")]
    public DialogueTrigger triggerFinale;


    public void showGemDialog()
    {
        Debug.Log("Sono nella zona gemme");
        triggerGem.TriggerDialogue();
    }
    
    public void showEnemyDialog()
    {
        Debug.Log("Sono nella zona Nemici");
        triggerEnemy.TriggerDialogue();
    }

    public void showJigglyDialog()
    {
        Debug.Log("Sono nella zona Nemici");
        triggerJiggly.TriggerDialogue();
    }

    public void showFinalDialog()
    {
        Debug.Log("Sono nella zona Nemici");
        triggerFinale.TriggerDialogue();
    }
}
