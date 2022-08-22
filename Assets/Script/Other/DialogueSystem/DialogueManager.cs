using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Script
    public DialogueTrigger trigger;

    private Queue<string> sentences;
    public TMP_Text avatarNameText;
    public TMP_Text dialogueText;

    private void Start()
    {
        sentences = new Queue<string>();
        trigger.TriggerDialogue();                                  //Trigger per iniziare il dialogo
    }

    public void StartDialogue(Dialogue dialogue)
    {
        avatarNameText.text = dialogue.avatarName;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
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
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    //Fine dialogo
    public void EndDialogue()
    {
        trigger.dialogueCanva.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
