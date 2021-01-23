using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform InteractableTransform;
    public GameObject playerInteractPos;

    private void Start()
    {
        if (playerInteractPos == null)
        {
            playerInteractPos = this.gameObject;
        }
    }

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
    }
}
