using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header ("Dialogue Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    
    
    Queue<string> sentences;


    // Start is called before the first frame
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {

        // Press Spacebar to go to the next sentence

        if (Input.GetButtonDown("Interact"))
        {
            DisplayNextSentence();
        }
    }// END Update

    // Starts dialogue
    public void StartDialogue(Dialogue dialogue)
    {

        // Open the dialogue box visually
        animator.SetBool("isOpen", true);

        // Checks who's talking
        nameText.text = dialogue.name;

        // Gets rid of old convo
        sentences.Clear();

        // Gets sentences ready
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }// END StartDialogue

    public void DisplayNextSentence()
    {
        // Stops chatting when you run out of things to say
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }


        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }// END TypeSentence

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

}
