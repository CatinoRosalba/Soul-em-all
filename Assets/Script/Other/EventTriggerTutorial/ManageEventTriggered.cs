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
        triggerGem.TriggerDialogue();
    }
    
    public void showEnemyDialog()
    {
        triggerEnemy.TriggerDialogue();
    }

    public void showJigglyDialog()
    {
        triggerJiggly.TriggerDialogue();
    }

    public void showFinalDialog()
    {
        triggerFinale.TriggerDialogue();
    }
}
