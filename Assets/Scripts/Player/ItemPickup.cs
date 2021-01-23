using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {

        Debug.Log("Picked up " + item.name);
        Inventory.instance.AddItem(item);
        //add to inventory
        Destroy(gameObject);
    }
}
