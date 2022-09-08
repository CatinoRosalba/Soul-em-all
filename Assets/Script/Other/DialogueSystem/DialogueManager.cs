using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Sound
    private GameObject sfx;
    private AudioSource dialogueSound;

    //Script
    public DialogueTrigger trigger;
    GameObject cameraObj;

    private Queue<string> sentences;
    public Image avatarImage; 
    public TMP_Text avatarNameText;                             //Nome dell'avatar
    public TMP_Text dialogueText;                               //Testo dialogo
    private bool start;                                         //Controllo primo dialogo

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        dialogueSound = sfx.transform.Find("SFX - Dialogue").GetComponent<AudioSource>();
        sentences = new Queue<string>();
        start = true;
        StartCoroutine(StartFirstDialogue());
    }

    IEnumerator StartFirstDialogue()
    {
        yield return new WaitForSeconds(1);
        if (start)                                              //Dopo un secondo dall'inizio della scena parte il dialogo
        {
            trigger.TriggerDialogue();                          //Trigger per iniziare il dialogo
            start = false;                                      //indico che non siamo più in fase di start
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        avatarNameText.text = dialogue.avatarName;
        avatarImage.sprite = dialogue.avatarImage;

        if(avatarImage.sprite.name == "jiggly_solo1024")
        {
            avatarImage.rectTransform.sizeDelta = new Vector2(83, 56);
        }
        else
        {
            avatarImage.rectTransform.sizeDelta = new Vector2(77, 70);
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Mostra le frasi
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Animazione di typing
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if (!dialogueSound.isPlaying)
            {
                dialogueSound.Play();
            }
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    //Fine dialogo
    public void EndDialogue()
    {
        dialogueSound.Stop();
        cameraObj = GameObject.Find("Third Person Camera");

        trigger.dialogueCanva.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraObj.GetComponent<FreeLookAxisDriver>().enabled = true;
        Time.timeScale = 1;
        
        DialogueTrigger.isStartedDialogue = false;
    }
}
