using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public GameObject responsePrefab;
    public GameObject continueButton;

    public Animator animator;

    private Queue<string> sentences;
    //private Queue<string> responses;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        //responses = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
    {
        continueButton.SetActive(true);
        continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
        continueButton.GetComponent<Button>().onClick.AddListener(() => { DisplayNextSentence(dialogue); });
        
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.characterName;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        //responses.Clear();
        if (sentences.Count == 1 && dialogue.responses.Length != 0)
        {
            foreach (Response _responses in dialogue.responses)
            {
                string _choiceText = "";
                string choiceText = _responses.GetChoiceText(_choiceText);
                GameObject responseObject = Instantiate<GameObject>(responsePrefab as GameObject, GameObject.Find("ResponseBox").transform);
                //_responses.choiceText = choiceText;
                Text displayChoiceText = responseObject.GetComponentInChildren<Text>();// = _responses.choiceText;
                displayChoiceText.text = choiceText;
                
                
                Dialogue _nextDialogue = null;
                Dialogue nextDialogue = _responses.GetNextDialogue(_nextDialogue);
                Button responseButton = responseObject.GetComponent<Button>();
                responseButton.onClick.RemoveAllListeners();
                responseButton.onClick.AddListener(() => { ResponseClicked(nextDialogue); });
            }
                continueButton.SetActive(false);
            
            //EndDialogue();
        }
        if (sentences.Count == 0 && dialogue.responses.Length == 0)
        {
           //foreach(Response _responses in dialogue.responses)
           //{
           //    Instantiate<GameObject>(responsePrefab as GameObject);
           //    //_responses.choiceText = dialogue.cho
           //}
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

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End Of Conversation");
    }

    public void ResponseClicked(Dialogue _nextDialogue)
    {
        continueButton.SetActive(true);
        GameObject[] responseObjects = GameObject.FindGameObjectsWithTag("responseObject");
        foreach(GameObject go in responseObjects)
        { 
             GameObject.Destroy(go);
            Debug.Log("destoryed " + go);
        }
        StartDialogue(_nextDialogue);
    }
}
