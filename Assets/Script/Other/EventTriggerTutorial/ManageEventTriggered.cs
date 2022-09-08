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

    public GameObject tasks;

    public void ShowGemDialog()
    {
        tasks.transform.Find("Init Zone").gameObject.SetActive(false);
        triggerGem.TriggerDialogue();
        tasks.transform.Find("Gem Zone").gameObject.SetActive(true);
    }

    public void ShowEnemyDialog()
    {
        tasks.transform.Find("Gem Zone").gameObject.SetActive(false);
        triggerEnemy.TriggerDialogue();
        tasks.transform.Find("Enemy Zone").gameObject.SetActive(true);
    }

    public void ShowJigglyDialog()
    {
        tasks.transform.Find("Enemy Zone").gameObject.SetActive(false);
        triggerJiggly.TriggerDialogue();
        tasks.transform.Find("Jiggly Zone").gameObject.SetActive(true);
    }

    public void ShowFinalDialog()
    {
        tasks.transform.Find("Jiggly Zone").gameObject.SetActive(false);
        triggerFinale.TriggerDialogue();
        tasks.transform.Find("Final Zone").gameObject.SetActive(true);
    }


}
