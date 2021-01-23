using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : Interactable
{
    public Animator anim;
    public bool isOpen = false;
    public Item keyItem;
    public bool isLocked = false;
    public override void Interact()
    {
        base.Interact();
        if (isLocked == true && Inventory.instance.activeItem == keyItem)
        {
            isLocked = false;
            Inventory.instance.RemoveItem(Inventory.instance.activeItem);
        }

        if (!isLocked)
            {
                toggleOpen();
            }
            
    }

    public void toggleOpen()
    {
        if (Inventory.instance.activeItem == keyItem || keyItem == null)
        {
            isOpen = !isOpen;
            anim.SetBool("isOpen", isOpen);
            AkSoundEngine.PostEvent("Gates", gameObject);
        }
    }
}
