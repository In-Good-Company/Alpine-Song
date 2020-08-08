using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : Interactable
{
    public Animator anim;
    public bool isOpen = false;
    public override void Interact()
    {
        base.Interact();
        toggleOpen();
        Debug.Log("shouldBeOpening");
    }

    public void toggleOpen()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
        AkSoundEngine.PostEvent("Gates", gameObject);
    }
}
