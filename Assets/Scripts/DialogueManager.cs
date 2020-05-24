using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public GameObject responsePrefab;

    public Animator animator;

   private Queue<string> sentences;
    private Queue<string> responses;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        responses = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if (sentences.Count == 0)
        {
            foreach(Response _responses in dialogue.responses)
            {
                Instantiate<GameObject>(responsePrefab as GameObject);
                //_responses.choiceText = dialogue.cho
            }
            EndDialogue();
            return;
        }
        string sentence =  sentences.Dequeue();
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
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        //Debug.Log("End Of Conversation");
    }

}
