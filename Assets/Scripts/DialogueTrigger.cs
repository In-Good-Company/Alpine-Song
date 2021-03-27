using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable {

    public Dialogue dialogue;


    public override void Interact()
    {
        base.Interact();

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.currentFaceExpressionController = GetComponent<FaceExpressionController>();
        dialogueManager.StartDialogue(dialogue);

    }

}
