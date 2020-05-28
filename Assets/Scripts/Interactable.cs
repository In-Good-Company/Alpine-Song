using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform playerInteractPos;
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
