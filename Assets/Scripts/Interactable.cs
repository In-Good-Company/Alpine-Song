using UnityEngine;


//This is a parent scripts for anything that needs interaction
//any child script only needs to inherit and place  "override Void Interact()" without the comments to be interactable through clicking
public class Interactable : MonoBehaviour
{
    public GameObject playerInteractPos;
    public float radius = 3f;
    public Transform InteractableTransform;

    public virtual void Interact()
    {
        // this method to be overwritten

    }

    private void OnDrawGizmosSelected()
    {

        if(InteractableTransform == null)
        {
            InteractableTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
